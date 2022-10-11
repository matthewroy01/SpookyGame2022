using MHR.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private Rigidbody _rb;
        [Header("States")]
        [SerializeField] private PlayerDefaultState _defaultState;
        [Header("Physics")]
        [SerializeField] private float _artificialGravity;
        private StateMachine _stateMachine;
        private Vector2 _movementVector;
        public Rigidbody RB => _rb;
        public Vector2 MovementVector => _movementVector;

        private void Awake()
        {
            _stateMachine = new StateMachine(_defaultState,
                new Connection(_defaultState));

            _defaultState.SetPlayerManager(this);
        }

        private void OnEnable()
        {
            _playerInput.InputMovement += OnInputMovement;
        }

        private void OnDisable()
        {
            _playerInput.InputMovement -= OnInputMovement;
        }

        private void OnInputMovement(Vector2 value)
        {
            SetMovementVector(value);
        }

        private void Update()
        {
            _stateMachine.CurrentState.ProcessState();
        }

        private void FixedUpdate()
        {
            ArtificialGravity();

            _stateMachine.CurrentState.ProcessStateFixed();
        }

        private void ArtificialGravity()
        {
            _rb.AddForce(Vector3.down * _artificialGravity, ForceMode.Acceleration);
        }

        private void SetMovementVector(Vector2 vector)
        {
            _movementVector = vector;
        }
    }
}