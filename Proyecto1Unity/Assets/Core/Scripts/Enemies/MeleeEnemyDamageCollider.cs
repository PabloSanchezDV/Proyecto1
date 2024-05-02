using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyDamageCollider : MonoBehaviour
{
    private Collider _collider;

    private void Start()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //DoDamage();
            _collider.enabled = false;
        }
    }
}
