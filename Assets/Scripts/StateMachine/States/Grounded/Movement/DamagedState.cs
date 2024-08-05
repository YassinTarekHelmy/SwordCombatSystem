using System.Collections;
using System.Collections.Generic;
using FiniteStateMachine;
using FiniteStateMachine.States;
using UnityEngine;

public class DamagedState : GroundedState
{
    private float _damageTime = 0f;
    private float _damageDuration = AttackStateFactory.GetAnimationTime(AnimationManager.AnimationType.Damage);

    public DamagedState(MovementStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        playerMovementData.movementMultiplier = 0f;
        RotateTowardsMovementDirection = false;

        _damageTime = Time.time;

        playerMovement.HealthBar.TakeDamage(30);

        stateMachine.EntityMovement.AnimationManager.PlayAnimation(AnimationManager.AnimationType.Damage, 0.2f);
    }

    public override void Update()
    {
        base.Update();

        if (Time.time > _damageTime + _damageDuration)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }
}
