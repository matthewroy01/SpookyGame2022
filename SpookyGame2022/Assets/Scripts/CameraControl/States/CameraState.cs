using MHR.StateMachine;

namespace CameraControl.States
{
    public abstract class CameraState : State
    {
        private CameraManager _cameraManager;
        public CameraManager CameraManager => _cameraManager;

        public void SetCameraManager(CameraManager cameraManager)
        {
            _cameraManager = cameraManager;
        }

        public override abstract void EnterState();
        public override abstract void ProcessState();
        public override abstract void ProcessStateFixed();
        public override abstract void ExitState();
    }   
}