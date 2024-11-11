using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    public PlayerAttackState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        _stateMachine.MovementSpeedModifier = 0;
        _stateMachine.Player.NavMeshAgent.speed = _stateMachine.MovementSpeedModifier;
        _stateMachine.Player.NavMeshAgent.isStopped = true;
        base.Enter();
        StartAnimation(_stateMachine.Player.AnimationData.AttackParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_stateMachine.Player.AnimationData.AttackParameterHash);
    }
}
