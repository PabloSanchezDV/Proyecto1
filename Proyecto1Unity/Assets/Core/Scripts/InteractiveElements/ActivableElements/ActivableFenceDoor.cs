using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivableFenceDoor : ActivableObject
{
    [SerializeField] private float _timeToActivate;
    private Animator _anim;
    private bool _isOpen;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    public override void Activate()
    {
        if (_isOpen)
            return;

        StartCoroutine(ActivateAfterTime());
    }

    public void DeactivateAnimator()
    {
        _anim.enabled = false;
    }

    IEnumerator ActivateAfterTime()
    {
        yield return new WaitForSeconds(_timeToActivate); 
        AudioManager.instance.PlayFence(gameObject);
        if (_anim != null)
        {
            _anim.SetTrigger("Open");
        }
    }
}
