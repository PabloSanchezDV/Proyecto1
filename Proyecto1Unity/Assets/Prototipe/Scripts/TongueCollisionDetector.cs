using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueCollisionDetector : MonoBehaviour
{
    /*[NonSerialized]*/ public GameObject collisionedObject;
    
    private bool _hasCollisioned = false;
    
    public bool HasCollisioned { get { return _hasCollisioned; } set { _hasCollisioned = value; } }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("TongueAimable"))
        {
            collisionedObject = other.gameObject;
            _hasCollisioned = true;
        }
    }
}
