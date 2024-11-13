using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerComboAttackState : PlayerAttackState
{
    private bool _alreadyAppliedCombo;
    //private bool _alreadyApllyForce;
    private bool _alreadyApliedDealing;

    private int _delayTime = 1000;  // 1√ 

    AttackInfoData _attackInfoData;

    public PlayerComboAttackState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(_stateMachine.Player.AnimationData.ComboAttackParameterHash);

        _alreadyAppliedCombo = false;
        //_alreadyApllyForce = false;
        _alreadyApliedDealing = false;

        int comboIndex = _stateMachine.ComboIndex;
        _attackInfoData = _stateMachine.Player.Data.AttackData.GetAttackInfoData(comboIndex);
        _stateMachine.Player.Animator.SetInteger("Combo", comboIndex);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_stateMachine.Player.AnimationData.ComboAttackParameterHash);

        if (!_alreadyAppliedCombo)
        {
            _stateMachine.ComboIndex = 0;

            AttackDelay();
        }
    }

    private async void AttackDelay()
    {
        _stateMachine.IsDelaying = true;

        await Task.Delay(_delayTime);

        _stateMachine.IsDelaying = false;
    }

    public override void Update()
    {
        base.Update();

        //ForceMove();

        float normalizedTime = GetNormalizedTime(_stateMachine.Player.Animator, "Attack");
        if (normalizedTime < 1f)
        {
            if (normalizedTime >= _attackInfoData.ComboTransitionTime)
            {
                if (_stateMachine.Target == null)
                {
                    _stateMachine.ChangeState(_stateMachine.IdleState);
                    return;
                }

                // ƒﬁ∫∏ Ω√µµ
                TryComboAttack();
            }

            //if (normalizedTime >= _attackInfoData.ForceTransitionTime)
            //{
            //     ¥Ô«Œ Ω√µµ
            //    TryApplyForce();
            //}

            int weaponStats = CharacterManager.Instance.Player.Equipment.EquipStats[(int)EquipType.Weapon];

            if (!_alreadyApliedDealing && normalizedTime >= _stateMachine.Player.Data.AttackData.
                GetAttackInfoData(_stateMachine.ComboIndex).Dealing_Start_TransitionTime)
            {
                _stateMachine.Player.Weapon.SetAttack(_stateMachine.Player.Data.AttackData.
                    GetAttackInfoData(_stateMachine.ComboIndex).Damage + weaponStats, 
                    _stateMachine.Player.Data.AttackData.GetAttackInfoData(_stateMachine.ComboIndex).Force);
                _stateMachine.Player.Weapon.gameObject.SetActive(true);
                _alreadyApliedDealing = true;

                if (_stateMachine.ComboIndex == 2)
                    CameraManager.Instance.CameraZoom.ZoomIn();
            }

            if (_alreadyApliedDealing && normalizedTime >= _stateMachine.Player.Data.AttackData.
                GetAttackInfoData(_stateMachine.ComboIndex).Dealing_End_TransitionTime)
            {
                _stateMachine.Player.Weapon.gameObject.SetActive(false);

                if (_stateMachine.ComboIndex == 2)
                    CameraManager.Instance.CameraZoom.ZoomOut();
            }
        }
        else
        {
            if (_alreadyAppliedCombo)
            {
                _stateMachine.ComboIndex = _attackInfoData.ComboStateIndex;
                _stateMachine.ChangeState(_stateMachine.ComboAttackState);
            }
            else
            {
                _stateMachine.ChangeState(_stateMachine.IdleState);
            }
        }
    }

    private void TryComboAttack()
    {
        if (_alreadyAppliedCombo) return;

        if (_attackInfoData.ComboStateIndex == -1) return;

        //if (!_stateMachine.IsAttacking) return;

        _alreadyAppliedCombo = true;
    }

    //private void TryApplyForce()
    //{
    //    if (_alreadyApllyForce) return;
    //    _alreadyApllyForce = true;

    //    _stateMachine.Player.ForceReceiver.Reset();
    //    _stateMachine.Player.ForceReceiver.AddForce(Vector3.forward * _attackInfoData.Force);
    //}
}
