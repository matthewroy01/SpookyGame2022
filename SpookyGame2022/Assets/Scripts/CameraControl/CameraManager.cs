using MHR.StateMachine;
using UnityEngine;
using CameraControl.States;

namespace CameraControl
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _cameraParentForHorizontalRotation;
        [Header("States")]
        [SerializeField] private DefaultCameraState _defaultState;
        private StateMachine _stateMachine;
        public Camera Camera => _camera;
        public Transform CameraParentForHorizontalRotation => _cameraParentForHorizontalRotation;

        private void Awake()
        {
            _defaultState.SetCameraManager(this);
        
            _stateMachine = new StateMachine(_defaultState,
                new Connection(_defaultState));
        }

        private void Update()
        {
            _stateMachine.CurrentState.ProcessState();
        }
    }   
}