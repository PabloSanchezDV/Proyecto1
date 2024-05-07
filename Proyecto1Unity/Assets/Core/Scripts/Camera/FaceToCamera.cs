using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceToCamera : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;

    private Camera _camera;
    private CharacterManager _characterManager;

    private void Start()
    {
        _camera = FindFirstObjectByType<Camera>();
        _characterManager = FindFirstObjectByType<CharacterManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.LookAt(_camera.transform.position);
        transform.Rotate(_offset, Space.Self);
    }
}
