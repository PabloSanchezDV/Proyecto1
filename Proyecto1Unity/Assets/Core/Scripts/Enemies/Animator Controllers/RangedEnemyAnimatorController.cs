using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyAnimatorController : EnemyAnimatorController
{
    [HideInInspector]
    public RangedEnemyManager enemyManager;
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

    public void Flee()
    {
        _animator.SetBool("isFleeing", true);
    }

    public void EndFlee()
    {
        _animator.SetBool("isFleeing", false);
    }

    public void Attack()
    {
        _animator.SetTrigger("Attack");
    }

    public void DisableOnDeathAnim()
    {
        if(!_isDead)
        {
            enemyManager.DisableOnDeath();
            _isDead = true;
        }
    }

    protected override void PlayDeathSound()
    {
        AudioManager.instance.PlayCobraDeath(gameObject);
    }
}
