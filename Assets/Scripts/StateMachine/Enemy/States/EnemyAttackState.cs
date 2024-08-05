using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FiniteStateMachine.Enemy.States
{   
    public class EnemyAttackState : EnemyMovementState
    {
        private float _attackStartTime;
        private const float ATTACK_LENGTH = 0.933f;
        public EnemyAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            enemyMovement.AttackIndicator.ShowIndicator();

            EnemyCirculatingState.isSomeOneAttacking = true;

            _attackStartTime = Time.time;

            stateMachine.EntityMovement.AnimationManager.PlayAnimation(AnimationManager.AnimationType.Attack1, 0.2f);
        }

        public override void Update()
        {
            base.Update();

            if (Time.time > _attackStartTime + ATTACK_LENGTH) {
                stateMachine.EntityMovement.AnimationManager.PlayAnimation(AnimationManager.AnimationType.Idle, 0.2f);
                
                EnemyCirculatingState.isSomeOneAttacking = false;
                
                stateMachine.ChangeState(stateMachine.Circulating);
            }
        }

        public override void Exit()
        {
            base.Exit();

            enemyMovement.AttackIndicator.HideIndicator();
        }
    }
}