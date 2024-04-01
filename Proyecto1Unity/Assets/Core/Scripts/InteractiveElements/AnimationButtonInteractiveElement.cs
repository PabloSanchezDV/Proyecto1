using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationButtonInteractiveElement : MonoBehaviour, IInteractable
{
    [SerializeField] private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator.enabled = false;
    }

    public void Interact()
    {
        _animator.enabled = true;
        Destroy(this);
    }

}
