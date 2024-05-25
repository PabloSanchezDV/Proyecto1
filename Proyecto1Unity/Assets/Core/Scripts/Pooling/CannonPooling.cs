using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonPooling : Pooling
{
    public static CannonPooling instance;

    [SerializeField] private Transform _playerTransform;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    protected override GameObject CreateGameObject()
    {
        GameObject newCannonBullet = Instantiate(_prefab, transform);
        newCannonBullet.SetActive(false);
        newCannonBullet.GetComponent<CannonBullet>().playerTransform = _playerTransform;
        _queue.Enqueue(newCannonBullet);
        return newCannonBullet;
    }

    public GameObject GetCannonBullet()
    {
        return GetPrefab();
    }

    public void ReturnCannonBullet(GameObject projectile)
    {
        ReturnGameObject(projectile);
    }
}
