using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private GameObject _box;
    [SerializeField] private ActivableObject[] _activableObjects;
    [SerializeField] private ParticleSystemManager _particleSystemManager;

    [SerializeField] private bool _isConditionComplete = false;

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

    public void Interact(bool state)
    {

        if (_animator != null)
            _animator.SetTrigger("ChangeState");

        if (state)
        {
            if (!_isConditionComplete)
            {
                AudioManager.instance.PlayPressurePlateBroken(gameObject);
                _particleSystemManager.PlayParticleSystem();
            }
            else
            {
                AudioManager.instance.PlayPressurePlate(gameObject);
                ActivateObjects();
            }
        }
    }

    private void ActivateObjects()
    {
        foreach(ActivableObject activableObject in  _activableObjects)
        {
            activableObject.Activate();
        }
    }

    public void ResetBox()
    {
        _box.transform.position = _boxResetPosition;
    }

    public void SetConditionCompleteAs(bool state)
    {
        _isConditionComplete = state;
    }
}
