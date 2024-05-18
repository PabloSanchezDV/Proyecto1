using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MosquitoAnimatorController : EnemyAnimatorController
{
    [SerializeField] private float _timeToRespawn;
    [SerializeField] private ParticleSystemManager _particleSystemManager;

    public void DisableOnDeath()
    {
        _particleSystemManager.PlayParticleSystem();
        SetComponentsAs(false);
        StartCoroutine(RespawnAfter());
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
