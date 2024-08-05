
using UnityEngine;

namespace FiniteStateMachine.States {
    public class DodgeState : GroundedState
    {
        private float _startTime = 0f;
        public DodgeState(MovementStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();    
            if (playerMovement.AttackSensor.CurrentEnemy == null)
            {
                stateMachine.ChangeState(stateMachine.IdleState);
                return;
            }

            _startTime = Time.time;

            playerMovement.AnimationManager.PlayAnimation(AnimationManager.AnimationType.Dodge, 0.5f);

            
            Vector3 dodgeDirection = (playerMovement.transform.position - playerMovement.AttackSensor.CurrentEnemy.transform.position).normalized;
            
            dodgeDirection = new Vector3(dodgeDirection.x, 0, dodgeDirection.z);
            
            playerMovement.transform.rotation = Quaternion.LookRotation(-dodgeDirection);

            stateMachine.EntityMovement.Tweener.TweenTo(playerMovement.transform,playerMovement.transform.position + dodgeDirection * 3f, 0.5f);
        }

        public override void Update()
        {
            base.Update();

            if (Time.time > _startTime + AttackStateFactory.GetAnimationTime(AnimationManager.AnimationType.Dodge)) {

                stateMachine.ChangeState(stateMachine.IdleState);
            }

        }
    }
}