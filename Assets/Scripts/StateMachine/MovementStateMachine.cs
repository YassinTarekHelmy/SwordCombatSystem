using System;
using System.Collections;
using System.Collections.Generic;
using FiniteStateMachine.Enemy;
using FiniteStateMachine.Enemy.States;
using FiniteStateMachine.States;
using InputSystem;
using UnityEngine;

namespace FiniteStateMachine {
    public class MovementStateMachine : StateMachine
    {
        public WalkState WalkState { get; private set; }
        public RunState RunState { get; private set; }
        public IdleState IdleState { get; private set; }
        public DamagedState DamagedState { get; private set; }

        public AttackState AttackState { get; private set; }
        public DodgeState DodgeState { get; private set; }
        public DeathState DeathState { get; private set; }

        public AttackStateFactory attackStateFactory;

        public EnemyStateMachine enemyStateMachine;

        public MovementStateMachine(PlayerMovement playerMovement) : base(playerMovement) {
            
            WalkState = new(this);
            RunState = new(this);
            IdleState = new(this);

            DamagedState = new(this);

            AttackState = new(this);

            DodgeState = new(this);

            DeathState = new(this);
            
            attackStateFactory = new();

            InputManager.Instance.AttackEvent += OnAttackPerformed;
            InputManager.Instance.DodgeEvent += OnDodgePerformed;
            playerMovement.HealthBar.OnDeath += OnDeathPerformed;

            playerMovement.AnimationTrigger.OnAttackPerformed += AttackConsequence;

            _stateTransitionDictionary.Add(IdleState, new List<Transition> {
                new Transition {
                    _from = IdleState,
                    _to = RunState,
                    _condition = () => InputManager.Instance.MovementInput != Vector2.zero && InputManager.Instance.IsRunning
                },
                new Transition {
                    _from = IdleState,
                    _to = WalkState,
                    _condition = () => InputManager.Instance.MovementInput != Vector2.zero
                }
            });

            _stateTransitionDictionary.Add(WalkState, new List<Transition> {
                new Transition {
                    _from = WalkState,
                    _to = IdleState,
                    _condition = () => InputManager.Instance.MovementInput == Vector2.zero
                },
                new Transition {
                    _from = IdleState,
                    _to = RunState,
                    _condition = () => InputManager.Instance.MovementInput != Vector2.zero && InputManager.Instance.IsRunning
                }
            });

            _stateTransitionDictionary.Add(RunState, new List<Transition> {
                new Transition {
                    _from = RunState,
                    _to = IdleState,
                    _condition = () => InputManager.Instance.MovementInput == Vector2.zero
                },
                new Transition {
                    _from = RunState,
                    _to = WalkState,
                    _condition = () => InputManager.Instance.MovementInput != Vector2.zero && !InputManager.Instance.IsRunning
                }
            });

            _stateTransitionDictionary.Add(AttackState, new List<Transition>());
            _stateTransitionDictionary.Add(DamagedState, new List<Transition>());
            _stateTransitionDictionary.Add(DodgeState, new List<Transition>());
            _stateTransitionDictionary.Add(DeathState, new List<Transition>());
        }

        private void OnDeathPerformed()
        {
            ChangeState(DeathState);

            UIManager.Instance.ShowDeathScreen();
        }

        private void OnDodgePerformed()
        {
            if (((PlayerMovement)entityMovement).AttackSensor.CurrentEnemy == null)
                return;
            
            ChangeState(DodgeState);
        }

        private void OnAttackPerformed()
        {
            ChangeState(AttackState);
        }

        private void AttackConsequence(float damage) {
            EnemySensor enemySensor = AttackState.enemyCollider.GetComponent<EnemySensor>();

            if (enemySensor.CurrentEnemy == null) return;

            enemyStateMachine = enemySensor.GetComponent<EnemyMovement>().EnemyStateMachine;
            //apply damage to enemy
            enemyStateMachine.ChangeState(enemyStateMachine.Damaged);
        }
    }
}