using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private GameObject _box;
    [SerializeField] private Animator _doorAnimator;
    [SerializeField] private ParticleSystemManager _particleSystemManager;
    [SerializeField] private bool _isConditionComplete = true;

    private Animator _animator;
    private Vector3 _boxResetPosition;

    private void Start()
    {
        _animator = transform.parent.parent.GetComponent<Animator>();
        _boxResetPosition = _box.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == _box)
        {
            Interact(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _box)
        {
            Interact(false);
        }
    }

    private void Interact(bool state)
    {
        if (_animator != null)
            _animator.SetTrigger("ChangeState");

        if (state)
        {
            if (!_isConditionComplete)
            {
                _particleSystemManager.PlayParticleSystem();
            }
            else
            {
                if(_doorAnimator != null)
                {
                    if (_doorAnimator.enabled)
                    {
                        _doorAnimator.SetTrigger("Open");
                    }
                }
            }
        }
    }

    public void ResetBox()
    {
        _box.transform.position = _boxResetPosition;
    }
}
