using MHR.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerDefaultState : PlayerState
    {
        [Header("Movement")]
        [SerializeField] private float _movementAcceleration;
        [SerializeField] private float _movementSpeed;
        private Vector3 _movementForce;

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
            Vector3 force = new Vector3(PlayerManager.MovementVector.x, 0.0f, PlayerManager.MovementVector.y) * _movementSpeed;
            _movementForce = Vector3.Lerp(_movementForce, force, Time.deltaTime * _movementAcceleration);

            PlayerManager.RB.velocity = new Vector3(_movementForce.x, PlayerManager.RB.velocity.y, _movementForce.z);
        }

        public override void ExitState()
        {
            _movementForce = Vector3.zero;
        }
    }
}