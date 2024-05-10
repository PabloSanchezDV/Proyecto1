using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivableLights : ActivableObject
{
    [SerializeField] private GameObject _lights;
    [SerializeField] private Material _material;
    private bool _isActive = false;
    private MeshRenderer _renderer;

    private void Start()
    {
        _renderer = transform.GetChild(2).GetComponent<MeshRenderer>();
    }

    public override void Activate()
    {
        if (!_isActive)
        {
            _lights.SetActive(true);
            _renderer.material = _material;
        }
    }
}
