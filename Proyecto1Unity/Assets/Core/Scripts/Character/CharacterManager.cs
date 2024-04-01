using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private static bool _canMove = true;
    public static bool CanMove { get { return _canMove; } set { _canMove = value; } }

    private static bool _isDragging = false;
    public static bool IsDragging { get { return _isDragging; } set { _isDragging = value; } }

    private static bool _canThrowTongue = true;
    public static bool CanThrowTongue { get { return _canThrowTongue; } set { _canThrowTongue = value; } }

    private static bool _isThrowingTongue = false;
    public static bool IsThrowingTongue { get { return _isThrowingTongue; } set { _isThrowingTongue = value; } }


    private static InputActions _inputActions;
    public static InputActions InputActions { get { return _inputActions;} }
       
    private static CharacterMovement characterMovement;
    private static CharacterAbilities characterAbilities;

    // Start is called before the first frame update
    void Awake()
    {
        _inputActions = new InputActions();
        characterMovement = GetComponent<CharacterMovement>();
        characterAbilities = GetComponent<CharacterAbilities>();

        characterMovement.enabled = true;
        characterAbilities.enabled = true;
    }

    public static void JumpOnHomingAttack()
    {
        characterMovement.JumpOnHomingAttack();
    }

    public static void EnableJumpOnEndDragging()
    {
        characterMovement.EnableJumpOnEndDragging();
    }

    public static void DisableJumpOnStartDragging()
    {
        characterMovement.DisableJumpOnStartDragging();
    }
}
