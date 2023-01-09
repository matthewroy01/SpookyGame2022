using System;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace Player
{
    public class PlayerInput : Singleton<PlayerInput>
    {
        private SpookyGameControls _controls;
        private Vector2 _movementVector;
        private Vector2 _previousMovementVector;
        public event Action<Vector2> InputMovement;
        public event Action<Vector2> InputMouseDelta;
        public event Action InputJump;
        public event Action InputLeftClick;

        protected override void Awake()
        {
            base.Awake();

            _controls = new SpookyGameControls();
            _controls.Enable();
        }

        private void OnEnable()
        {
            _controls.Base.Interact.performed += OnLeftClickPerformed;
            _controls.Base.MouseDelta.performed += OnMouseDeltaPerformed;
            _controls.Base.Jump.performed += OnJumpPerformed;
        }

        private void OnLeftClickPerformed(CallbackContext context)
        {
            InputLeftClick?.Invoke();
        }

        private void OnJumpPerformed(CallbackContext context)
        {
            InputJump?.Invoke();
        }

        private void OnMouseDeltaPerformed(CallbackContext context)
        {
            InputMouseDelta?.Invoke(context.ReadValue<Vector2>());
        }

        private void Update()
        {
            _movementVector = _controls.Base.Movement.ReadValue<Vector2>();
            if (!(_previousMovementVector == Vector2.zero && _movementVector == Vector2.zero))
            {
                InputMovement?.Invoke(_movementVector);
            }
            _previousMovementVector = _movementVector;
        }
    }
}