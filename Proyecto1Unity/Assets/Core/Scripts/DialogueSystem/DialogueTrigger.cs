using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] protected CinemachineVirtualCamera _virtualCamera;
    [SerializeField] protected int _id;
    [SerializeField] protected bool _overwriteAtEnd;
    [SerializeField] protected bool _requiresInput;
    [SerializeField] protected int _newID;

    protected Transform _cameraRef;
    protected Animator _animator;

    protected CharacterManager player;

    private void Start()
    {
        _cameraRef = transform.GetChild(0);
        _animator = GetComponent<Animator>();
    }

    protected void StartDialogue()
    {
        DialogueManager.instance.TriggerDialogue(_id, _virtualCamera, _animator);
    }

    protected void OverwriteAtEnd()
    {
        _id = _newID;
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (player == null)
                player = other.transform.GetComponent<CharacterManager>();
            if(!_requiresInput)
            {
                Talk();
                _requiresInput = true;
            }
            else
            {
                player.ActivateTalkInput(this);
            }
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_requiresInput)
            {
                player.DeactivateTalkInput(this); 
            }
        }
    }

    public virtual void Talk(InputAction.CallbackContext context)
    {
        Talk();
    }

    protected virtual void Talk()
    {
        EventHolder.instance.onStartDialogue?.Invoke();
        _cameraRef.transform.localPosition = (player.transform.position - transform.position) / 2;
        _cameraRef.transform.localPosition = new Vector3(_cameraRef.transform.localPosition.x,
                                                           -0.5f,
                                                            _cameraRef.transform.localPosition.z);
        CameraSwitcher.instance.SwitchCamera(_virtualCamera, _cameraRef);
        StartDialogue();
        if (_overwriteAtEnd)
            OverwriteAtEnd();
    }
}
