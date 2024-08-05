using System.Collections.Generic;
using System;

using FiniteStateMachine.States;

namespace FiniteStateMachine {
    public class StateMachine
    {
        protected class Transition {
            public IState _from;
            public IState _to;
            public Func<bool> _condition;

            public bool Evaluate() {
                return _condition();
            }
        }
        protected EntityMovement entityMovement;
        protected Dictionary<IState,List<Transition>> _stateTransitionDictionary = new();
        private IState _currentState;

        public IState CurrentState { get => _currentState; }
        public EntityMovement EntityMovement { get => entityMovement; }

        protected StateMachine(EntityMovement entityMovement) {
            this.entityMovement = entityMovement;
        }

        public void Update() {
            AutomateTransition();
            _currentState?.Update();
        } 

        public void FixedUpdate() {
            _currentState?.FixedUpdate();
        }

        public void ChangeState(IState newState) {
            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
        }

        public void Initialize(IState State) {
            ChangeState(State);
        }

        private void AutomateTransition() {
            foreach (var transition in _stateTransitionDictionary[_currentState]) {
                if (transition.Evaluate()) {
                    ChangeState(transition._to);
                    break;
                }
            }
        }

    }
}