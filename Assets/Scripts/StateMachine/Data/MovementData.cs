using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data {
    [Serializable]
    public class MovementData
    {
        public BaseMovementDataScriptableObject _baseMovementData ;
        public float movementMultiplier;
        public float rotationMultiplier;

        public float DashValue;
        public float MovementSpeed { get => _baseMovementData.MovementSpeed * movementMultiplier; } 
        public float RotationSpeed { get => _baseMovementData.RotationSpeed * rotationMultiplier; } 
    }
}