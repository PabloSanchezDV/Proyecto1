using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleParticleSystemManager : MonoBehaviour
{
    [SerializeField] private float _disableAfterTime;
    private ParticleSystem[] _particleSystems;

    // Start is called before the first frame update
    void Start()
    {
        _particleSystems = transform.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem p in _particleSystems)
        {
            p.Stop();
        }
    }

    public void PlayParticleSystem()
    {
        foreach(ParticleSystem p in _particleSystems)
        {
            p.gameObject.SetActive(true);
            p.Play();
        }
        StartCoroutine(DisableParticleSystemsAfterTime());
    }

    IEnumerator DisableParticleSystemsAfterTime()
    {
        yield return new WaitForSeconds(_disableAfterTime);
        foreach (ParticleSystem p in _particleSystems)
        {
            p.Stop();
            p.gameObject.SetActive(false);
        }
    }
}
