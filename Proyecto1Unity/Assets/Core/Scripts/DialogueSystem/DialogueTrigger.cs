using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] protected CinemachineVirtualCamera _virtualCamera;
    [SerializeField] protected int _id;
    [SerializeField] protected bool _overwriteAtEnd;
    [SerializeField] protected int _newID;

    protected Transform _cameraRef;

    private void Start()
    {
        _cameraRef = transform.GetChild(0);
    }

    protected void StartDialogue()
    {
        DialogueManager.instance.TriggerDialogue(_id, _virtualCamera);
    }

    protected void OverwriteAtEnd()
    {
        _id = _newID;
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EventHolder.instance.onStartDialogue?.Invoke();
            _cameraRef.transform.localPosition = (other.transform.position - transform.position) / 2;
            _cameraRef.transform.localPosition = new Vector3(_cameraRef.transform.localPosition.x, 
                                                               -0.5f,
                                                                _cameraRef.transform.localPosition.z);
            CameraSwitcher.instance.SwitchCamera(_virtualCamera, _cameraRef);
            StartDialogue();
            if (_overwriteAtEnd)
                OverwriteAtEnd();
        }
    }
}
