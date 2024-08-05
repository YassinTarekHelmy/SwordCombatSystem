using System.Collections;
using System.Collections.Generic;
using Data;
using InputSystem;
using UnityEngine;

namespace FiniteStateMachine.States {
    public class MovementState : IState
    {
        protected PlayerMovement playerMovement;
        protected MovementStateMachine stateMachine;
        protected MovementData playerMovementData;
        protected bool RotateTowardsMovementDirection = true;

        protected Rigidbody _rigidbody;
        
        private Vector3 _movementDirection;
        private float _turnSmoothVelocity;

        private float _rotationAngle;


        public MovementState(MovementStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;

            playerMovement = (PlayerMovement)stateMachine.EntityMovement;
            
            _rigidbody = playerMovement.GetComponent<Rigidbody>();

            playerMovementData = playerMovement.MovementData;
        }
        public virtual void Enter()
        {
            Debug.Log("Entered: " + GetType().Name);
        }

        public virtual void Exit()
        {
            //noop
        }

        public virtual void FixedUpdate()
        {
            HandleMovement();
        }

        public virtual void Update()
        {
            HandleRotation();
        }

        private void HandleMovement() {
            
            _movementDirection = new Vector3(InputManager.Instance.MovementInput.x, 0, InputManager.Instance.MovementInput.y).normalized;

            if (_movementDirection == Vector3.zero) {
                return;
            }

            Vector3 rotatedDirection = Quaternion.Euler(0, _rotationAngle, 0) * Vector3.forward;

            playerMovement.SetRotatedDirection(rotatedDirection);

            _rigidbody.AddForce(rotatedDirection * playerMovement.MovementData.MovementSpeed - GetHorizontalVelocity(), ForceMode.VelocityChange);
        }

        private void HandleRotation() {
            if (_movementDirection != Vector3.zero) {
                float directionAngle = Mathf.Atan2(_movementDirection.x, _movementDirection.z) * Mathf.Rad2Deg;

                if (directionAngle < 0) {
                    directionAngle += 360;
                }

                directionAngle += Camera.main.transform.eulerAngles.y;

                if (directionAngle >= 360) {
                    directionAngle -= 360;
                }

                _rotationAngle = directionAngle;

                if (RotateTowardsMovementDirection) {
                    float smoothedDirectionAngle = Mathf.SmoothDampAngle(_rigidbody.rotation.eulerAngles.y, directionAngle, ref _turnSmoothVelocity, playerMovement.MovementData._baseMovementData.TurnSmoothTime);

                    Quaternion smoothedRotation = Quaternion.Euler(0, smoothedDirectionAngle, 0);

                    _rigidbody.MoveRotation(smoothedRotation);
                }
            }
        }

#region  Reusable
        public Vector3 GetHorizontalVelocity() {
            return new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);
        }

#endregion
    }
}