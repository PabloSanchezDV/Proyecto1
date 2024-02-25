using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private Transform _camera;

    [Header("Properties")]
    [SerializeField] private float _movSpeed;
    [SerializeField] private float _jumpForce;

    private Rigidbody _rb;
    private Vector3 _inputVector;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _inputVector = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            _inputVector += _camera.transform.forward;
        }
        else if(Input.GetKey(KeyCode.S))
        {
            _inputVector -= _camera.transform.forward;
        }
        if(Input.GetKey(KeyCode.A))
        {
            _inputVector -= _camera.transform.right;
        }
        else if(Input.GetKey(KeyCode.D))
        {
            _inputVector += _camera.transform.right;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        if(_rb.isKinematic) { return; }

        _rb.velocity = new Vector3(_inputVector.normalized.x * _movSpeed * Time.deltaTime,
                                   _rb.velocity.y,
                                   _inputVector.normalized.z * _movSpeed * Time.deltaTime);
    }

    public void Jump()
    {
        _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }
}
