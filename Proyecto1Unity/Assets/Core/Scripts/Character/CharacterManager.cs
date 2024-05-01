using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    private static EnemyAnimatorController _enemyAnimatorControllerOnHomingAttack;
    public static EnemyAnimatorController EnemyAnimatorControllerOnHomingAttack { get { return _enemyAnimatorControllerOnHomingAttack; } set { _enemyAnimatorControllerOnHomingAttack = value; } }


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

        _inputActions.Gameplay.Pause.started += Pause;
        _inputActions.Menu.ClosePause.started += UnpauseByInput;
    }

    private void Start()
    {
        EventHolder.instance.onUnpause.AddListener(Unpause);
    }

    private void OnEnable()
    {
        _inputActions.Gameplay.Pause.started += Pause;
    }

    private void OnDisable()
    {
        _inputActions.Gameplay.Pause.started -= Pause;
    }

    private void Pause(InputAction.CallbackContext context)
    {
        _inputActions.Gameplay.Disable();
        _inputActions.Menu.Enable();
        EventHolder.instance.onPause?.Invoke();
    }

    private void Unpause()
    {
        _inputActions.Gameplay.Enable();
        _inputActions.Menu.Disable();
    }
    private void UnpauseByInput(InputAction.CallbackContext context)
    {
        Unpause();
        EventHolder.instance.onUnpause?.Invoke();
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
        if(_characterAnimatorController != null)
            _characterAnimatorController.Jump();
    }

    public static void DoubleJump()
    {
        if (_characterAnimatorController != null)
            _characterAnimatorController.DoubleJump();
    }

    public static void EndElevating()
    {
        if (_characterAnimatorController != null)
            _characterAnimatorController.EndElevating();
    }

    public static void ResetElevating()
    {
        if (_characterAnimatorController != null)
            _characterAnimatorController.ResetElevating();
    }

    public static void StartFalling()
    {
        if (_characterAnimatorController != null)
            _characterAnimatorController.StartFalling();
    }

    public static void EndFalling()
    {
        if (_characterAnimatorController != null)
            _characterAnimatorController.EndFalling();
    }
    #endregion

    #region Abilities - Animation Controller
    public static void StartThrowTongue()
    {
        if (_characterAnimatorController != null)
            _characterAnimatorController.StartThrowTongue();
    }

    public static void EndThrowTongue()
    {
        if (_characterAnimatorController != null)
            _characterAnimatorController.EndThrowTongue();
    }

    public static void StartHomingAttack()
    {
        if (_characterAnimatorController != null)
            _characterAnimatorController.StartHomingAttack();
    }

    public static void EndHomingAttack()
    {
        if (_characterAnimatorController != null)
            _characterAnimatorController.EndHomingAttack();
        if(_enemyAnimatorControllerOnHomingAttack != null)
            _enemyAnimatorControllerOnHomingAttack.Death();
    }

    public static void StartDragging()
    {
        if (_characterAnimatorController != null)
            _characterAnimatorController.StartDragging();
    }

    public static void EndDragging()
    {
        if (_characterAnimatorController != null)
            _characterAnimatorController.EndDragging();
    }
    #endregion

    #region Health - Animation Controller
    public static void Hurt()
    {
        if (_characterAnimatorController != null)
            _characterAnimatorController.Hurt();
    }

    public static void IsDead()
    {
        if (_characterAnimatorController != null)
            _characterAnimatorController.IsDead();
    }
    #endregion
    #endregion
    #endregion
}
