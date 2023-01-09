using MHR.StateMachine;
using System;
using UnityEngine;
using Player.States;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private Transform _cameraForward;
        [Header("States")]
        [SerializeField] private PlayerDefaultState _defaultState;
        [SerializeField] private PlayerMowerState _mowerState;
        [Header("Physics")]
        [SerializeField] private float _artificialGravity;
        [Header("Ground Check")]
        [SerializeField] private LayerMask _groundMask;
        [SerializeField] private Transform _groundCheck;
        [SerializeField] private float _groundCheckRadius;
        private StateMachine _stateMachine;
        private Vector2 _movementVector;
        private Vector3 _relativeMovementVector;
        private bool _grounded;
        public Rigidbody RB => _rb;
        public bool Grounded => _grounded;
        public Transform CameraForward => _cameraForward;
        public event Action HitGround;
        public event Action LeftGround;

        private void Awake()
        {
            _stateMachine = new StateMachine(_defaultState,
                new Connection(_defaultState, _mowerState),
                new Connection(_mowerState, _defaultState)
                );

            _defaultState.SetPlayerManager(this);
            _mowerState.SetPlayerManager(this);
        }

        private void OnEnable()
        {
            PlayerInput.Instance.InputMovement += OnInputMovement;
        }

        private void OnDisable()
        {
            PlayerInput.Instance.InputMovement -= OnInputMovement;
        }

        private void OnInputMovement(Vector2 value)
        {
            SetMovementVector(value);
        }

        private void Update()
        {
            _stateMachine.CurrentState.ProcessState();

            CheckGround();
        }

        private void FixedUpdate()
        {
            ArtificialGravity();

            _stateMachine.CurrentState.ProcessStateFixed();

            _movementVector = Vector2.zero;
        }

        private void ArtificialGravity()
        {
            _rb.AddForce(Vector3.down * _artificialGravity, ForceMode.Acceleration);
        }

        private void SetMovementVector(Vector2 vector)
        {
            _movementVector = vector;
        }

        public Vector3 GetRelativeMovementVector()
        {
            _relativeMovementVector = Vector3.zero;
            _relativeMovementVector += _cameraForward.forward * _movementVector.y;
            _relativeMovementVector += _cameraForward.right * _movementVector.x;
            return _relativeMovementVector;
        }

        private void CheckGround()
        {
            if (Physics.OverlapSphere(_groundCheck.position, _groundCheckRadius, _groundMask).Length > 0)
            {
                if (_grounded == false)
                {
                    _grounded = true;
                    HitGround?.Invoke();
                }
            }
            else
            {
                if (_grounded == true)
                {
                    _grounded = false;
                    LeftGround?.Invoke();
                }
            }
        }

        private void ToggleMowerState()
        {
            if (_stateMachine.CurrentState == _defaultState)
            {
                _stateMachine.TryChangeState(_mowerState);
            }
            else if (_stateMachine.CurrentState == _mowerState)
            {
                _stateMachine.TryChangeState(_defaultState);
            }
        }
    }
}