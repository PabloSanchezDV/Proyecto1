using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignWithCamera : MonoBehaviour
{
    [SerializeField] private RotateCameraAroundPlayer _camera;

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position, -Vector3.up, -_camera._rotX * _camera._sensitivity * Time.deltaTime);
    }
}
