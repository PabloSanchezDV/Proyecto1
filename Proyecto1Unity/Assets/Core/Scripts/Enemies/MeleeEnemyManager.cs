using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyManager : MonoBehaviour
{
    private MeleeEnemyAI _AI;
    private MeleeEnemyAnimatorController _animatorController;
    private MeleeEnemyMovement _enemyMovement;

    [SerializeField] private float _despawnAfterTime;
    [SerializeField] private ParticleSystemManager _particleSystemManager;

    [HideInInspector]
    public Transform Player { get { return _AI.Player; } }

    public bool IsMoving { get; set; }

    void Start()
    {
        _AI = GetComponent<MeleeEnemyAI>();
        _enemyMovement = GetComponent<MeleeEnemyMovement>();
        _animatorController = GetComponent<MeleeEnemyAnimatorController>();

        _AI.enemyManager = this;
        _enemyMovement.enemyManager = this;
        _animatorController.enemyManager = this;
    }

    #region Communications
    public void Detect()
    {
        IsMoving = true;
        if (_animatorController != null)
            _animatorController.Detect();
        if(_enemyMovement != null )
            _enemyMovement.Move();
    }

    public void EndDetect()
    {
        IsMoving = false;
        if (_animatorController != null)
            _animatorController.EndDetect();
        if (_enemyMovement != null)
            _enemyMovement.EndMove();
    }

    public void ContinueMove()
    {
        IsMoving = true;
        if (_enemyMovement != null && _enemyMovement.enabled)
            _enemyMovement.Move();
    }

    public void Attack()
    {
        IsMoving = false;
        if (_animatorController != null)
            _animatorController.Attack();
    }

    public void EndAttack()
    {
        if (_AI != null)
            _AI.EndAttack();
    }

    public void DisableOnDeath()
    {
        _enemyMovement.DisableNavMeshAgent();
        _enemyMovement.enabled = false;
        _AI.enabled = false;
        enabled = false;
        StartCoroutine(DespawnAfterTime());
    }
    #endregion

    IEnumerator DespawnAfterTime()
    {
        yield return new WaitForSeconds(_despawnAfterTime - 0.5f);
        _particleSystemManager.PlayParticleSystem();
        yield return new WaitForSeconds(0.5f);
        _particleSystemManager.transform.parent = null;
        gameObject.SetActive(false);
    }
}
