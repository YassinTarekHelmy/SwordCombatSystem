using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FiniteStateMachine.States {
    public class AttackStateFactory
    {

        public static float GetAnimationTime(int index) {
            return index switch
            {
                0 => 0.733f,
                1 => 0.617f,
                _ => 0,
            };
        }

        public static float GetAnimationTime(AnimationManager.AnimationType animationType) {
            return animationType switch
            {
                AnimationManager.AnimationType.Damage => 0.483f,
                AnimationManager.AnimationType.Dodge => 0.5f,
                _ => 0,
            };
        }
            
    }

}