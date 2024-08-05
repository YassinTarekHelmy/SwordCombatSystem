using System.Collections;
using System.Collections.Generic;
using FiniteStateMachine.States;
using UnityEngine;

namespace FiniteStateMachine.Enemy.States {
    public class EnemyDamagedState : EnemyMovementState
    {
        public float startTime;
        public EnemyDamagedState(EnemyStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            startTime = Time.time;

            if (stateMachine.enemySensor.CurrentEnemy != null) {
                enemyMovement.transform.rotation = Quaternion.LookRotation((stateMachine.enemySensor.CurrentEnemy.transform.position - enemyMovement.transform.position).normalized);
                
                stateMachine.EntityMovement.Tweener.TweenTo(stateMachine.EntityMovement.transform, stateMachine.EntityMovement.transform.position + stateMachine.enemySensor.CurrentEnemy.transform.forward * stateMachine.EntityMovement.MovementData.DashValue, 0.2f);
            }

            enemyMovement.HealthBar.TakeDamage(30f);

            stateMachine.EntityMovement.AnimationManager.PlayAnimation(AnimationManager.AnimationType.Damage);
            enemyMovement.NavMeshAgent.isStopped = true;
        }

        public override void Update()
        {
            base.Update();


            if (Time.time > startTime + AttackStateFactory.GetAnimationTime(AnimationManager.AnimationType.Damage))
                stateMachine.ChangeState(stateMachine.Idle);

        }
        public override void Exit()
        {
            base.Exit();

            enemyMovement.AnimationManager.StopCurrentAnimation();
        }
    }
}
