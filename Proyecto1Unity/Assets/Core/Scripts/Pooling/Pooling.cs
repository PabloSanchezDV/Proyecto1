using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooling : MonoBehaviour
{
    [SerializeField] protected GameObject _prefab;
    [SerializeField] private int _poolingSize;

    protected Queue<GameObject> _queue;

    // Start is called before the first frame update
    void Start()
    {
        _queue = new Queue<GameObject>();
        for (int i = 0; i < _poolingSize; i++)
        {
            CreateGameObject();
        }
    }

    protected virtual GameObject CreateGameObject()
    {
        GameObject newGameObject = Instantiate(_prefab, transform);
        newGameObject.SetActive(false);
        _queue.Enqueue(newGameObject);
        return newGameObject;
    }

    protected GameObject GetPrefab()
    {
        if (_queue.Count <= 0)
            CreateGameObject();

        return _queue.Dequeue();
    }

    protected void ReturnGameObject(GameObject go)
    {
        _queue.Enqueue(go);
    }
}
