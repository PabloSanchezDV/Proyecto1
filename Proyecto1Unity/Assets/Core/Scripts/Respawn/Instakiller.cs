using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instakiller : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EventHolder.instance.onDeath?.Invoke();
        }
    }
}
