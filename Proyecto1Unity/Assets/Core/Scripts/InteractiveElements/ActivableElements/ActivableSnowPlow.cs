using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivableSnowPlow : ActivableObject
{
    [SerializeField] private CinemachineVirtualCameraBase _camera;
    [SerializeField] private float _timePre;
    [SerializeField] private float _timePost;
    [SerializeField] private GameObject _smoke;
    [SerializeField] private GameObject _lights;
    [SerializeField] private Material _material;
    private bool _isActive = false;
    private MeshRenderer _renderer;

    private void Start()
    {
        _renderer = transform.GetChild(1).GetComponent<MeshRenderer>();
    }

    public override void Activate()
    {
        if(!_isActive)
        {
            StartCoroutine(TransitionCameraToShowChanges());
            _isActive = true;
        }
    }

    IEnumerator TransitionCameraToShowChanges()
    {
        GameManager.instance.Player.GetComponent<CharacterManager>().CanMove = false;
        CameraSwitcher.instance.SwitchCamera(_camera);
        yield return new WaitForSeconds(_timePre); 
        AudioManager.instance.PlaySnowplow(gameObject);
        _smoke.SetActive(true);
        _lights.SetActive(true);
        _renderer.material = _material;
        yield return new WaitForSeconds(_timePost);
        CameraSwitcher.instance.ResetPlayerCamera();
        GameManager.instance.Player.GetComponent<CharacterManager>().CanMove = true;
    }
}
