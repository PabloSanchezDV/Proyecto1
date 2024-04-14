using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBite : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("HomingAttackable"))
        {
            other.gameObject.SetActive(false);
            Debug.Log(other.name + " detected and deactivated.");
        }
    }
}
