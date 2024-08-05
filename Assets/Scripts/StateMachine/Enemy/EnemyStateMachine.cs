using System;
using System.Collections;
using System.Collections.Generic;
using FiniteStateMachine.Enemy.States;
using UnityEngine;

namespace FiniteStateMachine.Enemy{
    public class EnemyStateMachine : StateMachine
    {
        public EnemySensor enemySensor;
        public EnemyIdleState Idle { get; private set; }
        public EnemyRunState Run { get; private set; }
        public EnemyDamagedState Damaged { get; private set; }
        public EnemyCirculatingState Circulating { get; private set; }
        public EnemyAttackState Attack { get; private set; }
        public EnemyDeathState Death { get; private set; }

        public EnemyStateMachine(EnemyMovement enemyMovement) : base(enemyMovement)
        {
            enemySensor = enemyMovement.GetComponent<EnemySensor>();

            enemyMovement.HealthBar.OnDeath += OnDeathPerformed;
            
            Idle = new(this);
            Damaged = new(this);
            Run = new(this);
            Circulating = new(this);
            Attack = new(this);
            Death = new(this);

            _stateTransitionDictionary.Add(Idle, new List<Transition> {
                new Transition{
                    _from = Idle,
                    _to = Circulating,
                    _condition =  () => enemySensor.CurrentEnemy != null
                }
            });

            _stateTransitionDictionary.Add(Damaged, new List<Transition>());
            _stateTransitionDictionary.Add(Circulating, new List<Transition>());
            _stateTransitionDictionary.Add(Attack, new List<Transition>());
            _stateTransitionDictionary.Add(Death, new List<Transition>());

            _stateTransitionDictionary.Add(Run, new List<Transition>{
                new Transition{
                    _from = Run,
                    _to = Idle,
                    _condition =  () => enemySensor.CurrentEnemy == null
                }
            });
        }

        private void OnDeathPerformed()
        {
            ChangeState(Death);
        }
    }
}
