using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DesignPatterns;
using FiniteStateMachine.Enemy.States;
using InputSystem;
using UnityEngine;

public class EnemySensor : MonoBehaviour
{
    public enum FilterType {
        Closest,
        CurrentlyAttacking
    }

    [Header("Detection")]
    [SerializeField] private float _detectionRadius = 5f;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private bool _useInput;

    [SerializeField] private FilterType _filterType;
    private bool _isStopped = false;

    [Header("References")]
    [SerializeField] private InputScheduler _inputScheduler;

    private Collider _currentEnemy;

    public Collider CurrentEnemy => _currentEnemy;

    private void Awake() {
        _inputScheduler.OnCommandStarted += StopSensor;
        _inputScheduler.OnCommandFinished += StartSensor;
    }

    private void StartSensor()
    {
        _isStopped = false;
    }

    private void StopSensor()
    {
        _isStopped = true;
    }

    private void OnDestroy() {
        _inputScheduler.OnCommandStarted -= StopSensor;
        _inputScheduler.OnCommandFinished -= StartSensor;
    }

    private void Update()
    {
        if (_isStopped)
            return;

        Collider[] colliders = Physics.OverlapSphere(transform.position, _detectionRadius, _layerMask);


        Vector3 from = _useInput ? GetComponent<PlayerMovement>().RotatedDirection : transform.forward;

        switch (_filterType) {
            case FilterType.Closest:
                _currentEnemy = colliders.
                Where(collider => collider.TryGetComponent<EntityMovement>(out var entityMovement) && !entityMovement.IsDead).
                OrderBy(collider => Vector3.Angle(from.normalized, (collider.transform.position - transform.position).normalized)).FirstOrDefault();
                break;
            case FilterType.CurrentlyAttacking:
                _currentEnemy = colliders.
                Where(collider => collider.TryGetComponent<EntityMovement>(out var entityMovement) && !entityMovement.IsDead).
                OrderBy(Collider => Collider.TryGetComponent<EnemyMovement>(out var enemyMovement) && enemyMovement.StateMachine.CurrentState is EnemyAttackState).FirstOrDefault();
                break;
        }
        

    
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _detectionRadius);
    }
}
