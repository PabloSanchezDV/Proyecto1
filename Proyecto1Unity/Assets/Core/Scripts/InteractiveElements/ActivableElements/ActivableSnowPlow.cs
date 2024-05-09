using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivableSnowPlow : ActivableObject
{
    [SerializeField] private GameObject _smoke;
    [SerializeField] private GameObject _lights;
    [SerializeField] private Material _material;
    private bool _isActive = false;
    private MeshRenderer _renderer;

    private void Start()
    {
        _renderer = transform.GetChild(0).GetComponent<MeshRenderer>();
    }

    public override void Activate()
    {
        if(!_isActive)
        {
            _smoke.SetActive(true);
            _lights.SetActive(true);
            //_renderer.material = _material;
        }
    }
}
