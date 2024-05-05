using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instakiller : MonoBehaviour
{
    [SerializeField] private ParticleSystemManager _particleSystemManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _particleSystemManager.transform.position = other.transform.position;
            _particleSystemManager.PlayParticleSystem();
            EventHolder.instance.onDeath?.Invoke();
        }
    }
}
