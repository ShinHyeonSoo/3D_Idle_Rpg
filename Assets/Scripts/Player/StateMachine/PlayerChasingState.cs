using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerChasingState : PlayerBaseState
{
    public PlayerChasingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        _stateMachine.MovementSpeedModifier = _groundData.ChasingSpeedModifier;
        _stateMachine.Player.NavMeshAgent.speed = _stateMachine.MovementSpeed + _stateMachine.MovementSpeedModifier;
        _stateMachine.Player.NavMeshAgent.isStopped = false;
        base.Enter();
        StartAnimation(_stateMachine.Player.AnimationData.GroundParameterHash);
        StartAnimation(_stateMachine.Player.AnimationData.ChasingParameterHash);
    }

    public override void Exit() 
    { 
        base.Exit();
        StopAnimation(_stateMachine.Player.AnimationData.GroundParameterHash);
        StopAnimation(_stateMachine.Player.AnimationData.ChasingParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (!IsInChasingRange())
        {
            _stateMachine.Player.NavMeshAgent.SetDestination(_stateMachine.Player.transform.position);
            _stateMachine.ChangeState(_stateMachine.IdleState);
            return;
        }
        else
        {
            NavMeshPath path = new NavMeshPath(); // 다양한 정보로 세분화 가능 (추적 불가, 장애물의 여부 등)
            if (_stateMachine.Player.NavMeshAgent.CalculatePath(_stateMachine.Target.transform.position, path))
            {
                _stateMachine.Player.NavMeshAgent.SetDestination(_stateMachine.Target.transform.position);
            }
            else
            {
                NavMeshHit hit;
                if (NavMesh.SamplePosition(_stateMachine.Player.NavMeshAgent.transform.position, out hit, 1.0f, NavMesh.AllAreas))
                {
                    _stateMachine.Player.NavMeshAgent.Warp(hit.position);
                }

                _stateMachine.Player.NavMeshAgent.SetDestination(_stateMachine.Player.transform.position);
                _stateMachine.ChangeState(_stateMachine.IdleState);
                return;
            }

            if (IsInAttackRange())
            {
                _stateMachine.ChangeState(_stateMachine.IdleState);
                return;
            }
        }
    }
}
