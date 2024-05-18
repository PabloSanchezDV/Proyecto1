using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagableObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterManager cm = other.transform.GetComponent<CharacterManager>();
            if (cm != null)
                cm.HittingEnemy = transform;
            GameManager.instance.TakeDamage(1);
        }
    }
}
