using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    [field: Header("PlayerData")]
    [field: SerializeField] public PlayerSO Data { get; private set; }

    [field: Header("Animations")]
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }

    [field: Header("Weapon")]
    [field: SerializeField] public Weapon Weapon { get; private set; }

    // Components
    public Animator Animator { get; private set; }
    public CharacterController Controller { get; private set; }
    public NavMeshAgent NavMeshAgent { get; private set; }
    public PlayerCondition Condition { get; private set; }

    public Equipment Equipment { get; private set; }

    //public ForceReceiver ForceReceiver { get; private set; }

    // StateMachine
    private PlayerStateMachine _stateMachine;


    public ItemData _itemData;
    public Action AddItem;


    //public Health Health { get; private set; }

    private void Awake()
    {
        CharacterManager.Instance.Player = this;

        AnimationData.Initialize();
        Animator = GetComponentInChildren<Animator>();
        Controller = GetComponent<CharacterController>();
        //ForceReceiver = GetComponent<ForceReceiver>();
        NavMeshAgent = GetComponent<NavMeshAgent>();
        Condition = GetComponent<PlayerCondition>();
        Equipment = GetComponent<Equipment>();

        _stateMachine = new PlayerStateMachine(this);
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        _stateMachine.ChangeState(_stateMachine.IdleState);
        //Health.OnDie += OnDie;
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

    //private void OnDie()
    //{
    //    Animator.SetTrigger("Die");
    //    enabled = false;
    //}
}
