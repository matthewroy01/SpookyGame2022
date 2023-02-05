using MHR.StateMachine;

namespace Player.States
{
    public abstract class PlayerState : State
    {
        private PlayerManager _playerManager;
        protected PlayerManager PlayerManager => _playerManager;

        public void SetPlayerManager(PlayerManager playerManager)
        {
            _playerManager = playerManager;
        }

        public abstract override void EnterState();
        public abstract override void ProcessState();
        public abstract override void ProcessStateFixed();
        public abstract override void ExitState();
    }   
}