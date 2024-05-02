using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyAnimatorController : EnemyAnimatorController
{
    [HideInInspector]
    public MeleeEnemyManager enemyManager;
    private bool _isDead = false;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Detect()
    {
        _animator.SetBool("isDetecting", true);
    }

    public void EndDetect()
    {
        _animator.SetBool("isDetecting", false);
    }

    public void Attack()
    {
        _animator.SetTrigger("Attack");
    }

    public void EndAttackAnim()
    {
        enemyManager.EndAttack();
    }

    public void DisableOnDeathAnim()
    {
        if (!_isDead)
        {
            enemyManager.DisableOnDeath();
            _isDead = true;
        }
    }
}
