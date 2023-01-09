using System;
using System.Collections.Generic;
using Player;
using UnityEngine;

namespace Environment
{
    public class InteractionManager : MonoBehaviour
    {
        [SerializeField] private float _maxInteractionDistance;
        private List<Interactable> _interactables;
        private RaycastHit[] _raycastHits = new RaycastHit[10];
        private bool _colliding;
        private Interactable _currentInteractable;
        private Camera _mainCamera;
        public Action EnterCollisionWithInteractable;
        public Action ExitCollisionWithInteractable;
        
        private void Awake()
        {
            _interactables = new List<Interactable>(FindObjectsOfType<Interactable>());
            _mainCamera = Camera.main;
        }

        private void OnEnable()
        {
            PlayerInput.Instance.InputLeftClick += OnInputLeftClick;
        }

        private void OnDisable()
        {
            PlayerInput.Instance.InputLeftClick -= OnInputLeftClick;
        }

        private void Update()
        {
            LookForCollision();
        }

        private void LookForCollision()
        {
            if (_mainCamera == null)
            {
                return;
            }
            
            foreach (Interactable interactable in _interactables)
            {
                if (interactable.GetCollision(_mainCamera.transform.position, _mainCamera.transform.forward, ref _raycastHits, _maxInteractionDistance))
                {
                    // player is looking at an interactable object
                    _currentInteractable = interactable;

                    if (_colliding == false)
                    {
                        EnterCollisionWithInteractable?.Invoke();
                    }
                    
                    _colliding = true;
                }
                else
                {
                    // player is not looking at an interactable object
                    _currentInteractable = null;
                    
                    if (_colliding == true)
                    {
                        ExitCollisionWithInteractable?.Invoke();
                    }

                    _colliding = false;
                }
            }
        }

        private void OnInputLeftClick()
        {
            if (_currentInteractable == null)
            {
                return;
            }
            
            _currentInteractable.Interact();
        }
    }
}