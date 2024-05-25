using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleParticleSystemManager : MonoBehaviour
{
    [SerializeField] private float _disableAfterTime;
    private ParticleSystem[] _particleSystems;
    private Vector3 _originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        _originalPosition = transform.localPosition;
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

    public void PlayAndDettachParentWhilePlaying()
    {
        Transform parent = transform.parent;
        transform.parent = null;
        foreach (ParticleSystem p in _particleSystems)
        {
            p.gameObject.SetActive(true);
            p.Play();
        }
        StartCoroutine(DisableAndReattachParentAfterPlaying(parent));
    }

    IEnumerator DisableAndReattachParentAfterPlaying(Transform parent)
    {
        yield return new WaitForSeconds(_disableAfterTime);
        transform.parent = parent;
        transform.localPosition = _originalPosition;
        foreach (ParticleSystem p in _particleSystems)
        {
            p.Stop();
        }
    }
}
