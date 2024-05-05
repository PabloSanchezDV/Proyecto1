using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailPooling : Pooling
{
    public static TrailPooling instance;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    public GameObject GetTrail()
    {
        return GetPrefab();
    }

    public void ReturnTrail(GameObject trail)
    {
        ReturnGameObject(trail);
    }
}
