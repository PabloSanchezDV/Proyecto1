using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class RangedEnemyAI : MonoBehaviour
{
    [HideInInspector]
    public RangedEnemyManager enemyManager;

    [SerializeField] private GameObject _projectile;
    [SerializeField] private float _detectionRange;
    [SerializeField] private float _distanceCheckTime;
    [SerializeField] private float _fleeRange;
    public float FleeRange { get { return _fleeRange; } }

    private Transform _player;
    public Transform Player { get { return _player; } } 

    private Transform _shootingPoint;
    private bool _inAttackingRange;
    private bool _inFleeingRange;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _shootingPoint = transform.GetChild(0).GetChild(0).transform;
        StartCoroutine(CheckDistance());
    }

    IEnumerator CheckDistance()
    {
        yield return new WaitForSeconds(_distanceCheckTime);
        float distance = Vector3.Distance(_player.position, transform.position);

        if (distance <= _detectionRange)
        {
            if (!_inAttackingRange)
            {
                _inAttackingRange = true;
                enemyManager.Detect();
            }
        }
        else
        {
            if (_inAttackingRange)
            {
                _inAttackingRange = false;
                enemyManager.EndDetect();
            }
        }

        if (distance <= _fleeRange)
        {
            if (!_inFleeingRange)
            {
                _inFleeingRange = true;
                enemyManager.Flee();
            }
        }
        else
        {
            if (_inFleeingRange)
            {
                _inFleeingRange = false;
                enemyManager.EndFlee();
            }
        }

        if(_inAttackingRange && !_inFleeingRange)
        {
            Attack();
        }

        StartCoroutine(CheckDistance());
    }

    private void Attack()
    {
        enemyManager.Attack();
    }

    public void ShootProjectile()
    {
        Instantiate(_projectile, _shootingPoint.position, _shootingPoint.rotation);
    }
}
