using Player.States;
using UnityEngine;

namespace Grass
{
    public class ParticleGrassController : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;
        private float _refreshTime, _refreshTimer = 0.0f;
        private ParticleSystem.Particle[] _particles = new ParticleSystem.Particle[0];
        private PlayerMowerState _playerMowerState;

        private void Awake()
        {
            _refreshTime = _particleSystem.main.startLifetime.constant * 0.9f;
            _particles = new ParticleSystem.Particle[_particleSystem.main.maxParticles];
        }

        private void OnEnable()
        {
            if (_playerMowerState == null)
            {
                _playerMowerState = FindObjectOfType<PlayerMowerState>();
            }

            _playerMowerState.MowerPositionUpdated += OnMowerPositionUpdated;
        }

        private void OnDisable()
        {
            _playerMowerState.MowerPositionUpdated -= OnMowerPositionUpdated;
        }

        private void Update()
        {
            TryRefreshParticles();
        }

        private void TryRefreshParticles()
        {
            if (_refreshTimer >= _refreshTime)
            {
                RefreshParticles();
                _refreshTimer = 0.0f;
            }

            _refreshTimer += Time.deltaTime;
        }

        private void RefreshParticles()
        {
            _particleSystem.GetParticles(_particles);

            for (int i = 0; i < _particles.Length; i++)
            {
                if (_particles[i].remainingLifetime != float.PositiveInfinity)
                {
                    _particles[i].remainingLifetime = _particleSystem.main.startLifetime.constant;
                }
            }

            _particleSystem.SetParticles(_particles);
        }

        private void OnMowerPositionUpdated(Vector3 vector, float num)
        {
            RemoveGrassAtPosition(vector, num);
        }

        private void RemoveGrassAtPosition(Vector3 position, float radius)
        {
            _particleSystem.GetParticles(_particles);

            float sqrMagnitude = 0.0f;

            for (int i = 0; i < _particles.Length; ++i)
            {
                sqrMagnitude = Vector3.SqrMagnitude(position - _particles[i].position);
                if (sqrMagnitude <= radius * radius)
                {
                    _particles[i].remainingLifetime = -1.0f;
                }
            }

            _particleSystem.SetParticles(_particles);
        }
    }
}