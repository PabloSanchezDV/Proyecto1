using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public static CameraSwitcher instance;

    private CinemachineVirtualCameraBase[] _virtualCameras;
    public CinemachineVirtualCameraBase[] VirtualCameras {  get { return _virtualCameras; } }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        EventHolder.instance.onEndDialogue.AddListener(ResetPlayerCamera);

        GameObject[] vCameraGOs = GameObject.FindGameObjectsWithTag("VirtualCamera");
        _virtualCameras = new CinemachineVirtualCameraBase[vCameraGOs.Length];
        for(int i = 0; i < vCameraGOs.Length; i++)
        {
            _virtualCameras[i] = vCameraGOs[i].GetComponent<CinemachineVirtualCameraBase>();
        }
    }

    public void SwitchCamera(CinemachineVirtualCameraBase virtualCamera, Transform lookAt = null)
    {
        foreach(var vCamera in _virtualCameras)
        {
            if(vCamera == virtualCamera)
            {
                vCamera.enabled = true;
                if(lookAt != null)
                    vCamera.LookAt = lookAt;
                continue;

            }
            vCamera.enabled = false;
        }
    }

    public void ResetPlayerCamera()
    {
        SwitchCamera(GameManager.instance.PlayerCamera, GameManager.instance.Player.transform);
    }
}
