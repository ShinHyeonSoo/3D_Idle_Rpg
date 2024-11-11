using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        _stateMachine.MovementSpeedModifier = 0f;
        _stateMachine.Player.NavMeshAgent.speed = _stateMachine.MovementSpeedModifier;
        _stateMachine.Player.NavMeshAgent.isStopped = true;
        base.Enter();
        StartAnimation(_stateMachine.Player.AnimationData.GroundParameterHash);
        StartAnimation(_stateMachine.Player.AnimationData.IdleParameterHash);
    }

    public override void Exit() 
    { 
        base.Exit();
        StopAnimation(_stateMachine.Player.AnimationData.GroundParameterHash);
        StopAnimation(_stateMachine.Player.AnimationData.IdleParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (IsInChasingRange())
        {
            _stateMachine.ChangeState(_stateMachine.ChasingState);
            return;
        }
    }
}
