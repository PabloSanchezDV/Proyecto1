using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.AxisState;

public class CameraInputProvider : MonoBehaviour, IInputAxisProvider
{
    [SerializeField] private CharacterManager _characterManager;
    private InputActions _inputActions;

    private void Start()
    {
        _inputActions = _characterManager.InputActions;
    }

    public float GetAxisValue(int axis)
    {
        switch (axis)
        {
            case 0:
                float xInput = _inputActions.Gameplay.RotateCamera.ReadValue<Vector2>().x;
                xInput = Mathf.Clamp(xInput, -1, 1);
                return xInput;
            case 1:
                float yInput = _inputActions.Gameplay.RotateCamera.ReadValue<Vector2>().y;
                yInput = Mathf.Clamp(yInput, -1, 1);
                return yInput;
            case 2: 
                return 0f;
        }
        return 0;
    }
}
