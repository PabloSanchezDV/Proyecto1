using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MosquitoAnimatorController : EnemyAnimatorController
{
    [SerializeField] private float _timeToRespawn;
    [SerializeField] private ParticleSystemManager _particleSystemManager;
    private Coroutine _antiStuckCoroutine;

    public void DisableOnDeath()
    {
        if( _antiStuckCoroutine != null )
            StopCoroutine(_antiStuckCoroutine);
        _particleSystemManager.PlayParticleSystem();
        SetComponentsAs(false);
        StartCoroutine(RespawnAfter());
    }

    public void PlayDeathAfter()
    {
        _antiStuckCoroutine = StartCoroutine(PlayDeathAfterCoroutine());
    }

    IEnumerator PlayDeathAfterCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        Death();
    }


    IEnumerator RespawnAfter()
    {
        yield return new WaitForSeconds(_timeToRespawn);
        SetComponentsAs(true);
    }

    private void SetComponentsAs(bool setAs)
    {
        _animator.enabled = setAs;
        Collider collider = GetComponent<Collider>();
        transform.GetChild(0).gameObject.SetActive(setAs);
        collider.enabled = setAs;
    }

    protected override void PlayDeathSound()
    {
        AudioManager.instance.PlayMosquitoDeath(gameObject);
    }
}
