using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    [SerializeField] private Vector3 _respawnPosition;

    private Animator _anim;
    private bool _isActive = false;

    private void Start()
    {
        _anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (!_isActive)
            {
                AudioManager.instance.PlayCheckpoint(gameObject);
                _anim.ResetTrigger("ResetSignal");
                _anim.SetTrigger("SwapSignal");
                _isActive = true;
                GameManager.instance.LastRespawnPosition = _respawnPosition;
                RespawnPointController.instance.UpdateRespawnPoints(this);
            }
        }
    }

    public void ResetSignal()
    {
        _isActive = false;
        _anim.ResetTrigger("SwapSignal");
        _anim.SetTrigger("ResetSignal");
    }
}
