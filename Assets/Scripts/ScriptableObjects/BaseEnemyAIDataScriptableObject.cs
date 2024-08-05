
using UnityEngine;

public class BaseEnemyAIDataScriptableObject : ScriptableObject
{
    [Header("Movement")]
    public float walkSpeed = 1.5f;
    public float runSpeed = 3.5f;
    public float rotationSpeed = 10f;

    [Header("Attack")]
    public float attackRange = 1.5f;
    public float attackRate = 1.5f;
    public float attackDamage = 10f;
}
