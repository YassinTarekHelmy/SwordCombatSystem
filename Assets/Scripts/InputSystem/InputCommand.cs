using System;
using UnityEngine;

namespace DesignPatterns {
    public class InputCommand : ICommand
    {
        public bool IsRunning { get; set; } = false;
        public bool IsFinished { get; set; } = false;

        public Action InputAction;

        public InputCommand(Action inputAction)
        {
            InputAction = inputAction;
        }
        
        
        
        public virtual void Execute() {
            InputAction?.Invoke();
            Debug.Log("a normal Command has finished executing");
            
            EndAction();

        
        }

        protected void EndAction() {
            IsRunning = false;
            IsFinished = true;
        }
    }
}