using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueTip : MonoBehaviour
{
    [SerializeField] private ParticleSystemManager _particleSystemManager;

    private bool _hasCollided;
    public bool HasCollided { get { return _hasCollided; } set { _hasCollided = value; } }

    private void OnTriggerEnter(Collider other)
    {
        //Collision against homing attackable elements
        if (other.CompareTag("HomingAttackable"))
        {
            AudioManager.instance.PlayBufoTongueImp(gameObject);
            EnemyAnimatorController controller = other.GetComponent<EnemyAnimatorController>();
            _particleSystemManager.PlayParticleSystem();
            controller.Stun();
        }
        //Collision against dragable object
        if (other.CompareTag("Dragable"))
        {
            AudioManager.instance.PlayBufoTongueImp(gameObject);
            _particleSystemManager.PlayParticleSystem();
        }
        //Collision against interactable elements
        if (other.GetComponent<IInteractable>() != null)
        {
            AudioManager.instance.PlayBufoTongueImp(gameObject);
            other.GetComponent<IInteractable>().Interact();
        }
        // Collision against any other physical object
        if (!other.CompareTag("Player"))
        {
            _hasCollided = true;
        }
    }

    private void OnEnable()
    {
        _particleSystemManager.gameObject.SetActive(false);
    }
}
