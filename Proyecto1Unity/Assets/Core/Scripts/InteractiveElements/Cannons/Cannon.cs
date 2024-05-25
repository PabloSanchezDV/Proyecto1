using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : ActivableObject
{
    [SerializeField] private CinemachineVirtualCameraBase _camera;
    [SerializeField] private float _timePre;
    [SerializeField] private float _timePost;
    [SerializeField] private MultipleParticleSystemManager _shootParticleSystemManager;
    [SerializeField] private ParticleSystemManager _brokenParticleSystemManager;
    [SerializeField] private bool _canShoot;

    public bool CanShoot { get { return _canShoot; } }
    [NonSerialized] public float timeBetweenShootAndSpawn;
    private Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    public void Shoot()
    {
        if(_canShoot)
        {
            _anim.SetTrigger("Shoot");
            AudioManager.instance.PlayCannonShooting(gameObject);
            _shootParticleSystemManager.PlayParticleSystem();
            StartCoroutine(WaitTimeToInstantiateBullet());
        }
    }

    private void InstantiateBullet()
    {
        GameObject cB = CannonPooling.instance.GetCannonBullet();
        cB.SetActive(true);
        cB.GetComponent<CannonBullet>().Initialize();
    }

    IEnumerator WaitTimeToInstantiateBullet()
    {
        yield return new WaitForSeconds(timeBetweenShootAndSpawn);
        InstantiateBullet();
    }

    public override void Activate()
    {
        if (_canShoot)
        {
            CannonManager.instance.CanShoot = false;
            StartCoroutine(TransitionCameraToShowChanges());
            _canShoot = false;
        }
    }

    IEnumerator TransitionCameraToShowChanges()
    {
        GameManager.instance.Player.GetComponent<CharacterManager>().CanMove = false;
        CameraSwitcher.instance.SwitchCamera(_camera);
        yield return new WaitForSeconds(_timePre);
        AudioManager.instance.PlayCannonBroken(gameObject);
        _brokenParticleSystemManager.PlayParticleSystem();
        yield return new WaitForSeconds(_timePost);
        CameraSwitcher.instance.ResetPlayerCamera();
        GameManager.instance.Player.GetComponent<CharacterManager>().CanMove = true;
        CannonManager.instance.CanShoot = true;
    }
}
