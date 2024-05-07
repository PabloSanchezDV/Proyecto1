using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private int _id;
    [SerializeField] private bool _overwriteAtEnd;
    [SerializeField] private int _newID;

    private Transform _cameraRef;

    private void Start()
    {
        _cameraRef = transform.GetChild(0);
    }

    private void StartDialogue()
    {
        DialogueManager.instance.TriggerDialogue(_id);
    }

    private void OverwriteAtEnd()
    {
        _id = _newID;
    }

    private void OnTriggerEnter(Collider other)
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
