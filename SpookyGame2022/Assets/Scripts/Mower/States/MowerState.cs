using MHR.StateMachine;

namespace Mower.States
{
    public abstract class MowerState : State
    {
        private MowerManager _mowerManager;
        protected MowerManager MowerManager => _mowerManager;

        public void SetMowerManager(MowerManager mowerManager)
        {
            _mowerManager = mowerManager;
        }
        
        public abstract override void EnterState();
        public abstract override void ProcessState();
        public abstract override void ProcessStateFixed();
        public abstract override void ExitState();
    }
}