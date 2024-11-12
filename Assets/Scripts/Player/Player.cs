using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    [field: SerializeField] public PlayerSO Data { get; private set; }

    [field: Header("Animations")]
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }

    public Animator Animator { get; private set; }
    public CharacterController Controller { get; private set; }
    //public ForceReceiver ForceReceiver { get; private set; }
    public NavMeshAgent NavMeshAgent { get; private set; }

    private PlayerStateMachine _stateMachine;

    [field: SerializeField] public Weapon Weapon { get; private set; }

    //public Health Health { get; private set; }

    private void Awake()
    {
        AnimationData.Initialize();
        Animator = GetComponentInChildren<Animator>();
        Controller = GetComponent<CharacterController>();
        //ForceReceiver = GetComponent<ForceReceiver>();
        //Health = GetComponent<Health>();
        NavMeshAgent = GetComponent<NavMeshAgent>();

        _stateMachine = new PlayerStateMachine(this);
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
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
