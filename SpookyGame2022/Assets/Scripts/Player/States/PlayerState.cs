using MHR.StateMachine;
using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : State
{
    private PlayerManager _playerManager;
    public PlayerManager PlayerManager => _playerManager;

    public void SetPlayerManager(PlayerManager playerManager)
    {
        _playerManager = playerManager;
    }

    public override abstract void EnterState();
    public override abstract void ProcessState();
    public override abstract void ProcessStateFixed();
    public override abstract void ExitState();
}