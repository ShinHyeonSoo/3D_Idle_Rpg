using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Enemy : MonoBehaviour
{
    [field: SerializeField] public EnemySO Data { get; private set; }

    [field: Header("Animations")]
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }

    public Animator Animator { get; private set; }
    public CharacterController Controller { get; private set; }
    //public ForceReceiver ForceReceiver { get; private set; }

    public EnemyCondition Condition { get; private set; }

    public NavMeshAgent NavMeshAgent { get; private set; }

    private EnemyStateMachine _stateMachine;

    public Action RemoveTarget;

    [field: SerializeField] public Weapon Weapon { get; private set; }

    private void Awake()
    {
        AnimationData.Initialize();
        Animator = GetComponentInChildren<Animator>();
        Controller = GetComponent<CharacterController>();
        //ForceReceiver = GetComponent<ForceReceiver>();
        Condition = GetComponent<EnemyCondition>();

        NavMeshAgent = GetComponent<NavMeshAgent>();

        _stateMachine = new EnemyStateMachine(this);
    }

    void Start()
    {
        _stateMachine.ChangeState(_stateMachine.IdleState);
        RemoveTarget += CharacterManager.Instance.Player.RemoveTarget;
    }

    private void Update()
    {
        _stateMachine.HandleInput();
        _stateMachine.Update();
    }

    private void FixedUpdate()
    {
        _stateMachine.PhysicsUpdate();
    }
}
