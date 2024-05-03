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
            CharacterManager cm = other.transform.GetComponent<CharacterManager>();
            if (cm != null)
                cm.HittingEnemy = transform;
            GameManager.instance.TakeDamage(1);
            _collider.enabled = false;
        }
    }
}
