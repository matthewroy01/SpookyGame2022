using Player;
using UnityEngine;

namespace CameraControl.States
{
    public class DefaultCameraState : CameraState
    {
        [SerializeField] private float _maxVerticalRotation = 90.0f;
        [SerializeField] private bool _lockCursor = true;
        [SerializeField] private bool _invisibleCursor = true;
        [SerializeField] private float _mouseSensitivity = 2.0f;
        private Vector2 _mouseDelta;
        private float _inputX, _inputY;
        private float _cameraVerticalRotation = 0.0f;
    
        private void OnEnable()
        {
            PlayerInput.Instance.InputMouseDelta += OnInputMouseDelta;
        }
    
        private void OnDisable()
        {
            PlayerInput.Instance.InputMouseDelta -= OnInputMouseDelta;
        }
    
        private void OnInputMouseDelta(Vector2 value)
        {
            SetMouseDelta(value);
        }
    
        private void SetMouseDelta(Vector2 vector)
        {
            _mouseDelta = vector;
        }
    
        public override void EnterState()
        {
            _mouseDelta = Vector2.zero;
    
            Cursor.visible = !_invisibleCursor;
            Cursor.lockState = _lockCursor ? CursorLockMode.Locked : CursorLockMode.None;
        }
    
        public override void ProcessState()
        {
            MouseMovement();
        }
    
        public override void ProcessStateFixed() { }
        public override void ExitState() { }
    
        private void MouseMovement()
        {
            _inputX = _mouseDelta.x * _mouseSensitivity;
            _inputY = _mouseDelta.y * _mouseSensitivity;
    
            _cameraVerticalRotation -= _inputY;
            _cameraVerticalRotation = Mathf.Clamp(_cameraVerticalRotation, _maxVerticalRotation * -1.0f, _maxVerticalRotation);
    
            CameraManager.Camera.transform.localEulerAngles = Vector3.right * _cameraVerticalRotation;
    
            CameraManager.CameraParentForHorizontalRotation.Rotate(Vector3.up * _inputX);
    
            _mouseDelta = Vector2.zero;
        }
    }
}