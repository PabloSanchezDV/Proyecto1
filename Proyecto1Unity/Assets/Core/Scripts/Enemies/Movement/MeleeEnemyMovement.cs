using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemyMovement : MonoBehaviour
{
    [HideInInspector]
    public MeleeEnemyManager enemyManager;

    [SerializeField] private float _checkingTime = 0.25f;

    private NavMeshAgent _agent;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    public void Move()
    {
        StartCoroutine(GoToNewPosition());
        _agent.isStopped = false;
    }

    public void EndMove()
    {
        _agent.isStopped = true;
    }

    public void DisableNavMeshAgent()
    {
        _agent.enabled = false;
    }

    IEnumerator GoToNewPosition()
    {
        Vector3 newPosition = enemyManager.Player.transform.position;
        //newPosition -= newPosition.normalized * _distanceToStop;
        if(_agent.enabled)
            _agent.SetDestination(newPosition);
        yield return _checkingTime;
        if(enemyManager.IsMoving)
            StartCoroutine(GoToNewPosition());
    }
}
