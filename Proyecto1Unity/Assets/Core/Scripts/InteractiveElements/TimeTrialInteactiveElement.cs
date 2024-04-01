using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTrialInteactiveElement : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject[] _trialGameObjects;
    [SerializeField] private float _trialTime;

    private bool _isActive = false;

    public void Interact()
    {
        if (_isActive)
            return;

        _isActive = true;
        StartCoroutine(TimeTrialCoroutine());
    }

    IEnumerator TimeTrialCoroutine()
    {
        SetGameObjectsAs(true);
        yield return new WaitForSeconds(_trialTime);
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
