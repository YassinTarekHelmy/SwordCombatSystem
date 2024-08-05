using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    public event Action<float> OnAttackPerformed;
    
    private void AttackPerformed(float damage) {
        OnAttackPerformed?.Invoke(damage);
    }
}
