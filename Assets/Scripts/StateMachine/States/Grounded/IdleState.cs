using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FiniteStateMachine.States {
    public class IdleState : GroundedState
    {
        public IdleState(MovementStateMachine stateMachine) : base(stateMachine)
        {
            
        }

        public override void Enter()
        {
            base.Enter();

            playerMovementData.movementMultiplier = 0f;
            RotateTowardsMovementDirection = false;

            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            
            stateMachine.EntityMovement.AnimationManager.PlayAnimation(AnimationManager.AnimationType.Idle, 0.2f);

        }
    }
}