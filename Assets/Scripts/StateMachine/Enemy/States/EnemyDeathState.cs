
namespace FiniteStateMachine.Enemy.States {
    public class EnemyDeathState : EnemyMovementState
    {
        public EnemyDeathState(EnemyStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            stateMachine.EntityMovement.AnimationManager.PlayAnimation(AnimationManager.AnimationType.Death, 0.2f);

            enemyMovement.NavMeshAgent.isStopped = true;
            enemyMovement.IsDead = true;
        }
    }
}
