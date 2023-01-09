using UnityEngine;

namespace Player.States
{
    public class PlayerDefaultState : PlayerState
    {
        [Header("Movement")]
        [SerializeField] private float _movementAcceleration;
        [SerializeField] private float _movementSpeed;
        [Header("Jumping")]
        [SerializeField] private float _jumpForce;
        private Vector3 _movementForce;
        private Vector3 _force;

        private void OnEnable()
        {
            PlayerInput.Instance.InputJump += OnInputJump;
        }

        private void OnDisable()
        {
            PlayerInput.Instance.InputJump -= OnInputJump;
        }

        public override void EnterState()
        {
            _movementForce = Vector3.zero;
        }

        public override void ProcessState() { }

        public override void ProcessStateFixed()
        {
            Movement();
        }

        private void Movement()
        {
            Vector3 movementVector = PlayerManager.GetRelativeMovementVector();
            _force = new Vector3(movementVector.x, 0.0f, movementVector.z) * _movementSpeed;
            _movementForce = Vector3.Lerp(_movementForce, _force, Time.deltaTime * _movementAcceleration);

            PlayerManager.RB.velocity = new Vector3(_movementForce.x, PlayerManager.RB.velocity.y, _movementForce.z);
        }

        private void OnInputJump()
        {
            Jump();
        }

        private void Jump()
        {
            if (!PlayerManager.Grounded)
            {
                return;
            }

            PlayerManager.RB.AddForce(Vector3.up * _jumpForce, ForceMode.Acceleration);
        }

        public override void ExitState()
        {
            _movementForce = Vector3.zero;
        }
    }
}