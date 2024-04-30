using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTrialInteactiveElement : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject[] _trialGameObjects;
    [SerializeField] private float _trialTime;

    private Animator _animator;
    private bool _isActive = false;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Interact()
    {
        if (_isActive)
            return;

        _isActive = true;
        if(_animator != null)
            _animator.SetTrigger("ChangeState");
        StartCoroutine(TimeTrialCoroutine());
    }

    IEnumerator TimeTrialCoroutine()
    {
        SetGameObjectsAs(true);
        yield return new WaitForSeconds(_trialTime); 
        if (_animator != null)
            _animator.SetTrigger("ChangeState");
        SetGameObjectsAs(false);
        _isActive = false;
    }

    private void SetGameObjectsAs(bool setAs)
    {
        foreach(GameObject go in _trialGameObjects)
        {
            go.SetActive(setAs);
        }
    }
}
