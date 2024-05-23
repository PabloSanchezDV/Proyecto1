using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailPooling : Pooling
{
    public static TrailPooling instance;
    [SerializeField] private Color _trailColor;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    protected override GameObject CreateGameObject()
    {
        GameObject newGameObject = Instantiate(_prefab, transform);
        ParticleSystem.MainModule main = _prefab.GetComponent<ParticleSystem>().main;
        main.startColor = _trailColor;
        newGameObject.SetActive(false);
        _queue.Enqueue(newGameObject);
        return newGameObject;
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
