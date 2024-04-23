using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class RangedEnemyMovement : MonoBehaviour
{
    [HideInInspector]
    public RangedEnemyManager enemyManager;
    
    [SerializeField] private float _checkingPositionAngle = 22.5f;
    [SerializeField] private float _distanceToStop = 0.25f;

    private NavMeshAgent _agent;
    private Vector3 _newPosition;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    public void Flee()
    {
        StartCoroutine(GoToNewPosition());
        _agent.isStopped = false;
    }

    IEnumerator GoToNewPosition()
    {
        NavMeshPath path = new NavMeshPath();
        int side = 0;
        do
        {
            yield return null;
            transform.Rotate(Vector3.up, side * _checkingPositionAngle);
            side = -(side + 1);

            Vector3 v = 1 * (transform.position - enemyManager.Player.position);
            _newPosition = transform.position + v;
            _newPosition.y = transform.position.y;
            _agent.CalculatePath(_newPosition, path);
        } while (path.status == NavMeshPathStatus.PathInvalid);

        _agent.SetDestination(_newPosition);
        StartCoroutine(CheckNewPosition());
    }

    IEnumerator CheckNewPosition()
    {
        yield return new WaitUntil(() => Vector3.Distance(_newPosition, transform.position) < _distanceToStop);
        _agent.isStopped = true;
    }
}
