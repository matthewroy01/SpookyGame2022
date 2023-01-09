using Environment;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private InteractionManager _interactionManager;
        [Header("Reticle")]
        [SerializeField] private Image _reticle;
        [SerializeField] private Color _reticleColor;
        [SerializeField, Range(0.0f, 1.0f)] private float _defaultReticleAlpha;

        private void Awake()
        {
            _reticle.color = _reticleColor;
            SetReticleAlpha(_defaultReticleAlpha);
        }

        private void OnEnable()
        {
            _interactionManager.EnterCollisionWithInteractable += OnEnterCollisionWithInteractable;
            _interactionManager.ExitCollisionWithInteractable += OnExitCollisionWithInteractable;
        }

        private void OnDisable()
        {
            _interactionManager.EnterCollisionWithInteractable -= OnEnterCollisionWithInteractable;
            _interactionManager.ExitCollisionWithInteractable -= OnExitCollisionWithInteractable;
        }

        private void OnEnterCollisionWithInteractable()
        {
            ShowReticle();
        }

        private void OnExitCollisionWithInteractable()
        {
            HideReticle();
        }

        private void ShowReticle()
        {
            SetReticleAlpha(1.0f);
        }

        private void HideReticle()
        {
            SetReticleAlpha(_defaultReticleAlpha);
        }

        private void SetReticleAlpha(float alpha)
        {
            _reticle.CrossFadeAlpha(alpha, 0.0f, false);
        }
    }
}