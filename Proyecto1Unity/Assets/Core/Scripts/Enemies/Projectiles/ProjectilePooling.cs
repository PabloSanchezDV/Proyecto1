using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePooling : MonoBehaviour
{
    public static ProjectilePooling instance;

    [SerializeField] private GameObject _projectile;
    [SerializeField] private int _poolingSize;

    private Queue<GameObject> _projectileQueue;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        _projectileQueue = new Queue<GameObject>();
        for(int i = 0; i < _poolingSize; i++)
        {
            CreateProjectile();
        }
    }

    public GameObject CreateProjectile()
    {
        GameObject newProjectile = Instantiate(_projectile, transform);
        newProjectile.SetActive(false);
        _projectileQueue.Enqueue(newProjectile);
        return newProjectile;
    }

    public GameObject GetProjectile()
    {
        if(_projectileQueue.Count <= 0)
            CreateProjectile();
                 
        return _projectileQueue.Dequeue();
    }

    public void ReturnBullet(GameObject projectile)
    {
        _projectileQueue.Enqueue(projectile);
    }
}
