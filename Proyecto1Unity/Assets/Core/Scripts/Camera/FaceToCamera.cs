using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceToCamera : MonoBehaviour
{
    private Camera _camera;

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
        }
    }
}
