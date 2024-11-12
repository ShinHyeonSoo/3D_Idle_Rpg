using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    //private bool _alreadyApllyForce;
    private bool _alreadyApliedDealing;

    public EnemyAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        _stateMachine.MovementSpeedModifier = 0;
        _stateMachine.Enemy.NavMeshAgent.speed = _stateMachine.MovementSpeedModifier;
        _stateMachine.Enemy.NavMeshAgent.isStopped = true;
        base.Enter();
        StartAnimation(_stateMachine.Enemy.AnimationData.AttackParameterHash);
        StartAnimation(_stateMachine.Enemy.AnimationData.BaseAttackParameterHash);
        //_alreadyApllyForce = false;
        _alreadyApliedDealing = false;
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_stateMachine.Enemy.AnimationData.AttackParameterHash);
        StopAnimation(_stateMachine.Enemy.AnimationData.BaseAttackParameterHash);
    }

    public override void Update()
    {
        base.Update();

        //ForceMove();

        float normalizedTime = GetNormalizedTime(_stateMachine.Enemy.Animator, "Attack");
        if (normalizedTime < 1f)
        {
            if (normalizedTime >= _stateMachine.Enemy.Data.ForceTransitionTime)
            {
                // ´ïÇÎ ½Ãµµ
                //TryApplyForce();
            }

            if (!_alreadyApliedDealing && normalizedTime >= _stateMachine.Enemy.Data.Dealing_Start_TransitionTime)
            {
                _stateMachine.Enemy.Weapon.SetAttack(_stateMachine.Enemy.Data.Damage, _stateMachine.Enemy.Data.Force);
                _stateMachine.Enemy.Weapon.gameObject.SetActive(true);
                _alreadyApliedDealing = true;
            }

            if (_alreadyApliedDealing && normalizedTime >= _stateMachine.Enemy.Data.Dealing_End_TransitionTime)
            {
                _stateMachine.Enemy.Weapon.gameObject.SetActive(false);
            }
        }
        else
        {
            if (IsInChasingRange())
            {
                _stateMachine.ChangeState(_stateMachine.ChasingState);
                return;
            }
            else
            {
                _stateMachine.ChangeState(_stateMachine.IdleState);
                return;
            }
        }
    }

    //private void TryApplyForce()
    //{
    //    if (_alreadyApllyForce) return;
    //    _alreadyApllyForce = true;

    //    _stateMachine.Enemy.ForceReceiver.Reset();
    //    _stateMachine.Enemy.ForceReceiver.AddForce(Vector3.forward * _stateMachine.Enemy.Data.Force);
    //}
}
