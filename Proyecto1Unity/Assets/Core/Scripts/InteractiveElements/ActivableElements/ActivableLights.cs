using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivableLights : ActivableObject
{
    [SerializeField] private CinemachineVirtualCameraBase _camera;
    [SerializeField] private float _timePre;
    [SerializeField] private float _timePost;
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
            StartCoroutine(TransitionCameraToShowChanges());
        }
    }

    IEnumerator TransitionCameraToShowChanges()
    {
        GameManager.instance.Player.GetComponent<CharacterManager>().CanMove = false;
        CameraSwitcher.instance.SwitchCamera(_camera);
        yield return new WaitForSeconds(_timePre);
        _lights.SetActive(true);
        _renderer.material = _material;
        yield return new WaitForSeconds(_timePost);
        CameraSwitcher.instance.ResetPlayerCamera();
        GameManager.instance.Player.GetComponent<CharacterManager>().CanMove = true;
    }
}
