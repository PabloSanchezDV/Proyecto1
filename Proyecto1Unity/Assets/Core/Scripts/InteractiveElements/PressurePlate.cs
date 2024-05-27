using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private GameObject _box;
    [SerializeField] private ActivableObject[] _activableObjects;
    [SerializeField] private ParticleSystemManager _particleSystemManager;
    [SerializeField] private float _boxMaxDistance; 

    [SerializeField] private bool _isConditionComplete = false;

    private Animator _animator;
    private Vector3 _boxResetPosition;
    private CharacterManager _characterManager;

    private void Start()
    {
        _animator = transform.parent.parent.GetComponent<Animator>();
        _characterManager = GameManager.instance.Player.GetComponent<CharacterManager>();
        _boxResetPosition = _box.transform.position;
        StartCoroutine(CheckBoxDistance());
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
        if (_characterManager.IsDragging)
        {
            _characterManager.DettachDragableObject(_box);
        }
        _box.transform.position = _boxResetPosition;
    }

    public void SetConditionCompleteAs(bool state)
    {
        _isConditionComplete = state;
    }

    IEnumerator CheckBoxDistance()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            if(Vector3.Distance(_box.transform.position, transform.position) > _boxMaxDistance)
            {
                ResetBox();
            }
        }
    }
}
