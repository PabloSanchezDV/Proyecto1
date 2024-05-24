using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivableLightedSign : ActivableObject
{
    [SerializeField] private CinemachineVirtualCameraBase _camera;
    [SerializeField] private float _timePre;
    [SerializeField] private float _timePost;
    GameObject _screen;
    private bool _isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        _screen = transform.GetChild(1).gameObject;
    }

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
        AudioManager.instance.PlayLightedSign(gameObject);
        _screen.SetActive(true);
        yield return new WaitForSeconds(_timePost);
        CameraSwitcher.instance.ResetPlayerCamera();
        GameManager.instance.Player.GetComponent<CharacterManager>().CanMove = true;
    }
}
