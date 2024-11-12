using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChasingState : EnemyBaseState
{
    public EnemyChasingState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        _stateMachine.MovementSpeedModifier = _groundData.ChasingSpeedModifier;
        _stateMachine.Enemy.NavMeshAgent.speed = _stateMachine.MovementSpeed + _stateMachine.MovementSpeedModifier;
        _stateMachine.Enemy.NavMeshAgent.isStopped = false;
        base.Enter();
        StartAnimation(_stateMachine.Enemy.AnimationData.GroundParameterHash);
        StartAnimation(_stateMachine.Enemy.AnimationData.ChasingParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_stateMachine.Enemy.AnimationData.GroundParameterHash);
        StopAnimation(_stateMachine.Enemy.AnimationData.ChasingParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (!IsInChasingRange())
        {
            _stateMachine.Enemy.NavMeshAgent.SetDestination(_stateMachine.Enemy.transform.position);
            _stateMachine.ChangeState(_stateMachine.IdleState);
            return;
        }
        else
        {
            NavMeshPath path = new NavMeshPath(); // 다양한 정보로 세분화 가능 (추적 불가, 장애물의 여부 등)
            if (_stateMachine.Enemy.NavMeshAgent.CalculatePath(_stateMachine.Target.transform.position, path))
            {
                _stateMachine.Enemy.NavMeshAgent.SetDestination(_stateMachine.Target.transform.position);
            }
            else
            {
                _stateMachine.Enemy.NavMeshAgent.SetDestination(_stateMachine.Enemy.transform.position);
                _stateMachine.ChangeState(_stateMachine.IdleState);
                return;
            }

            if (IsInAttackRange())
            {
                _stateMachine.ChangeState(_stateMachine.AttackState);
                return;
            }
        }
    }

    protected bool IsInAttackRange()
    {
        float playerDistanceSqr = (_stateMachine.Target.transform.position - _stateMachine.Enemy.transform.position).sqrMagnitude;
        return playerDistanceSqr <= _stateMachine.Enemy.Data.AttackRange * _stateMachine.Enemy.Data.AttackRange;
    }
}
