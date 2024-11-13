using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerBaseState : IState
{
    protected PlayerStateMachine _stateMachine;
    protected readonly PlayerGroundData _groundData;
    protected readonly LayerMask _enemyLayer = 1 << LayerMask.NameToLayer("Enemy");

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this._stateMachine = stateMachine;
        _groundData = stateMachine.Player.Data.GroundData;
    }

    public virtual void Enter()
    {
        
    }

    public virtual void Exit()
    {
        
    }

    public virtual void HandleInput()
    {
       
    }

    public virtual void PhysicsUpdate()
    {
        
    }

    public virtual void Update()
    {
        if (_stateMachine.Target != null)
        {
            if (_stateMachine.Player.NavMeshAgent.velocity.sqrMagnitude > 0.1f)
                LookAtNavPath();
            else
                Rotate();
        }
    }

    protected void StartAnimation(int animatorHash)
    {
        _stateMachine.Player.Animator.SetBool(animatorHash, true);
    }

    protected void StopAnimation(int animatorHash)
    {
        _stateMachine.Player.Animator.SetBool(animatorHash, false);
    }

    private void Rotate()
    {
        Vector3 movementDirection = GetMovementDirection();

        Rotate(movementDirection);
    }

    private Vector3 GetMovementDirection()
    {
        Vector3 dir = (_stateMachine.Target.transform.position - _stateMachine.Player.transform.position);

        return dir;
    }

    private void Rotate(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            Transform playerTransform = _stateMachine.Player.transform;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation, _stateMachine.RotationDamping * Time.deltaTime);
        }
    }

    private void LookAtNavPath()
    {
        Transform playerTransform = _stateMachine.Player.transform;
        Quaternion targetRotation = Quaternion.LookRotation(_stateMachine.Player.NavMeshAgent.velocity.normalized);
        playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation, _stateMachine.RotationDamping * Time.deltaTime);
    }

    //protected void ForceMove()
    //{
    //    _stateMachine.Player.Controller.Move(_stateMachine.Player.ForceReceiver.Movement * Time.deltaTime);
    //}

    protected float GetNormalizedTime(Animator animator, string tag)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

        // 전환되고 있을 때 && 다음 애니메이션이 tag
        if (animator.IsInTransition(0) && nextInfo.IsTag(tag))
        {
            return nextInfo.normalizedTime;
        }
        // 전환되고 있지 않을 때 && 현재 애니메이션이 tag
        else if (!animator.IsInTransition(0) && currentInfo.IsTag(tag))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0;
        }
    }

    protected bool IsInChasingRange()
    {
        //if (_stateMachine.Target._isDie) return false;

        if (_stateMachine.Target == null)
        {
            // 타겟이 null 일 때, 새 타겟을 찾는 로직
            _stateMachine.Target = NearEnemy();

            // 타겟이 null 일 때, 콤보 공격을 멈추고 Idle 상태로 돌아가기
            if (_stateMachine.Target == null) return false;
        }

        float enemyDistanceSqr = (_stateMachine.Target.transform.position - _stateMachine.Player.transform.position).sqrMagnitude;
        return enemyDistanceSqr <= _stateMachine.Player.Data.GroundData.EnemyChasingRange * _stateMachine.Player.Data.GroundData.EnemyChasingRange;
    }

    protected bool IsInAttackRange()
    {
        float playerDistanceSqr = (_stateMachine.Target.transform.position - _stateMachine.Player.transform.position).sqrMagnitude;
        return playerDistanceSqr <= _stateMachine.Player.Data.GroundData.AttackRange * _stateMachine.Player.Data.GroundData.AttackRange;
    }

    protected GameObject NearEnemy()
    {
        Collider[] hitColliders = Physics.OverlapSphere(_stateMachine.Player.transform.position, 
            _stateMachine.Player.Data.GroundData.EnemyChasingRange, _enemyLayer);

        float minDistance = float.MaxValue;
        GameObject enemy = null;

        foreach (Collider collider in hitColliders)
        {
            float distanceSqr = (collider.gameObject.transform.position - _stateMachine.Player.transform.position).sqrMagnitude;

            if (distanceSqr < minDistance)
            {
                minDistance = distanceSqr;
                enemy = collider.gameObject;
            }
        }

        return enemy;
    }
}
