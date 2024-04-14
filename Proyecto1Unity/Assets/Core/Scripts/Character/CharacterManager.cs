using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    #region Variables
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
       
    private static CharacterMovement _characterMovement;
    private static CharacterAbilities _characterAbilities;
    private static CharacterAnimatorController _characterAnimatorController;
    #endregion

    // Start is called before the first frame update
    void Awake()
    {
        _inputActions = new InputActions();
        _characterMovement = GetComponent<CharacterMovement>();
        _characterAbilities = GetComponent<CharacterAbilities>();
        _characterAnimatorController = GetComponent<CharacterAnimatorController>();

        _characterMovement.enabled = true;
        _characterAbilities.enabled = true;
        _characterAnimatorController.enabled = true;
    }

    #region Communications
    #region Movement - Abilities
    public static void JumpOnHomingAttack()
    {
        _characterMovement.JumpOnHomingAttack();
    }

    public static void EnableJumpOnEndDragging()
    {
        _characterMovement.EnableJumpOnEndDragging();
    }

    public static void DisableJumpOnStartDragging()
    {
        _characterMovement.DisableJumpOnStartDragging();
    }
    #endregion

    #region Animation Communications
    #region Movement - Animation Controller
    public static void Jump()
    {
        _characterAnimatorController.Jump();
    }

    public static void DoubleJump()
    {
        _characterAnimatorController.DoubleJump();
    }

    public static void EndElevating()
    {
        _characterAnimatorController.EndElevating();
    }

    public static void ResetElevating()
    {
        _characterAnimatorController.ResetElevating();
    }

    public static void StartFalling()
    {
        _characterAnimatorController.StartFalling();
    }

    public static void EndFalling()
    {
        _characterAnimatorController.EndFalling();
    }
    #endregion

    #region Abilities - Animation Controller
    public static void StartThrowTongue()
    {
        _characterAnimatorController.StartThrowTongue();
    }

    public static void EndThrowTongue()
    {
        _characterAnimatorController.EndThrowTongue();
    }

    public static void StartHomingAttack()
    {
        _characterAnimatorController.StartHomingAttack();
    }

    public static void EndHomingAttack()
    {
        _characterAnimatorController.EndHomingAttack();
    }

    public static void StartDragging()
    {
        _characterAnimatorController.StartDragging();
    }

    public static void EndDragging()
    {
        _characterAnimatorController.EndDragging();
    }
    #endregion

    #region Health - Animation Controller
    public static void Hurt()
    {
        _characterAnimatorController.Hurt();
    }

    public static void IsDead()
    {
        _characterAnimatorController.IsDead();
    }
    #endregion
    #endregion
    #endregion
}
