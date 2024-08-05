using System.Collections;
using System.Collections.Generic;
using Data;
using FiniteStateMachine;
using UnityEngine;

public abstract class EntityMovement : MonoBehaviour
{
    protected StateMachine stateMachine;
    protected AnimationManager animationManager;
    public bool IsDead = false;
    protected Tweener tweener;

    [Header("Entity Data")]
    [SerializeField] private MovementData _movementData;
    [SerializeField] private FloatingCapsuleData _floatingCapsuleData;
    protected AnimationData animationData;

    
    [Header("References")]
    [SerializeField] protected Animator animator;
    [SerializeField] protected HealthBar healthBar;
    
    public HealthBar HealthBar { get => healthBar; }
    public MovementData MovementData { get => _movementData; }
    public FloatingCapsuleData FloatingCapsuleData { get => _floatingCapsuleData; }
    public Tweener Tweener { get => tweener; }
    public StateMachine StateMachine { get => stateMachine; }
    public AnimationManager AnimationManager { get => animationManager; }
    public AnimationData AnimationData { get => animationData; }
}
