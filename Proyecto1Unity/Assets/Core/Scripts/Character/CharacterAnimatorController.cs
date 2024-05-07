using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterAnimatorController : MonoBehaviour
{
    private Animator _anim;
    private InputAction _move;

    [NonSerialized] public CharacterManager characterManager;

    private void Start()
    {
        _anim = transform.GetChild(0).GetChild(0).GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _move = characterManager.InputActions.Gameplay.Move;
        characterManager.InputActions.Gameplay.Bite.performed += Hit;
    }

    private void OnDisable()
    {
        characterManager.InputActions.Gameplay.Bite.performed -= Hit;
    }

    private void Update()
    {
        CheckMovement();
    }

    private void CheckMovement()
    {
        if(_move != null)
        {
            float moveMagnitude = _move.ReadValue<Vector2>().magnitude;
            if (moveMagnitude <= 0f)
            {
                if (_anim.GetBool("isMoving"))
                    _anim.SetBool("isMoving", false);
            }
            else
            {
                if (!_anim.GetBool("isMoving"))
                    _anim.SetBool("isMoving", true);
                if (moveMagnitude > 0.5f)
                {
                    if (_anim.GetBool("isWalking"))
                        _anim.SetBool("isWalking", false);
                }
                else
                {
                    if (!_anim.GetBool("isWalking"))
                        _anim.SetBool("isWalking", true);
                }
            }

        }
    }

    public void Jump()
    {
        _anim.SetTrigger("Jump");
    }

    public void DoubleJump()
    {
        _anim.SetTrigger("DoubleJump");
    }

    public void EndElevating()
    {
        _anim.SetTrigger("EndElevating");
    }

    public void ResetElevating()
    {
        _anim.ResetTrigger("EndElevating");
    }

    public void StartFalling()
    {
        _anim.SetTrigger("StartFalling");
    }

    public void EndFalling()
    {
        _anim.ResetTrigger("StartFalling");
        _anim.ResetTrigger("Hit");
        _anim.SetTrigger("EndFalling");
    }

    public void StartThrowTongue()
    {
        _anim.SetBool("isThrowingTongue", true);
    }

    public void EndThrowTongue()
    {
        _anim.ResetTrigger("Hit");
        _anim.SetBool("isThrowingTongue", false);
    }

    public void StartHomingAttack()
    {
        _anim.SetBool("isHomingAttacking", true);
    }

    public void EndHomingAttack()
    {
        _anim.ResetTrigger("EndElevating");
        _anim.SetBool("isThrowingTongue", false);
        _anim.SetBool("isHomingAttacking", false);
    }

    public void StartDragging()
    {
        _anim.SetBool("isDragging", true);
    }

    public void EndDragging()
    {
        _anim.SetBool("isDragging", false);
    }

    private void Hit(InputAction.CallbackContext context)
    {
        _anim.SetTrigger("Hit");
    }

    public void Hurt()
    {
        _anim.SetTrigger("Hurt");
    }

    public void IsDead()
    {
        _anim.SetTrigger("Die");
    }

    public void Respawn()
    {
        _anim.SetTrigger("Respawn");
    }
}
