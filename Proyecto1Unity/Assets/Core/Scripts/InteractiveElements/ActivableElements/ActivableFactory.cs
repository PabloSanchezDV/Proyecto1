using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivableFactory : ActivableObject
{
    [SerializeField] private GameObject _smoke;
    private bool _isActive = false;

    public override void Activate()
    {
        if (!_isActive)
        {
            AudioManager.instance.PlayFactory(gameObject);
            _smoke.SetActive(true);
        }
    }
}
