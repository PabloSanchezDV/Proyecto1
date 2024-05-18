using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverInteractiveElement : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject[] _leverGameObjects;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Interact()
    {
        if (_animator != null)
        {
            AudioManager.instance.PlayLever(gameObject);
            _animator.SetTrigger("ChangeState");
        }

        EnableDisableGameObjects();
    }

    private void EnableDisableGameObjects()
    {
        foreach (GameObject go in _leverGameObjects)
        {
            go.SetActive(!go.activeInHierarchy);
        }
    }
}
