using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyManager : MonoBehaviour
{
    private RangedEnemyAI _AI;
    private RangedEnemyAnimatorController _animatorController;
    private RangedEnemyMovement _enemyMovement;

    [SerializeField] private float _despawnAfterTime;

    [HideInInspector]
    public float FleeRange { get { return _AI.FleeRange; } }
    public Transform Player {  get { return _AI.Player; } }

    void Start()
    {
        _AI = GetComponent<RangedEnemyAI>();
        _enemyMovement = GetComponent<RangedEnemyMovement>();
        _animatorController = GetComponent<RangedEnemyAnimatorController>();

        _AI.enemyManager = this;
        _enemyMovement.enemyManager = this;
        _animatorController.enemyManager = this;
    }

    #region Communications
    public void Detect()
    {
        if(_animatorController != null)
            _animatorController.Detect();
    }

    public void EndDetect()
    {
        if (_animatorController != null)
            _animatorController.EndDetect();
    }

    public void Flee()
    {
        if(_animatorController != null)
            _animatorController.Flee();
    }

    public void StartMoving()
    {
        if(_enemyMovement != null)
            _enemyMovement.Flee();
    }

    public void EndFlee()
    {
        if(_animatorController != null)
            _animatorController.EndFlee();
    }

    public void Attack()
    {
        if(_animatorController != null)
            _animatorController.Attack();
    }

    public void DisableOnDeath()
    {
        _enemyMovement.enabled = false;
        _AI.enabled = false;
        enabled = false;
        StartCoroutine(DespawnAfterTime());
    }
    #endregion

    IEnumerator DespawnAfterTime()
    {
        yield return new WaitForSeconds(_despawnAfterTime);
        gameObject.SetActive(false);
    }
}
