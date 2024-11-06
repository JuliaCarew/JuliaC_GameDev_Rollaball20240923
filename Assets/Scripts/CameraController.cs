using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public GameObject VCamGameplay;
    //public GameObject VCamMainMenu;

    public CinemachineBrain cinemachineBrain;

    public CinemachineFreeLook freeLook;
    public Transform lookAt;

    public Vector3 cameraPositionOffset = new Vector3(0, 5, -10);

    public void EnableCameraRotation()
    {
        VCamGameplay.SetActive(true);
    }
    public void SetCameraOrientation(Vector3 targetOrientation)
    {
        VCamGameplay.transform.LookAt(targetOrientation);
    }
}
