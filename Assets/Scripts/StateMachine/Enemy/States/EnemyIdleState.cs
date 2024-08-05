using System.Collections;
using System.Collections.Generic;


namespace FiniteStateMachine.Enemy.States
{
    public class EnemyIdleState : EnemyMovementState
    {
        public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            stateMachine.EntityMovement.AnimationManager.PlayAnimation(AnimationManager.AnimationType.Idle, 0.2f);
            

            enemyMovement.NavMeshAgent.isStopped = true;
        }
    }
}