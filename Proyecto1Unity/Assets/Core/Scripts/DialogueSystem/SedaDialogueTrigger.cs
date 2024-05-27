using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SedaDialogueTrigger : DialogueTrigger
{
    [SerializeField] private int _idAfterCompletion;

    protected override void Talk()
    {
        EventHolder.instance.onStartDialogue?.Invoke();
        _cameraRef.transform.localPosition = (player.transform.position - transform.position) / 2;
        _cameraRef.transform.localPosition = new Vector3(-_cameraRef.transform.localPosition.x,
                                                           -0.5f,
                                                            -_cameraRef.transform.localPosition.z);
        CameraSwitcher.instance.SwitchCamera(_virtualCamera, _cameraRef);
        StartDialogue();
        if (_overwriteAtEnd)
            OverwriteAtEnd();
        EventHolder.instance.onAllCollectiblesCollected.AddListener(ChangeDialogueLines);
    }

    private void ChangeDialogueLines()
    {
        _id = _idAfterCompletion;
        EventHolder.instance.onEndDialogue.AddListener(GameManager.instance.NextScene);
    }
}
