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
    protected float _cameraRefOriginalYPosition;
    protected Transform _visualPlane;

    private void Start()
    {
        _cameraRef = transform.GetChild(0);
        _visualPlane = transform.GetChild(2);
        _animator = GetComponent<Animator>();
        _cameraRefOriginalYPosition = _cameraRef.localPosition.y;
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
        LookAtPlayer();
        _cameraRef.transform.localPosition = (player.transform.position - transform.position) / 2;
        _cameraRef.transform.localPosition = new Vector3(_cameraRef.transform.localPosition.x,
                                                           _cameraRefOriginalYPosition,
                                                            _cameraRef.transform.localPosition.z);
        CameraSwitcher.instance.SwitchCamera(_virtualCamera, _cameraRef);
        StartDialogue();
        if (_overwriteAtEnd)
            OverwriteAtEnd();
    }

    protected void LookAtPlayer()
    {
        if ((player.transform.position - transform.position).x >= 0)
        {
            _visualPlane.transform.localScale = new Vector3(Mathf.Abs(_visualPlane.transform.localScale.x),
                                               _visualPlane.transform.localScale.y,
                                               _visualPlane.transform.localScale.z);
        }
        else
        {
            _visualPlane.transform.localScale = new Vector3(-Mathf.Abs(_visualPlane.transform.localScale.x),
                                               _visualPlane.transform.localScale.y,
                                               _visualPlane.transform.localScale.z);
        }
    }
}
