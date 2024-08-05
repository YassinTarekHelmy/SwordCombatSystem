using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FiniteStateMachine.States {
    public class WalkState : GroundedState
    {
        public WalkState(MovementStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            playerMovementData.movementMultiplier = .5f;
            RotateTowardsMovementDirection = true;            
            
            stateMachine.EntityMovement.AnimationManager.PlayAnimation(AnimationManager.AnimationType.Walk, 0.2f);

        }
    }
}