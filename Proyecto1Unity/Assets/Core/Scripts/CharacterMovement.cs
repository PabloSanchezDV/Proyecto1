using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    private InputActions _inputActions;
    private InputAction _move;

    private Rigidbody _rb;

    [Header("References")]
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private GameObject _shadow;

    [Header("Properties")]
    [SerializeField] private float _runForce = 1f;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _cancelJumpForce = 2.5f;
    [SerializeField] private float _maxRunSpeed = 5f;
    [SerializeField] private float _maxWalkSpeed = 3f;
    [SerializeField] private float _fallingSpeedMultiplier = 3f;
    [SerializeField] private float _coyoteTime = 0.2f;
    [SerializeField] private float _jumpBuffer = 0.2f;

    private Vector3 _forceDirection = Vector3.zero;
    private LayerMask _ground;

    private bool _isWalking = false;
    private bool _canDoubleJump = true;

    private float _coyoteTimeCounter = 0f;
    private float _jumpBufferCounter = 0f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _inputActions = new InputActions();

        _ground = LayerMask.GetMask("Ground");
    }

    private void OnEnable()
    {
        _inputActions.Gameplay.Jump.started += Jump;
        _inputActions.Gameplay.Jump.canceled += CancelJump;
        _inputActions.Gameplay.Run.started += TurnOnWalk;
        _inputActions.Gameplay.Run.canceled += TurnOffWalk;
        _move = _inputActions.Gameplay.Move;
        _inputActions.Gameplay.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Gameplay.Jump.started -= Jump;
        _inputActions.Gameplay.Run.started -= TurnOnWalk;
        _inputActions.Gameplay.Run.canceled -= TurnOffWalk;
        _inputActions.Gameplay.Disable();
    }

    private void FixedUpdate()
    {
        //Apply forces
        _forceDirection += _move.ReadValue<Vector2>().x * GetCameraRight(_playerCamera) * _runForce;
        _forceDirection += _move.ReadValue<Vector2>().y * GetCameraForward(_playerCamera) * _runForce;      
        _rb.AddForce(_forceDirection, ForceMode.Impulse);
        _forceDirection = Vector3.zero;

        //Increase falling speed
        if(_rb.velocity.y < 0f)
        {
            _rb.velocity -= Vector3.down * Physics.gravity.y * Time.fixedDeltaTime * _fallingSpeedMultiplier;
        }

        //Cap max speed
        Vector3 horizontalVelocity = _rb.velocity;
        horizontalVelocity.y = 0f;
        if(IsRunning())
        {
            if(horizontalVelocity.sqrMagnitude > _maxRunSpeed * _maxRunSpeed) 
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

        //Coyote Time & Jump Buffer
        if(IsGrounded())
        {
            _coyoteTimeCounter = _coyoteTime;
            if (_jumpBufferCounter > 0f)
            {
                InputAction.CallbackContext ctx = new InputAction.CallbackContext();
                Jump(ctx);
                _jumpBufferCounter = 0f;
            }
        }
        else
        {
            _coyoteTimeCounter -= Time.fixedDeltaTime;
            _jumpBufferCounter -= Time.fixedDeltaTime;
        }
    }

    private void LateUpdate()
    {
        Ray ray = new Ray(this.transform.position + Vector3.up * 0.25f, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, _ground))
        {
            _shadow.transform.position = hit.point + new Vector3(0, 0.001f, 0);
            float distanceToGround = Map(transform.position.y - _shadow.transform.position.y, 0f, 3f, 0.01f, 0.025f);
            _shadow.transform.localScale = new Vector3(distanceToGround, distanceToGround, 1);
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
        if(IsGrounded() || _coyoteTimeCounter > 0f || _jumpBufferCounter > 0f)
        {
            if (!_canDoubleJump)
                _canDoubleJump = true;

            _rb.velocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
            _forceDirection += Vector3.up * _jumpForce;
            _coyoteTimeCounter = 0f;    
        }
        else if(_canDoubleJump)
        {
            _canDoubleJump = false;
            _rb.velocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
            _forceDirection += Vector3.up * _jumpForce;
        }
        else
        {
            _jumpBufferCounter = _jumpBuffer;
            Time.timeScale = 0.2f;
        }
    }

    private void CancelJump(InputAction.CallbackContext context)
    {
        _forceDirection -= Vector3.up * _cancelJumpForce;
    }

    private bool IsGrounded()
    {
        Ray ray = new Ray(this.transform.position + Vector3.up * 0.25f, Vector3.down);
        if(Physics.Raycast(ray, out RaycastHit hit, 0.3f, _ground))
        {
            return true;
        }
        else
        {
            return false;
        }
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
}
