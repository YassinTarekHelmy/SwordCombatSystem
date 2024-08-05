using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMovementDataScriptableObject : ScriptableObject
{
    [Header("Movement")]
    public float MovementSpeed;

    [Header("Rotation")]
    public float RotationSpeed;
    public float TurnSmoothTime;
}
