using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAnimatorController : MonoBehaviour
{
    protected Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Stun()
    {
        _animator.SetTrigger("Stun");
    }

    public void Death()
    {
        PlayDeathSound();
        _animator.SetTrigger("Death");
        _animator.ResetTrigger("Stun");
    }

    protected abstract void PlayDeathSound();
}
