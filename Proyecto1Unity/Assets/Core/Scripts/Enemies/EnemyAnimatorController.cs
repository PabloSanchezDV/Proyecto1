using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorController : MonoBehaviour
{
    [SerializeField] protected Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Stun()
    {
        Debug.Log("Stun");
        _animator.SetTrigger("Stun");
    }

    public void Death()
    {
        _animator.SetTrigger("Death");
        _animator.ResetTrigger("Stun");
    }
}
