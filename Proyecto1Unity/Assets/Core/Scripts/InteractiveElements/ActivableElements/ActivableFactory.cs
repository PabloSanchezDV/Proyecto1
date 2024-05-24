using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivableFactory : ActivableObject
{
    [SerializeField] private CinemachineVirtualCameraBase _camera;
    [SerializeField] private float _timePre;
    [SerializeField] private float _timePost;
    [SerializeField] private GameObject _smoke;
    private bool _isActive = false;

    public override void Activate()
    {
        if (!_isActive)
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
        AudioManager.instance.PlayFactory(gameObject);
        _smoke.SetActive(true);
        yield return new WaitForSeconds(_timePost);
        CameraSwitcher.instance.ResetPlayerCamera();
        GameManager.instance.Player.GetComponent<CharacterManager>().CanMove = true;
    }
}
