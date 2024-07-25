using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class ShowSelectedButtonOnJoystickMovement : MonoBehaviour
{
    public static ShowSelectedButtonOnJoystickMovement instance;

    [SerializeField] private Button _firstButton;
    private InputSystemUIInputModule _inputSystemUIInputModule;
    private InputAction _navigateMove;
    private bool _showSelected = false;
    public bool ShowSelected {  get { return _showSelected; } }

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(instance);
    }

    void Start()
    {
        _inputSystemUIInputModule = GetComponent<InputSystemUIInputModule>();
        _navigateMove = _inputSystemUIInputModule.move.action;
    }

    private void Update()
    {
        if(_navigateMove.ReadValue<Vector2>().magnitude > 0.1f)
        {
            _firstButton.Select();
            _showSelected = true;
            this.enabled = false;
        }
    }
}
