using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommand
{
    public bool IsRunning { get; set; }
    public bool IsFinished { get; set; }
    public void Execute(); 
}
