using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using DesignPatterns;
using FiniteStateMachine.States;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputSystem {
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance { get; private set; }
        [SerializeField] private InputScheduler _inputScheduler;

        private InputActions _inputActions;
        private Vector2 _movementInput;

        public Vector2 MovementInput { get =>  _movementInput; }

        public event Action JumpEvent = delegate { };
        public event Action AttackEvent = delegate {};
        public event Action DodgeEvent = delegate {};
        public event Action ScreenCaptureEvent = delegate {};

        public bool IsRunning { get; private set; }

        private void Awake() {
            _inputActions = new InputActions();

            if (Instance != null && Instance != this) {
                Destroy(gameObject);
            } else {
                Instance = this;
            }

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void OnEnable() {
            _inputActions.Enable();

            _inputActions.Player.Move.performed += OnMovementPerformed;
            _inputActions.Player.Move.canceled += OnMovementPerformed;

            _inputActions.Player.Run.performed += OnRunPerformed;
            _inputActions.Player.Run.canceled += OnRunPerformed;

            _inputActions.Player.Attack.performed += OnAttackPerformed;

            _inputActions.Player.Jump.performed += OnJumpPerformed;

            _inputActions.Player.Dodge.performed += OnDodgePerformed;

            _inputActions.Screen.Capture.performed += OnScreenCapturePerformed;
        }

        private void OnScreenCapturePerformed(InputAction.CallbackContext context)
        {
            ScreenCaptureEvent?.Invoke();
        }

        private void OnDodgePerformed(InputAction.CallbackContext context)
        {
            float attackTime = AttackStateFactory.GetAnimationTime(AnimationManager.AnimationType.Dodge);
            InputCommand timedInputCommand = new TimedInputCommand(attackTime, DodgeEvent);

            _inputScheduler.AddCommand(timedInputCommand);
        }

        private void OnAttackPerformed(InputAction.CallbackContext context)
        {
       
            float attackTime = AttackStateFactory.GetAnimationTime(AttackState.attackIndex + 1);
            InputCommand timedInputCommand = new TimedInputCommand(attackTime, AttackEvent);

            _inputScheduler.AddCommand(timedInputCommand);
        }

        private void OnRunPerformed(InputAction.CallbackContext context)
        {
            if (context.performed) {
                IsRunning = true;
            } else if (context.canceled) {
                IsRunning = false;
            }
        }

        private void OnJumpPerformed(InputAction.CallbackContext context)
        {
            InputCommand jumpCommand = new InputCommand(JumpEvent);

            _inputScheduler.AddCommand(jumpCommand);
        }

        private void OnMovementPerformed(InputAction.CallbackContext context) {
            if (context.performed) {
                _movementInput = context.ReadValue<Vector2>();
            } else if (context.canceled) {
                _movementInput = Vector2.zero;
            }
        }

        private void OnDisable() {
            _inputActions.Disable();

            _inputActions.Player.Move.performed -= OnMovementPerformed;
            _inputActions.Player.Move.canceled -= OnMovementPerformed;

            _inputActions.Player.Run.performed -= OnRunPerformed;
            _inputActions.Player.Run.canceled -= OnRunPerformed;

            _inputActions.Player.Attack.performed -= OnAttackPerformed;

            _inputActions.Player.Jump.performed -= OnJumpPerformed;

            _inputActions.Player.Dodge.performed -= OnDodgePerformed;

            _inputActions.Screen.Capture.performed -= OnScreenCapturePerformed;
        }
    }
}