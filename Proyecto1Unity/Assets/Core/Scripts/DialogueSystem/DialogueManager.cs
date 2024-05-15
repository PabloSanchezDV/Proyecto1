using Cinemachine;
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

    private Animator _bufoAnimator;
    public Animator BufoAnimator { set { _bufoAnimator = value; } }

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

    public void TriggerDialogue(int id, CinemachineVirtualCameraBase npcCamera, Animator animator)
    {
        _isInDialogue = true;
        List<DialogueNode> dialogueNodes = GetDialogueNodeList(id);
        StartCoroutine(ShowDialogues(dialogueNodes, npcCamera, animator));
    }

    private List<DialogueNode> GetDialogueNodeList(int id)
    {
        return _dialogueBST.Search(id);
    }

    IEnumerator ShowDialogues(List<DialogueNode> dialogueNodes, CinemachineVirtualCameraBase npcCamera, Animator animator)
    {
        UIManager.instance.ShowDialoguePanel();
        foreach (DialogueNode node in dialogueNodes)
        {
            if(node.Character != "*")
            {
                string character = node.Character;
                string text = node.Text;
                if (character != "Bufo")
                    animator.SetBool("isTalking", true);
                else
                    _bufoAnimator.SetBool("isTalking", true);
                UIManager.instance.UpdateDialoguePanel(character, text);
                while(!_doNextDialogue)
                    yield return null;
                if (character != "Bufo")
                    animator.SetBool("isTalking", false);
                else
                    _bufoAnimator.SetBool("isTalking", false);
                _doNextDialogue = false;
            }
            else
            {
                CinemachineVirtualCameraBase newCamera = new CinemachineVirtualCamera();

                if(node.Text != "Reset")
                {
                    foreach (CinemachineVirtualCameraBase camera in CameraSwitcher.instance.VirtualCameras)
                    {
                        if(camera.name == node.Text)
                        {
                            newCamera = camera;
                            break;
                        }
                    }
                }
                else
                {
                    newCamera = npcCamera;
                }

                if (newCamera == null)
                    throw new Exception("Camera is not found in scene. Confirm the name of the camera both in the CSV file and in the scene");

                CameraSwitcher.instance.SwitchCamera(newCamera);
            }
        }
        UIManager.instance.HideDialoguePanel();
        _isInDialogue = false;
        EventHolder.instance.onEndDialogue?.Invoke();
    }
}
