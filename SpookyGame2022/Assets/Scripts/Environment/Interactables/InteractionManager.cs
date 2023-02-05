using System;
using System.Collections.Generic;
using Player;
using UnityEngine;

namespace Environment
{
    public class InteractionManager : MonoBehaviour
    {
        [SerializeField] private float _maxInteractionDistance;
        private List<Interactable> _interactables = new List<Interactable>();
        private List<Interactable> _collidedInteractables = new List<Interactable>();
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

            _collidedInteractables.Clear();

            foreach (Interactable interactable in _interactables)
            {
                Interactable tmp = interactable.GetCollision(_mainCamera.transform.position,
                    _mainCamera.transform.forward, ref _raycastHits,
                    _maxInteractionDistance + interactable.ExtraInteractionDistance);
                
                if (tmp == null)
                {
                    continue;
                }

                _collidedInteractables.Add(tmp);
            }

            if (_collidedInteractables.Count == 0)
            {
                ExitInteractable();
                return;
            }

            Interactable closestInteractable = GetClosestInteractable();

            if (closestInteractable == null)
            {
                // player is not looking at an interactable object
                ExitInteractable();
            }
            else
            {
                // player is looking at an interactable object
                EnterInteractable(closestInteractable);
            }
        }

        private void EnterInteractable(Interactable interactable)
        {
            _currentInteractable = interactable;

            if (_colliding == false)
            {
                EnterCollisionWithInteractable?.Invoke();
            }

            _colliding = true;
        }
        
        private void ExitInteractable()
        {
            _currentInteractable = null;

            if (_colliding == true)
            {
                ExitCollisionWithInteractable?.Invoke();
            }

            _colliding = false;
        }

        private void OnInputLeftClick()
        {
            if (_currentInteractable == null)
            {
                return;
            }

            _currentInteractable.Interact();
        }

        private Interactable GetClosestInteractable()
        {
            Interactable closestInteractable = null;
            float closestDistance = float.MaxValue;
            
            foreach (Interactable interactable in _collidedInteractables)
            {
                if (interactable.DistanceAway > closestDistance)
                {
                    continue;
                }
                
                closestInteractable = interactable;
                closestDistance = interactable.DistanceAway;
            }

            return closestInteractable;
        }
    }
}