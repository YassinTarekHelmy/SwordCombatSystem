using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Utilities;

namespace FiniteStateMachine.Enemy.States {
    public class EnemyCirculatingState : EnemyMovementState
    {
        public static bool isSomeOneAttacking = false;
        private float _randomTime = 0f;
        private float _randomAttackTime = 0f;
        private bool isReversed = false;

        private float _distanceFromTarget = 0f;

        private float circulatingRadius;

        
        private Vector3 _previousPosition;
        private Quaternion _previousRotation;
        public EnemyCirculatingState(EnemyStateMachine stateMachine) : base(stateMachine)
        {
            _randomTime = 0f;
            _randomAttackTime = Random.Range(enemyMovement.EnemyAIData.lowestAttackTime, enemyMovement.EnemyAIData.highestAttackTime);

        }


        public override void Enter()
        {
            base.Enter();

            enemyMovement.NavMeshAgent.isStopped = true;

            _previousPosition = enemyMovement.transform.position;
            _previousRotation = enemyMovement.transform.rotation;

            if (stateMachine.enemySensor.CurrentEnemy == null)
            {
                enemyMovement.AnimationManager.SetFloat("X", Mathf.Lerp(enemyMovement.AnimationManager.GetFloat("X"), 0f, 0.1f));
                enemyMovement.AnimationManager.SetFloat("Y", Mathf.Lerp(enemyMovement.AnimationManager.GetFloat("Y"), 0f, 0.1f));


                stateMachine.ChangeState(stateMachine.Idle);
                return;
            }

            _distanceFromTarget = Vector3.Distance(stateMachine.enemySensor.CurrentEnemy.transform.position,enemyMovement.transform.position);

            enemyMovement.AnimationManager.PlayAnimation(AnimationManager.AnimationType.Locomotion, 0.2f);

            circulatingRadius = Random.Range(enemyMovement.EnemyAIData.lowestCirculatingRadius, enemyMovement.EnemyAIData.highestCirculatingRadius);
        }

        public override void Update()
        {
            base.Update();

            if (stateMachine.enemySensor.CurrentEnemy == null)
            {
                enemyMovement.AnimationManager.SetFloat("X", Mathf.Lerp(enemyMovement.AnimationManager.GetFloat("X"), 0f, 0.1f));
                enemyMovement.AnimationManager.SetFloat("Y", Mathf.Lerp(enemyMovement.AnimationManager.GetFloat("Y"), 0f, 0.1f));

                stateMachine.ChangeState(stateMachine.Idle);
                return;
            }

            if (_randomAttackTime <= 0f) {
                _randomAttackTime = Random.Range(enemyMovement.EnemyAIData.lowestAttackTime, enemyMovement.EnemyAIData.highestAttackTime);

                stateMachine.ChangeState(stateMachine.Run);
                
                return;
            }

            if (!isSomeOneAttacking)
            {   
                _randomAttackTime -= Time.deltaTime;
            }

            Vector3 directionToTarget = (stateMachine.enemySensor.CurrentEnemy.transform.position - enemyMovement.transform.position).normalized;

            _distanceFromTarget = Vector3.Distance(stateMachine.enemySensor.CurrentEnemy.transform.position,enemyMovement.transform.position);

            if (_distanceFromTarget > circulatingRadius)
            {
                enemyMovement.NavMeshAgent.Move(enemyMovement.EnemyAIData.baseEnemyAIData.walkSpeed * Time.deltaTime * directionToTarget);
            }

            else if (_distanceFromTarget < circulatingRadius - 1f) 
            {
                enemyMovement.NavMeshAgent.Move(-enemyMovement.EnemyAIData.baseEnemyAIData.walkSpeed * Time.deltaTime * directionToTarget);
            
            } 

            else {            
                Vector3 normalDirection = isReversed? Vector3.Cross(Vector3.up,directionToTarget).normalized : Vector3.Cross(directionToTarget, Vector3.up).normalized;
                

                if (_randomTime <= 0)
                {
                    _randomTime = Random.Range(enemyMovement.EnemyAIData.lowestReverseTime, enemyMovement.EnemyAIData.highestReverseTime);                    
                    
                    isReversed = !isReversed;
                }
                else
                {
                    _randomTime -= Time.deltaTime;
                }


                enemyMovement.NavMeshAgent.Move(enemyMovement.EnemyAIData.baseEnemyAIData.walkSpeed * Time.deltaTime * normalDirection);

            }
            enemyMovement.transform.rotation = Quaternion.Slerp(enemyMovement.transform.rotation,Quaternion.LookRotation(directionToTarget),0.02f);
            
            Vector3 movementVector = _previousRotation * (enemyMovement.transform.position - _previousPosition) * 75;

            enemyMovement.AnimationManager.SetFloat("X", Mathf.Lerp(enemyMovement.AnimationManager.GetFloat("X"), movementVector.x, 0.1f));
            enemyMovement.AnimationManager.SetFloat("Y", Mathf.Lerp(enemyMovement.AnimationManager.GetFloat("Y"), movementVector.z, 0.1f));
            
            _previousPosition = enemyMovement.transform.position;
            _previousRotation = enemyMovement.transform.rotation;
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}