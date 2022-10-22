using Grass;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerMowerState : PlayerState
    {
        [SerializeField] private Transform _mowerTransform;
        [SerializeField] private float _mowerRadius;
        private Vector3 _previousPosition;
        public event Action<Vector3, float> MowerPositionUpdated;

        public override void EnterState()
        {
            
        }

        public override void ProcessState()
        {
            Mow();
        }

        public override void ProcessStateFixed() { }

        public override void ExitState()
        {
            
        }

        private void Mow()
        {
            if (_mowerTransform.position != _previousPosition)
            {
                MowerPositionUpdated?.Invoke(_mowerTransform.position, _mowerRadius);
            }

            _previousPosition = _mowerTransform.position;
        }
    }
}