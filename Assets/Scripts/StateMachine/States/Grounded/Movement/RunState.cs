

namespace FiniteStateMachine.States {
    public class RunState : GroundedState
    {
        public RunState(MovementStateMachine stateMachine) : base(stateMachine)
        {
            
        }

        public override void Enter()
        {
            base.Enter();

            playerMovementData.movementMultiplier = 1.5f;
            RotateTowardsMovementDirection = true;
            
            stateMachine.EntityMovement.AnimationManager.PlayAnimation(AnimationManager.AnimationType.Run, 0.2f);

        }
    }
}