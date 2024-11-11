using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player Player { get; }

    public float MovementSpeed { get; private set; }
    public float RotationDamping { get; private set; }
    public float MovementSpeedModifier { get; set; } = 1f;

    public bool IsAttacking { get; set; }
    public int ComboIndex { get; set; }

    public Transform MainCamTransform { get; set; }


    public GameObject Target { get; set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerChasingState ChasingState { get; private set; }
    
    public PlayerComboAttackState ComboAttackState { get; private set; }

    public PlayerStateMachine(Player player)
    {
        this.Player = player;

        MainCamTransform = Camera.main.transform;

        // TODO : ���� Ÿ���� �ڵ����� ã�� ����. ������ �ڵ�
        Target = GameObject.FindGameObjectWithTag("Enemy");

        IdleState = new PlayerIdleState(this);
        ChasingState = new PlayerChasingState(this);
        
        ComboAttackState = new PlayerComboAttackState(this);
        
        MovementSpeed = player.Data.GroundData.BaseSpeed;
        RotationDamping = player.Data.GroundData.BaseRotationDamping;
    }
}
