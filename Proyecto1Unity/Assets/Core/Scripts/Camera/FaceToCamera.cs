using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceToCamera : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] private Vector3 _offset;

    private void Start()
    {
        _camera = FindFirstObjectByType<Camera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (CharacterManager.CanMove)
        {   
            transform.LookAt(_camera.transform.position);
            transform.Rotate(_offset, Space.Self);
        }
    }
}
