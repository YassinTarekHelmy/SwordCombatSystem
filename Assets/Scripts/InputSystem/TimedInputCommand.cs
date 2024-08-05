using System;
using System.Collections;
using UnityEngine;
using Utilities;

namespace DesignPatterns {
    public class TimedInputCommand : InputCommand
    {
        private float _executionTime;
        public TimedInputCommand(float executionTime ,Action inputAction) : base(inputAction)
        {
            _executionTime = executionTime;
        }

        public override void Execute()
        {
            IsRunning = true;

            
            if (InputAction != null) {
                InputAction.Invoke();

                CoroutineManager.Instance.StartRemoteCoroutine(WaitForTime());
            } else {
                EndAction();
            }
        }

        private IEnumerator WaitForTime()
        {
            yield return new WaitForSeconds(_executionTime);
            
            EndAction();
        }
    }
}