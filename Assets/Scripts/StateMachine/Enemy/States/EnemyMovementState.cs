using System.Collections;
using System.Collections.Generic;
using FiniteStateMachine.States;
using UnityEngine;
using UnityEngine.AI;

namespace FiniteStateMachine.Enemy.States {
    public class EnemyMovementState : IState
    {
        protected EnemyMovement enemyMovement;
        protected EnemyStateMachine stateMachine;
        
        public EnemyMovementState(EnemyStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;

            enemyMovement = (EnemyMovement)stateMachine.EntityMovement;
        }

        public virtual void Enter()
        {
            enemyMovement.NavMeshAgent.speed = enemyMovement.EnemyAIData.baseEnemyAIData.runSpeed;
        }

        public virtual void Exit()
        {
            //noop
        }

        public virtual void FixedUpdate()
        {
            //noop
        }

        public virtual void Update()
        {
            //noop

            enemyMovement.NavMeshAgent.SetDestination(enemyMovement.EnemyAIData.target.position);
        }
    }
}
