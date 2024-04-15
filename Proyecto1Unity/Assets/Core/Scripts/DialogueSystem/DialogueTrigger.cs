using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] private bool _overwriteAtEnd;
    [SerializeField] private int _newID;

    private void StartDialogue()
    {
        DialogueManager.instance.TriggerDialogue(_id);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T)) 
        {
            if (!DialogueManager.instance.IsInDialogue)
            {
                StartDialogue();
                if(_overwriteAtEnd)
                    OverwriteAtEnd();
            }
            else
                DialogueManager.instance.DoNextDialogue = true;
        }
    }

    private void OverwriteAtEnd()
    {
        _id = _newID;
    }
}
