using System;
using System.Collections;
using System.Collections.Generic;
using FiniteStateMachine;
using UnityEngine;

public class PlayerMovement : EntityMovement
{
    [Header("Player Specific Data")]
    [SerializeField] private AnimationTrigger _animationTrigger;
    [SerializeField] private EnemySensor _attackSensor;
    [SerializeField] public float dodgeDistance;

    private Vector3 _rotatedDirection;
    
    public Vector3 RotatedDirection { get => _rotatedDirection; }
    public EnemySensor AttackSensor { get => _attackSensor; }
    public MovementStateMachine MovementStateMachine { get => (MovementStateMachine)stateMachine; }
    public PlayerAnimationData PlayerAnimationData { get => (PlayerAnimationData)animationData; }
    public AnimationTrigger AnimationTrigger { get => _animationTrigger; }

    private void Awake() {
        
        tweener = GetComponent<Tweener>();

        stateMachine = new MovementStateMachine(this);

        animationData = new PlayerAnimationData();

        Dictionary<AnimationManager.AnimationType, string> _animationNames = new Dictionary<AnimationManager.AnimationType, string>{
            {AnimationManager.AnimationType.Idle, animationData.Idle},
            {AnimationManager.AnimationType.Walk, animationData.Walk},
            {AnimationManager.AnimationType.Run, animationData.Run},
            {AnimationManager.AnimationType.Jump, animationData.Jump},
            {AnimationManager.AnimationType.Attack1, PlayerAnimationData.Attack1},
            {AnimationManager.AnimationType.Attack2, PlayerAnimationData.Attack2},
            {AnimationManager.AnimationType.Dodge, PlayerAnimationData.Dodge},
            {AnimationManager.AnimationType.Damage, PlayerAnimationData.Damage},
            {AnimationManager.AnimationType.Death, animationData.Death},
            {AnimationManager.AnimationType.Locomotion, animationData.Locomotion}
        };


        animationManager = new AnimationManager(animator,_animationNames);
    }

    private void Start()
    {
        stateMachine.Initialize(MovementStateMachine.IdleState);
    }



    // Update is called once per frame
    private void Update()
    {
        stateMachine?.Update();
    }

    private void FixedUpdate() {
        stateMachine?.FixedUpdate();
    }
    

    public void SetRotatedDirection(Vector3 rotatedDirection) {
        _rotatedDirection = rotatedDirection;
    }
}
