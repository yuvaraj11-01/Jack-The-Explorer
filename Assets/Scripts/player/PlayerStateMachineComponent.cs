using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KevinCastejon.HierarchicalFiniteStateMachine;
public class PlayerStateMachineComponent : MonoBehaviour
{
    [SerializeField] [Range(1, 10)] float MoveSpeed;
    [SerializeField] PlayerAnimation playerAnimation;
    [SerializeField] Transform SpawnPoint;

    private PlayerStateMachine _stateMachine;
    private void Awake()
    {
        _stateMachine = AbstractHierarchicalFiniteStateMachine.CreateRootStateMachine<PlayerStateMachine>("PlayerStateMachine");
        _stateMachine.rb = GetComponent<Rigidbody>();
        _stateMachine.moveSpeed = MoveSpeed;
        _stateMachine.transform = transform;
        _stateMachine.playerAnimation = playerAnimation;
        _stateMachine.SpawnPoint = SpawnPoint;
    }
    private void Start()
    {
        _stateMachine.OnEnter();
    }
    private void Update()
    {
        _stateMachine.moveSpeed = MoveSpeed;
        _stateMachine.OnUpdate();
    }
    private void FixedUpdate()
    {
        _stateMachine.OnFixedUpdate();
    }

    public void ChangeDeadState()
    {
        _stateMachine.TransitionToState<PlayerStateMachine.PlayerState>(PlayerStateMachine.PlayerState.DEAD);
    }
}
