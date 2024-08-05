

namespace FiniteStateMachine.States {
    public class GroundedState : MovementState
    {
        
        public GroundedState(MovementStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

        }

        public override void Update()
        {
            base.Update();

            FloatCapsule();
        }

        private void FloatCapsule() {
        
        }
    }
}