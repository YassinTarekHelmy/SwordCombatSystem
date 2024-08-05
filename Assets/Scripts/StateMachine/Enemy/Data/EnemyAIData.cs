using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyAIData
{
    public BaseEnemyAIDataScriptableObject baseEnemyAIData;
    public Transform target;
    [Header("Circulating Data")]

    public float lowestCirculatingRadius;
    public float highestCirculatingRadius;
    
    public float lowestReverseTime;
    public float highestReverseTime;

    public float lowestAttackTime;
    public float highestAttackTime;

    public float attackRadius; 
    
}
