using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePooling : Pooling
{
    public static ProjectilePooling instance;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    public GameObject GetProjectile()
    {
        return GetPrefab();
    }

    public void ReturnBullet(GameObject projectile)
    {
        ReturnGameObject(projectile);
    }
}
