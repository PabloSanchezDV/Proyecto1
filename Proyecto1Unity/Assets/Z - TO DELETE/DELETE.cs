using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DELETE : MonoBehaviour
{
    private bool slowTime = false;
    [SerializeField] Animator animator;
    [SerializeField] int currentScene;
    [SerializeField] CharacterManager characterManager;

    private InputAction rotateCamera;
    private Gamepad gamepad;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(currentScene);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (slowTime)
            {
                Time.timeScale = 1.0f;
                slowTime = false;
            }
            else
            {
                Time.timeScale = 0.25f;
                slowTime = true;
            }
        }

        if(Input.GetKeyDown(KeyCode.I))
        {
            if (rotateCamera == null)
            {
                rotateCamera = characterManager.InputActions.Gameplay.RotateCamera;
                gamepad = Gamepad.current;
            }
            else
                rotateCamera = null;
        }

        if(rotateCamera != null)
        {
            DebugInputValue();
        }
    }

    private void DebugInputValue()
    {
        Vector2 value = rotateCamera.ReadValue<Vector2>();
        Debug.Log("Input value: (" + value.x + ", " + value.y + ")");
    }
}
