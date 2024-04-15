using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    [SerializeField] private int _currentLevel;
    private DialogueBST _dialogueBST;
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

        _dialogueBST = new DialogueBST(DialogueBuilder.BuildDialogueListsList(_currentLevel));
    }

    public void TriggerDialogue(int id)
    {
        _isInDialogue = true;
        List<DialogueNode> dialogueNodes = GetDialogueNodeList(id);
        StartCoroutine(SendDialoguesToUIManager(dialogueNodes));
    }

    private List<DialogueNode> GetDialogueNodeList(int id)
    {
        return _dialogueBST.Search(id);
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
