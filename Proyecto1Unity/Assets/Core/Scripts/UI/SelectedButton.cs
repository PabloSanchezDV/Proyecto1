using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedButton : MonoBehaviour
{
    [SerializeField] private Button onEnableButton;
    [SerializeField] private Button onDisableButton;
    private void OnEnable()
    {
        if(ShowSelectedButtonOnJoystickMovement.instance.ShowSelected)
            onEnableButton.Select();
    }

    private void OnDisable()
    {
        if (ShowSelectedButtonOnJoystickMovement.instance.ShowSelected)
            onDisableButton.Select();
    }
}
