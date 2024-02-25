using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCameraAroundPlayer : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [Header("Properties")]
    [SerializeField] public float _sensitivity;

    [NonSerialized] public float _rotX, _rotY;
    private Vector3 _offset;


    // Start is called before the first frame update
    void Start()
    {
        _offset = transform.position - _player.position;
    }

    // Update is called once per frame
    void Update()
    {
        _rotX = Input.GetAxis("Mouse X");
        _rotY = Input.GetAxis("Mouse Y");
        
    }

    void LateUpdate()
    {
        transform.RotateAround(_player.position, -Vector3.up, -_rotX * _sensitivity * Time.deltaTime);
        transform.RotateAround(_player.position, transform.right, _rotY * _sensitivity * Time.deltaTime);
    }
}
