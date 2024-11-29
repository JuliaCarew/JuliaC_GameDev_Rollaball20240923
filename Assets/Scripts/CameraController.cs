using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
   
    public GameObject player;

    void OrbitCamera()
    {
        Vector3 playerPos = new Vector3(0, 2, -27);
        transform.RotateAround(playerPos, Vector3.up, Input.GetAxis("Mouse X"));
    }
   
    private void Update()
    {
        OrbitCamera();
    }
}
