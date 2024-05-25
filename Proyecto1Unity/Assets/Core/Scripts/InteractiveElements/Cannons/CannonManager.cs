using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonManager : MonoBehaviour
{
    public static CannonManager instance;

    [SerializeField] private Cannon[] _cannons;
    [SerializeField] private float _timeBetweenShootAndSpawn;
    [SerializeField] private float _cannonsCadence;

    private bool _canShoot;
    public bool CanShoot { get { return _canShoot; } set { _canShoot = value; } }

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        _canShoot = true;

        foreach(Cannon cannon in  _cannons)
        {
            cannon.timeBetweenShootAndSpawn = _timeBetweenShootAndSpawn;
        }

        Invoke("StartShootingCannons", 0.5f);
    }

    private void StartShootingCannons()
    {
        StartCoroutine(ShootCannons());
    }

    IEnumerator ShootCannons()
    {
        bool allCannonsAreOff = true;

        foreach(Cannon cannon in _cannons)
        {
            if (cannon.CanShoot)
            {
                if (!_canShoot)
                    break;

                allCannonsAreOff = false;
                cannon.Shoot();
                yield return new WaitForSeconds(_cannonsCadence);
            }
        }

        if(!allCannonsAreOff && _canShoot)
            StartCoroutine(ShootCannons());
    }

    public void ResumeShooting()
    {
        _canShoot = true;
        StartCoroutine(ShootCannons());
    }
}
