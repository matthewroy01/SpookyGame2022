using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerInput : MonoBehaviour
    {
        private Vector2 _inputMovement;
        private Vector2 _previousInputMovement;
        private bool _inputJump;
        private bool _inputLeftClick;
        public event Action<Vector2> InputMovement;
        public event Action InputJump;
        public event Action InputLeftClick;

        private void Update()
        {
            InputPolling();
        }

        private void LateUpdate()
        {
            InvokeEvents();
        }

        private void InputPolling()
        {
            _inputLeftClick = Mouse.current.leftButton.wasPressedThisFrame;

            _inputJump = Keyboard.current.spaceKey.wasPressedThisFrame;

            float x = 0.0f, y = 0.0f;
            if (Keyboard.current.aKey.isPressed)
            {
                x -= 1.0f;
            }
            if (Keyboard.current.dKey.isPressed)
            {
                x += 1.0f;
            }
            if (Keyboard.current.sKey.isPressed)
            {
                y -= 1.0f;
            }
            if (Keyboard.current.wKey.isPressed)
            {
                y += 1.0f;
            }
            _inputMovement = new Vector2(x, y).normalized;
        }

        private void InvokeEvents()
        {
            if (_inputLeftClick)
            {
                InputLeftClick?.Invoke();
            }

            if (_inputJump)
            {
                InputJump?.Invoke();
            }

            if (_inputMovement != _previousInputMovement)
            {
                InputMovement?.Invoke(_inputMovement);
            }

            _previousInputMovement = _inputMovement;
        }
    }
}