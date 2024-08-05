using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FiniteStateMachine.Enemy.States
{
    public class EnemyRunState : EnemyMovementState
    {
        public EnemyRunState(EnemyStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            stateMachine.EntityMovement.AnimationManager.PlayAnimation(AnimationManager.AnimationType.Run, 0.2f);
            
            enemyMovement.NavMeshAgent.isStopped = false;
        }

        public override void Update()
        {
            base.Update();

            if (enemyMovement.NavMeshAgent.remainingDistance <= enemyMovement.EnemyAIData.attackRadius) {
                stateMachine.ChangeState(stateMachine.Attack);
                
            }
        }

        public override void Exit()
        {
            base.Exit();
            enemyMovement.NavMeshAgent.isStopped = true;

            stateMachine.EntityMovement.AnimationManager.StopCurrentAnimation();
        }

    }
}
