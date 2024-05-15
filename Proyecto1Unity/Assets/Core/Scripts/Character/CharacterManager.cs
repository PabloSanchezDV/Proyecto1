using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private CinemachineInputProvider _inputProvider;
    public CinemachineInputProvider InputProvider { get { return _inputProvider; } }

    private bool _canMove = true;
    public bool CanMove { get { return _canMove; } set { _canMove = value; } }

    private bool _isDragging = false;
    public bool IsDragging { get { return _isDragging; } set { _isDragging = value; } }

    private bool _canThrowTongue = true;
    public bool CanThrowTongue { get { return _canThrowTongue; } set { _canThrowTongue = value; } }

    private bool _isThrowingTongue = false;
    public bool IsThrowingTongue { get { return _isThrowingTongue; } set { _isThrowingTongue = value; } }

    private EnemyAnimatorController _enemyAnimatorControllerOnHomingAttack;
    public EnemyAnimatorController EnemyAnimatorControllerOnHomingAttack { get { return _enemyAnimatorControllerOnHomingAttack; } set { _enemyAnimatorControllerOnHomingAttack = value; } }

    
    private Transform _hittingEnemy;
    public Transform HittingEnemy { set { _hittingEnemy = value; } }

    private InputActions _inputActions;
    public InputActions InputActions { get { return _inputActions;} }
       
    private CharacterMovement _characterMovement;
    private CharacterAbilities _characterAbilities;
    private CharacterAnimatorController _characterAnimatorController;
    private CharacterDamage _characterDamage;
    #endregion

    // Start is called before the first frame update
    void Awake()
    {
        _inputActions = new InputActions();
        _characterMovement = GetComponent<CharacterMovement>();
        _characterAbilities = GetComponent<CharacterAbilities>();
        _characterAnimatorController = GetComponent<CharacterAnimatorController>();
        _characterDamage = GetComponent<CharacterDamage>();

        _characterMovement.characterManager = this;
        _characterAbilities.characterManager = this;
        _characterAnimatorController.characterManager = this;
        _characterDamage.characterManager = this;

        _characterMovement.enabled = true;
        _characterAbilities.enabled = true;
        _characterAnimatorController.enabled = true;
        _characterDamage.enabled = true;
    }

    private void Start()
    {
        _inputActions.Gameplay.Pause.started += Pause;
        _inputActions.Menu.ClosePause.started += UnpauseByInput;
        _inputActions.Dialogue.NextDialogue.started += NextDialogue;
        
        EventHolder.instance.onUnpause.AddListener(Unpause);
        EventHolder.instance.onHurt.AddListener(ActivateHurt);
        EventHolder.instance.onDeath.AddListener(ActivateDeath);
        EventHolder.instance.onRespawn.AddListener(ActivateMovementOnRespawn);
        EventHolder.instance.onRespawn.AddListener(GoToIdleAfterRespawn);
        EventHolder.instance.onStartDialogue.AddListener(StopMovement);
        EventHolder.instance.onStartDialogue.AddListener(ChangeInputModeToDialogue);
        EventHolder.instance.onEndDialogue.AddListener(ChangeInputModeToGamePlay);
        EventHolder.instance.onEndDialogue.AddListener(ResumeMovement);

        GameManager.instance.Player = gameObject;
    }

    private void OnEnable()
    {
        _inputActions.Gameplay.Pause.started += Pause;
        _inputActions.Menu.ClosePause.started += UnpauseByInput;
        _inputActions.Dialogue.NextDialogue.started += NextDialogue;
        if (EventHolder.instance != null)
        {
            EventHolder.instance.onUnpause.AddListener(Unpause);
            EventHolder.instance.onHurt.AddListener(ActivateHurt);
            EventHolder.instance.onDeath.AddListener(ActivateDeath);
            EventHolder.instance.onRespawn.AddListener(ActivateMovementOnRespawn);
            EventHolder.instance.onRespawn.AddListener(GoToIdleAfterRespawn);
            EventHolder.instance.onStartDialogue.AddListener(StopMovement);
            EventHolder.instance.onStartDialogue.AddListener(ChangeInputModeToDialogue);
            EventHolder.instance.onEndDialogue.AddListener(ChangeInputModeToGamePlay);
            EventHolder.instance.onEndDialogue.AddListener(ResumeMovement);
        }
    }

    private void OnDisable()
    {
        _inputActions.Gameplay.Pause.started -= Pause;
        _inputActions.Menu.ClosePause.started -= UnpauseByInput;
        _inputActions.Dialogue.NextDialogue.started -= NextDialogue;
        EventHolder.instance.onUnpause.RemoveListener(Unpause);
        EventHolder.instance.onHurt.RemoveListener(ActivateHurt);
        EventHolder.instance.onDeath.RemoveListener(ActivateDeath);
        EventHolder.instance.onRespawn.RemoveListener(ActivateMovementOnRespawn);
        EventHolder.instance.onRespawn.RemoveListener(GoToIdleAfterRespawn);
        EventHolder.instance.onStartDialogue.RemoveListener(StopMovement);
        EventHolder.instance.onStartDialogue.RemoveListener(ChangeInputModeToDialogue);
        EventHolder.instance.onEndDialogue.RemoveListener(ChangeInputModeToGamePlay);
        EventHolder.instance.onEndDialogue.RemoveListener(ResumeMovement);
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

    private void ResumeMovement()
    {
        _canMove = true;
    }

    private void StopMovement()
    {
        _canMove = false;
        _characterMovement.StopMovement();
    }

    private void ChangeInputModeToDialogue()
    {
        _inputActions.Gameplay.Disable();
        _inputActions.Dialogue.Enable();
    }

    private void ChangeInputModeToGamePlay()
    {
        _inputActions.Dialogue.Disable();
        _inputActions.Gameplay.Enable();
    }

    private void NextDialogue(InputAction.CallbackContext context)
    {
        if (!DialogueManager.instance.DoNextDialogue)
            DialogueManager.instance.DoNextDialogue = true;
    }

    #region Communications
    #region Movement - Abilities
    public void JumpOnHomingAttack()
    {
        _characterMovement.JumpOnHomingAttack();
    }

    public void EnableJumpOnEndDragging()
    {
        _characterMovement.EnableJumpOnEndDragging();
    }

    public void DisableJumpOnStartDragging()
    {
        _characterMovement.DisableJumpOnStartDragging();
    }
    #endregion

    #region Animation Communications
    #region Movement - Animation Controller
    public void Jump()
    {
        if(_characterAnimatorController != null)
            _characterAnimatorController.Jump();
    }

    public void DoubleJump()
    {
        if (_characterAnimatorController != null)
            _characterAnimatorController.DoubleJump();
    }

    public void EndElevating()
    {
        if (_characterAnimatorController != null)
            _characterAnimatorController.EndElevating();
    }

    public void ResetElevating()
    {
        if (_characterAnimatorController != null)
            _characterAnimatorController.ResetElevating();
    }

    public void StartFalling()
    {
        if (_characterAnimatorController != null)
            _characterAnimatorController.StartFalling();
    }

    public void EndFalling()
    {
        if (_characterAnimatorController != null)
            _characterAnimatorController.EndFalling();
    }
    #endregion

    #region Abilities - Animation Controller
    public void StartThrowTongue()
    {
        if (_characterAnimatorController != null)
            _characterAnimatorController.StartThrowTongue();
    }

    public void EndThrowTongue()
    {
        if (_characterAnimatorController != null)
            _characterAnimatorController.EndThrowTongue();
    }

    public void StartHomingAttack()
    {
        if (_characterAnimatorController != null)
            _characterAnimatorController.StartHomingAttack();
    }

    public void EndHomingAttack()
    {
        if (_characterAnimatorController != null)
            _characterAnimatorController.EndHomingAttack();
        if(_enemyAnimatorControllerOnHomingAttack != null)
            _enemyAnimatorControllerOnHomingAttack.Death();
    }

    public void StartDragging()
    {
        if (_characterAnimatorController != null)
            _characterAnimatorController.StartDragging();
    }

    public void EndDragging()
    {
        if (_characterAnimatorController != null)
            _characterAnimatorController.EndDragging();
    }
    #endregion
    #endregion
    #endregion

    private void ActivateHurt()
    {
        if (_characterAnimatorController != null)
            _characterAnimatorController.Hurt();
        if(_characterDamage != null)
            _characterDamage.ActivateInvincibilityFrames();
        if (_characterMovement != null)
            _characterMovement.KnockbackCharacter(_hittingEnemy);
    }

    private void ActivateDeath()
    {
        _inputProvider.enabled = false;
        _canMove = false;
        if (_characterAnimatorController != null)
            _characterAnimatorController.IsDead();
    }

    private void ActivateMovementOnRespawn()
    {
        _canMove = true;
        _inputProvider.enabled = true;
    }

    private void GoToIdleAfterRespawn()
    {
        if (_characterAnimatorController != null)
            _characterAnimatorController.Respawn();
    }

    public void ActivateTalkInput(DialogueTrigger dialogueTrigger)
    {
        _inputActions.Gameplay.Talk.started += dialogueTrigger.Talk;
    }

    public void DeactivateTalkInput(DialogueTrigger dialogueTrigger)
    {
        _inputActions.Gameplay.Talk.started -= dialogueTrigger.Talk;
    }
}
