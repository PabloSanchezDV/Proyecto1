using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemManager : MonoBehaviour
{
    [SerializeField] private float _disableAfterTime;
    private ParticleSystem _particleSystem;

    // Start is called before the first frame update
    void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _particleSystem.Stop();
    }

    public void PlayParticleSystem()
    {
        _particleSystem.gameObject.SetActive(true);
        _particleSystem.Play();
        StartCoroutine(DisableParticleSystemAfterTime());
    }

    IEnumerator DisableParticleSystemAfterTime()
    {
        yield return new WaitForSeconds(_disableAfterTime);
        _particleSystem.Stop();
        _particleSystem.gameObject.SetActive(false);
    }
}
