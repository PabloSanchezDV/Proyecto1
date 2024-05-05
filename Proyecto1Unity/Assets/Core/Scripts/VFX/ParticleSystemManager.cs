using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemManager : MonoBehaviour
{
    [SerializeField] private float _disableAfterTime;
    private ParticleSystem _particleSystem;
    private Vector3 _originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        if (_particleSystem == null)
        {
            _particleSystem = GetComponent<ParticleSystem>();
            _particleSystem.Stop();            
        }

        if(transform.parent != null)
            _originalPosition = transform.localPosition;
    }

    public void PlayParticleSystem()
    {
        if(_particleSystem == null)
            _particleSystem = GetComponent<ParticleSystem>();

        gameObject.SetActive(true);
        _particleSystem.Play();
        StartCoroutine(DisableParticleSystemAfterTime());
    }

    IEnumerator DisableParticleSystemAfterTime()
    {
        yield return new WaitForSeconds(_disableAfterTime);
        _particleSystem.Stop();
        gameObject.SetActive(false);
    }

    public void PlayAndDettachParentWhilePlaying()
    {
        if (_particleSystem == null)
            _particleSystem = GetComponent<ParticleSystem>();

        Transform parent = transform.parent;
        transform.parent = null;
        gameObject.SetActive(true);
        _particleSystem.Play();
        StartCoroutine(DisableAndReattachParentAfterPlaying(parent));
    }

    IEnumerator DisableAndReattachParentAfterPlaying(Transform parent)
    {
        yield return new WaitForSeconds(_disableAfterTime);
        transform.parent = parent;
        transform.localPosition = _originalPosition;
        _particleSystem.Stop();
        gameObject.SetActive(false);
    }
}
