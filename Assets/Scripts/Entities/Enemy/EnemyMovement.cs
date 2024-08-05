using System;
using System.Collections;
using System.Collections.Generic;
using FiniteStateMachine.Enemy;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : EntityMovement
{
    [Header("Enemy Specific Data")]
    [SerializeField] private EnemyAIData _enemyAIData;
    [SerializeField] private WorldSpaceUI _attackIndicator;

    private NavMeshAgent _navMeshAgent;

    public NavMeshAgent NavMeshAgent { get => _navMeshAgent; }
    public EnemyAIData EnemyAIData { get => _enemyAIData; }

    public WorldSpaceUI AttackIndicator { get => _attackIndicator; }
    public EnemyStateMachine EnemyStateMachine { get => (EnemyStateMachine)stateMachine; }

    private void Awake()
    {
        tweener = GetComponent<Tweener>();

        stateMachine = new EnemyStateMachine(this);
        
        animationData = new EnemyAnimationData();

        _navMeshAgent = GetComponent<NavMeshAgent>();

        Dictionary<AnimationManager.AnimationType, string> _animationNames = new Dictionary<AnimationManager.AnimationType, string>{
            {AnimationManager.AnimationType.Idle, animationData.Idle},
            {AnimationManager.AnimationType.Walk, animationData.Walk},
            {AnimationManager.AnimationType.Run, animationData.Run},
            {AnimationManager.AnimationType.Jump, animationData.Jump},
            {AnimationManager.AnimationType.Attack1, animationData.Attack1},
            {AnimationManager.AnimationType.Damage, animationData.Damage},
            {AnimationManager.AnimationType.Death, animationData.Death},
            {AnimationManager.AnimationType.Locomotion, animationData.Locomotion}
        };

        animationManager = new AnimationManager(animator, _animationNames);


    }

    private void Start() {
        stateMachine.Initialize(((EnemyStateMachine)stateMachine).Idle);
    }

    private void Update()
    {
        stateMachine?.Update();
    }

    private void FixedUpdate()
    {
        stateMachine?.FixedUpdate();
    }
}
