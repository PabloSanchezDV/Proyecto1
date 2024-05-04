using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    private InputAction _move;

    private Rigidbody _rb;

    [Header("References")]
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private GameObject _shadow;
    [SerializeField] private LayerMask[] _groundLayers;

    [Header("Properties")]
    [SerializeField] private float _runForce = 1f;
    [SerializeField] private float _decelerationForce = 2f;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _cancelJumpForce = 2.5f;
    [SerializeField] private float _maxRunSpeed = 5f;
    [SerializeField] private float _maxWalkSpeed = 3f;
    [SerializeField] private float _maxDraggingSpeed = 3f;
    [SerializeField] private float _fallingSpeedMultiplier = 3f;
    [SerializeField] private float _coyoteTime = 0.2f;
    [SerializeField] private float _jumpBuffer = 0.2f;
    [SerializeField] private float _knockbackForce;
    [SerializeField] private float _knockbackVerticalForce;

    private Vector3 _forceDirection = Vector3.zero;

    private bool _isWalking = false;
    private bool _canDoubleJump = true;
    private bool _isFalling = false;

    private float _coyoteTimeCounter = 0f;
    private float _jumpBufferCounter = 0f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        CharacterManager.InputActions.Gameplay.Jump.started += Jump;
        CharacterManager.InputActions.Gameplay.Jump.canceled += CancelJump;
        CharacterManager.InputActions.Gameplay.Run.started += TurnOnWalk;
        CharacterManager.InputActions.Gameplay.Run.canceled += TurnOffWalk;
        _move = CharacterManager.InputActions.Gameplay.Move;
        CharacterManager.InputActions.Gameplay.Enable();
    }

    private void OnDisable()
    {
        CharacterManager.InputActions.Gameplay.Jump.started -= Jump;
        CharacterManager.InputActions.Gameplay.Run.started -= TurnOnWalk;
        CharacterManager.InputActions.Gameplay.Run.canceled -= TurnOffWalk;
        CharacterManager.InputActions.Gameplay.Disable();
    }

    private void FixedUpdate()
    {
        if (!CharacterManager.CanMove)
            return;

        //Apply forces
        if(_move.ReadValue<Vector2>().magnitude > 0f)
        {
            _forceDirection += _move.ReadValue<Vector2>().x * GetCameraRight(_playerCamera) * _runForce;
            _forceDirection += _move.ReadValue<Vector2>().y * GetCameraForward(_playerCamera) * _runForce;      
        }
        else
        {
            Vector3 velocityDirectionInversed = -_rb.velocity.normalized;
            _forceDirection += new Vector3(velocityDirectionInversed.x, 0, velocityDirectionInversed.z) * _decelerationForce;
        }
        _rb.AddForce(_forceDirection, ForceMode.Impulse);
        _forceDirection = Vector3.zero;

        //Increase falling speed
        if(_rb.velocity.y < 0f)
        {
            CharacterManager.EndElevating();
            _rb.velocity -= Vector3.down * Physics.gravity.y * Time.fixedDeltaTime * _fallingSpeedMultiplier;
        }
        else
        {
            CharacterManager.ResetElevating();
        }

        //Cap max speed
        Vector3 horizontalVelocity = _rb.velocity;
        horizontalVelocity.y = 0f;

        if (CharacterManager.IsDragging)
        {
            if (horizontalVelocity.sqrMagnitude > _maxDraggingSpeed * _maxDraggingSpeed)
            {
                _rb.velocity = horizontalVelocity.normalized * _maxDraggingSpeed + Vector3.up * _rb.velocity.y;
            }
        }
        else
        {
            if (IsRunning())
            {
                if (horizontalVelocity.sqrMagnitude > _maxRunSpeed * _maxRunSpeed)
                {
                    _rb.velocity = horizontalVelocity.normalized * _maxRunSpeed + Vector3.up * _rb.velocity.y;
                }
            }
            else
            {
                if (horizontalVelocity.sqrMagnitude > _maxWalkSpeed * _maxWalkSpeed)
                {
                    _rb.velocity = horizontalVelocity.normalized * _maxWalkSpeed + Vector3.up * _rb.velocity.y;
                }
            }
        }

        //Coyote Time, Jump Buffer & Falling Checks
        if(IsGrounded())
        {
            if (_isFalling)
                EndFall();

            if(_rb.velocity.y < 0f)
            {
                _coyoteTimeCounter = _coyoteTime;
            }
            if (_jumpBufferCounter > 0f)
            {
                _jumpBufferCounter = 0f;
                _canDoubleJump = true;
                DoJump();
            }

            if(!CharacterManager.IsThrowingTongue)
                CharacterManager.CanThrowTongue = true;
        }
        else
        {
            if (!_isFalling)
               StartFall();            

            _coyoteTimeCounter -= Time.fixedDeltaTime;
            _jumpBufferCounter -= Time.fixedDeltaTime;
        }
    }

    private void LateUpdate()
    {
        Ray ray = new Ray(this.transform.position + Vector3.up * 0.25f, Vector3.down);
        foreach (LayerMask layer in _groundLayers)
        {
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layer))
            {
                _shadow.transform.position = hit.point + new Vector3(0, 0.001f, 0);
                float distanceToGround = Map(transform.position.y - _shadow.transform.position.y, 0f, 10f, 0.025f, 0.01f);
                _shadow.transform.localScale = new Vector3(distanceToGround, distanceToGround, 1);
            }
        }
    }

    private Vector3 GetCameraForward(Camera playerCamera)
    {
        Vector3 forward = playerCamera.transform.forward;
        forward.y = 0f;
        return forward.normalized;
    }

    private Vector3 GetCameraRight(Camera playerCamera)
    {
        Vector3 right = playerCamera.transform.right;
        right.y = 0f;
        return right.normalized;
    }

    private void Jump(InputAction.CallbackContext context)
    {
        _jumpBufferCounter = _jumpBuffer;
        if (IsGrounded() || _coyoteTimeCounter > 0f)
        {
            _coyoteTimeCounter = 0f;
            _jumpBufferCounter = 0f;

            if (!_canDoubleJump)
                _canDoubleJump = true;

            CharacterManager.Jump();
            DoJump();
        }
        else if(_canDoubleJump)
        {
            _canDoubleJump = false;
            CharacterManager.DoubleJump();
            DoJump();
        }
    }

    private void DoJump()
    {
        _rb.velocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
        _forceDirection += Vector3.up * _jumpForce;
    }

    public void JumpOnHomingAttack()
    {
        _rb.velocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
        _forceDirection += Vector3.up * _jumpForce;
        _canDoubleJump = true;
    }

    private void CancelJump(InputAction.CallbackContext context)
    {
        _forceDirection -= Vector3.up * _cancelJumpForce;
    }

    public void DisableJumpOnStartDragging()
    {
        CharacterManager.InputActions.Gameplay.Jump.started -= Jump;
    }

    public void EnableJumpOnEndDragging()
    {
        CharacterManager.InputActions.Gameplay.Jump.started += Jump;
    }

    private bool IsGrounded()
    {
        //Debug.DrawRay(this.transform.position + Vector3.up * 0.25f, Vector3.down, Color.red);
        Ray ray = new Ray(this.transform.position + Vector3.up * 0.25f, Vector3.down);
        bool isGrounded = false;
        foreach(LayerMask layer in _groundLayers)
        {
            if (Physics.Raycast(ray, out RaycastHit hit, 0.3f, layer))
            {
                isGrounded = true; 
                break;
            }
            else
            {
                isGrounded = false;
            }
        }
        return isGrounded;
    }

    private float Map(float x, float in_min, float in_max, float out_min, float out_max)
    {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }

    private bool IsRunning()
    {
        if(_move.ReadValue<Vector2>().magnitude <= 0.5f)
        {
            return false;
        }
        else
        {
            if(_isWalking)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    private void TurnOnWalk(InputAction.CallbackContext context)
    {
        _isWalking = true;
    }

    private void TurnOffWalk(InputAction.CallbackContext context)
    {
        _isWalking = false;
    }

    private void StartFall()
    {
        _isFalling = true;
        CharacterManager.StartFalling();
    }

    private void EndFall()
    {
        _isFalling = false;
        CharacterManager.EndFalling();
    }

    public void KnockbackCharacter(Transform hittingEnemy)
    {
        Vector3 direction = transform.position - hittingEnemy.position;
        direction.y = 0;
        _rb.AddForce(direction.normalized * _knockbackForce, ForceMode.Impulse);
        _rb.AddForce(Vector3.up *  _knockbackVerticalForce, ForceMode.Impulse);
    }
}
