using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data {
    [Serializable]
    public class FloatingCapsuleData
    {
        [Range(0,1)] public float capsuleHeight = 0.5f;
        [Range(0,1)] public float rayDisatance = 0.5f;
        public LayerMask groundLayer;
    }
}