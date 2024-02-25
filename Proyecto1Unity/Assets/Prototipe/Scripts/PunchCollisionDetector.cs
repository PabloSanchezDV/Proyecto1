using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchCollisionDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            Debug.Log("Punch");
            other.GetComponent<Animator>().SetTrigger("Punch");
        }
    }
}
