using System.Collections;
using System.Collections.Generic;
using InputSystem;
using UnityEngine;

namespace FiniteStateMachine.States {
    public class DeathState : GroundedState
    {
        public DeathState(MovementStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            playerMovement.AnimationManager.PlayAnimation(AnimationManager.AnimationType.Death, 0.2f);

            InputManager.Instance.gameObject.SetActive(false);

            playerMovement.IsDead = true;
        }
    }
}
