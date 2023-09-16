using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KevinCastejon.HierarchicalFiniteStateMachine;
public class PlayerStateMachineComponent : MonoBehaviour
{
    [SerializeField] [Range(1, 10)] float MoveSpeed;
    [SerializeField] PlayerAnimation playerAnimation;

    private PlayerStateMachine _stateMachine;
    private void Awake()
    {
        _stateMachine = AbstractHierarchicalFiniteStateMachine.CreateRootStateMachine<PlayerStateMachine>("PlayerStateMachine");
        _stateMachine.rb = GetComponent<Rigidbody>();
        _stateMachine.moveSpeed = MoveSpeed;
        _stateMachine.transform = transform;
        _stateMachine.playerAnimation = playerAnimation;
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
}
