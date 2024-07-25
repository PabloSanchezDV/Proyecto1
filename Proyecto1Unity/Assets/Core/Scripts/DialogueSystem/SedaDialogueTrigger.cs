using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SedaDialogueTrigger : DialogueTrigger
{
    [SerializeField] private int _idAfterCompletion;
    [SerializeField] private bool _isFinalSeda;

    private void Start()
    {
        _cameraRef = transform.GetChild(0);
        _visualPlane = transform.GetChild(2);
        _animator = GetComponent<Animator>();
        _cameraRefOriginalYPosition = _cameraRef.localPosition.y; 
        EventHolder.instance.onAllCollectiblesCollected.AddListener(ChangeDialogueLines);
    }

    protected override void Talk()
    {
        EventHolder.instance.onStartDialogue?.Invoke();
        _cameraRef.transform.localPosition = (player.transform.position - transform.position) / 2;
        _cameraRef.transform.localPosition = new Vector3(-_cameraRef.transform.localPosition.x,
                                                           -0.5f,
                                                            -_cameraRef.transform.localPosition.z);
        CameraSwitcher.instance.SwitchCamera(_virtualCamera, _cameraRef);
        if(_id == _idAfterCompletion)
            EventHolder.instance.onEndDialogue.AddListener(GameManager.instance.NextScene);
        StartDialogue();
        if (_overwriteAtEnd)
            OverwriteAtEnd();
    }

    private void ChangeDialogueLines()
    {
        _id = _idAfterCompletion;
    }
}
