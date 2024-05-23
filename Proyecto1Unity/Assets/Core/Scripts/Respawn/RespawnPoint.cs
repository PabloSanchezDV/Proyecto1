using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    [SerializeField] private Vector3 _respawnPosition;

    private Animator _anim;

    private void Start()
    {
        _anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _anim.ResetTrigger("ResetSignal");
            _anim.SetTrigger("SwapSignal");
            GameManager.instance.LastRespawnPosition = _respawnPosition;
            RespawnPointController.instance.UpdateRespawnPoints(this);
            Debug.Log("New spawn point set");
        }
    }

    public void ResetSignal()
    {
        _anim.ResetTrigger("SwapSignal");
        _anim.SetTrigger("ResetSignal");
    }
}
