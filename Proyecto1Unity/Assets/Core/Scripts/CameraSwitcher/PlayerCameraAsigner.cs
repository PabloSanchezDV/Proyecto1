using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraAsigner : MonoBehaviour
{
    void Start()
    {
        GameManager.instance.PlayerCamera = GetComponent<CinemachineVirtualCameraBase>();
    }
}
