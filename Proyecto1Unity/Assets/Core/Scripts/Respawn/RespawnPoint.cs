using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    [SerializeField] private Vector3 _respawnPosition;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //animation
            GameManager.instance.LastRespawnPosition = _respawnPosition;
            Debug.Log("New spawn point set");
        }
    }
}
