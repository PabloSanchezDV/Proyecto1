using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverForActivableObjects : MonoBehaviour, IInteractable
{
    [SerializeField] ActivableObject _activableObject;
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

        if (_animator != null)
        {
            AudioManager.instance.PlayLever(gameObject);
            _animator.SetTrigger("ChangeState");
        }

        _activableObject.Activate();
    }
}
