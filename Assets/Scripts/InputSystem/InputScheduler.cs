using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns {
    public class InputScheduler : MonoBehaviour
    {
        [SerializeField] private int _maxNumberOfCommands;
        private int _currentNumberOfCommands = 0;

        private ICommand _currentCommand;
        private Queue<ICommand> _queueOfCommands = new Queue<ICommand>();

        public event Action OnCommandQueued = delegate { };
        public event Action OnCommandStarted = delegate { };
        public event Action OnCommandFinished = delegate { };
        
        public void AddCommand(ICommand command) {
            if (_currentNumberOfCommands >= _maxNumberOfCommands) {
                return;
            }

            _currentNumberOfCommands++;
            _queueOfCommands.Enqueue(command);

            Debug.Log("Enqueued a command");
            OnCommandQueued?.Invoke();
        }

        private void Update() {
            if (_currentNumberOfCommands > 0) {
                // If _currentCommand is null, this is the first time or the previous command has finished.
                if (_currentCommand == null || _currentCommand.IsFinished) {
                    if (_currentCommand != null) 
                        OnCommandStarted?.Invoke();
                    
                    // Dequeue the next command if available
                    if (_queueOfCommands.Count > 0) {
                        _currentCommand = _queueOfCommands.Dequeue();

                        _currentNumberOfCommands--;
                    } else {
                        _currentCommand = null;
                    }
                }
                if (_currentCommand != null && !_currentCommand.IsRunning) {
                    _currentCommand.Execute();
                    
                    OnCommandFinished?.Invoke();
                    
                }
            }
        }
    }
}