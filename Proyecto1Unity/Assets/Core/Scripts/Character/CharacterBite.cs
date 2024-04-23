using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBite : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("HomingAttackable"))
        {
            EnemyAnimatorController controller = other.GetComponent<EnemyAnimatorController>();
            controller.Death();
        }
    }
}
