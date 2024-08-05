using System.Collections;
using System.Collections.Generic;
using FiniteStateMachine;
using FiniteStateMachine.Enemy.States;
using UnityEngine;
using UnityEngine.AI;

public class EnemySword : MonoBehaviour
{
    [SerializeField] private EnemyMovement _enemyMovement;

    [SerializeField] private float _cooldown;
    private float _currentTime;
    private float _previousTime;

    private void Start() {
        _previousTime = 0f;
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            
            _currentTime = Time.time;

            if (_currentTime < _previousTime + _cooldown) 
                return;

            _previousTime = _currentTime;
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            
            if (_enemyMovement.StateMachine.CurrentState is EnemyAttackState) {

                player.StateMachine.ChangeState(((MovementStateMachine)player.StateMachine).DamagedState);
            }
        }   
    }
}
