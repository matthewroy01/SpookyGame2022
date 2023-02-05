using Environment;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private InteractionManager _interactionManager;
        [Header("Reticle")]
        [SerializeField] private CanvasGroup _reticleCanvasGroup;
        [SerializeField] private Image _reticleImage;
        [SerializeField] private Sprite _disabledReticle;
        [SerializeField] private Sprite _enabledReticle;
        [SerializeField, Range(0.0f, 1.0f)] private float _defaultReticleAlpha;

        private void Awake()
        {
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
            _reticleImage.sprite = _enabledReticle;
        }

        private void HideReticle()
        {
            SetReticleAlpha(_defaultReticleAlpha);
            _reticleImage.sprite = _disabledReticle;
        }

        private void SetReticleAlpha(float alpha)
        {
            _reticleCanvasGroup.alpha = alpha;
        }
    }
}