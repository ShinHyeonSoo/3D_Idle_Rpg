using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerAnimationData
{
    [SerializeField] private string _groundParameterName = "@Ground";
    [SerializeField] private string _idleParameterName = "Idle";
    [SerializeField] private string _chasingParameterName = "Chasing";

    [SerializeField] private string _attackParameterName = "@Attack";
    [SerializeField] private string _comboAttackParameterName = "ComboAttack";

    [SerializeField] private string _baseAttackParameterName = "BaseAttack";

    public int GroundParameterHash { get; private set; }
    public int IdleParameterHash { get; private set; }
    public int ChasingParameterHash { get; private set; }

    public int AttackParameterHash { get; private set; }
    public int ComboAttackParameterHash { get; private set; }

    public int BaseAttackParameterHash { get; private set; }

    public void Initialize()
    {
        GroundParameterHash = Animator.StringToHash(_groundParameterName);
        IdleParameterHash = Animator.StringToHash(_idleParameterName);
        ChasingParameterHash = Animator.StringToHash(_chasingParameterName);

        AttackParameterHash = Animator.StringToHash(_attackParameterName);
        ComboAttackParameterHash = Animator.StringToHash(_comboAttackParameterName);

        BaseAttackParameterHash = Animator.StringToHash(_baseAttackParameterName);
    }
}
