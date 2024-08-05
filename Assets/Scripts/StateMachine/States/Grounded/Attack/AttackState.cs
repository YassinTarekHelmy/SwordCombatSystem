using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;


namespace FiniteStateMachine.States {
    public class AttackState : GroundedState
    {
        protected float attackStartTime;
        
        public Collider enemyCollider;
        public float[] ATTACK_LENGTH = { 0.933f, 0.817f};

        public static int attackIndex = -1;
        public AttackState(MovementStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            attackIndex = attackIndex == 1 ? 0 : attackIndex + 1;

            playerMovement.MovementData.movementMultiplier = 0f;
            RotateTowardsMovementDirection = false;

            attackStartTime = Time.time;

            AnimationManager.AnimationType animationType = attackIndex == 0 ? AnimationManager.AnimationType.Attack1 : AnimationManager.AnimationType.Attack2;

            stateMachine.EntityMovement.AnimationManager.PlayAnimation(animationType, 0.2f);
            
            enemyCollider = stateMachine.EntityMovement.GetComponent<EnemySensor>().CurrentEnemy;

            Vector3 tweenDirection = enemyCollider == null ? 
                                    playerMovement.transform.position + 
                                    playerMovement.transform.forward *
                                    playerMovement.MovementData.DashValue
                                    :
                                    enemyCollider.transform.position - (enemyCollider.transform.position - playerMovement.transform.position).normalized * 1f;
            

            stateMachine.EntityMovement.Tweener.TweenTo(playerMovement.transform, tweenDirection, .5f);
        
        }

        public override void Update() {
            base.Update();
            
            if (enemyCollider == null) {
                stateMachine.ChangeState(stateMachine.IdleState);
                return;
            }

            Vector3 lookDirection =   (enemyCollider.transform.position - playerMovement.transform.position).normalized;
            playerMovement.transform.rotation = Quaternion.Slerp(playerMovement.transform.rotation,Quaternion.LookRotation(lookDirection), 0.14f);

            if (Time.time > attackStartTime + ATTACK_LENGTH[attackIndex]) {
                attackIndex = -1;

                stateMachine.ChangeState(stateMachine.IdleState);
            }

        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
        }
    }
}