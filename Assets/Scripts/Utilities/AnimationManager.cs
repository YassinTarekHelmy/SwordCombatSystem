using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager
{
    public enum AnimationType {
        Idle,
        Walk,
        Run,
        Jump,
        Attack1,
        Attack2,
        Damage,
        Death,
        Locomotion,
        Dodge
    }

    public AnimationType CurrentAnimation { get; private set; }
    private Animator _animator;

    private Dictionary<AnimationType, string> _animationNames;

    public bool IsFinished { get => _animator.GetCurrentAnimatorStateInfo(0).length <= 0 || !_animator.GetCurrentAnimatorStateInfo(0).IsName(_animationNames[CurrentAnimation]); }
    
    public AnimationManager(Animator _animator, Dictionary<AnimationType, string> _animationNames)
    {
        this._animator = _animator;
        
        this._animationNames = _animationNames;
    }

    public void PlayAnimation(AnimationType animationType, float crossfadeTime) {
        CurrentAnimation = animationType;
            
        _animator.CrossFade(_animationNames[animationType], crossfadeTime);
    
    }

    public void PlayAnimation(AnimationType animationType) {
        CurrentAnimation = animationType;
            
        _animator.Play(_animationNames[animationType]);
    
    }

    public void StopCurrentAnimation() {
        _animator.StopPlayback();
    }

    public void SetLayerWeight(int layerIndex, float weight) {
        _animator.SetLayerWeight(layerIndex, weight);
    }

    public void SetFloat(string name, float value) {
        _animator.SetFloat(name, value);
    }

    public float GetFloat(string name) {
        return _animator.GetFloat(name);
    }
}
