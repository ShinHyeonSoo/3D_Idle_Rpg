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
            // TODO : ������ ������ false �� ������.
            _stateMachine.IsAttacking = true;
            _stateMachine.Player.NavMeshAgent.SetDestination(_stateMachine.Player.transform.position);
            _stateMachine.ChangeState(_stateMachine.IdleState);
            return;
        }
        else
        {
            NavMeshPath path = new NavMeshPath(); // �پ��� ������ ����ȭ ���� (���� �Ұ�, ��ֹ��� ���� ��)
            if (_stateMachine.Player.NavMeshAgent.CalculatePath(_stateMachine.Target.transform.position, path))
            {
                _stateMachine.Player.NavMeshAgent.SetDestination(_stateMachine.Target.transform.position);
                Debug.Log("Chasing State");
            }
            else
            {
                _stateMachine.Player.NavMeshAgent.SetDestination(_stateMachine.Player.transform.position);
                _stateMachine.ChangeState(_stateMachine.IdleState);
                return;
            }

            if (IsInAttackRange())
            {
                _stateMachine.ChangeState(_stateMachine.ComboAttackState);
                return;
            }
        }
    }
}
