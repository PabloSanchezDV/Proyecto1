using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    private List<DialogueDatabase> _dialogueDatabaseList;
    private bool _doNextDialogue = false;
    private bool _isInDialogue = false;
    public bool DoNextDialogue { get { return _doNextDialogue; } set {  _doNextDialogue = value; } }
    public bool IsInDialogue { get { return _isInDialogue; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        _dialogueDatabaseList = DialogueBuilder.BuildDialogueDatabaseList();
    }

    public void TriggerDialogue(int level, int id)
    {
        _isInDialogue = true;
        List<DialogueNode> dialogueNodes = GetDialogueNodeList(level, id);
        StartCoroutine(SendDialoguesToUIManager(dialogueNodes));
    }

    private List<DialogueNode> GetDialogueNodeList(int level, int id)
    {
        foreach(DialogueDatabase dialogueDatabase in _dialogueDatabaseList)
        {
            if(dialogueDatabase.Level == level)
            {
                foreach(DialogueList dialogueList in dialogueDatabase.List)
                {
                    if(dialogueList.ID == id)
                    {
                        return dialogueList.DialogueNodeList;
                    }
                }
            }
        }
        throw new Exception($"Dialogue (level: {level}, id: {id}) doesn't exist.");
    }

    IEnumerator SendDialoguesToUIManager(List<DialogueNode> dialogueNodes)
    {
        foreach(DialogueNode node in dialogueNodes)
        {
            //UIManager.StartDialogue();
            string character = node.Character;
            string text = node.Text;
            Debug.Log(character + ": " + text);
            //UIManager.ShowDialogue(character, text);
            while(!_doNextDialogue)
                yield return null;
            _doNextDialogue = false;
        }
        //UIManager.EndDialogue();
        _isInDialogue = false;
    }
}
