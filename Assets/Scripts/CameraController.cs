using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    public float sensitivity;
    public float lookXLimit = 45.0f;

    private void FixedUpdate()
    {
        float rotateHorizontal = Input.GetAxis("Mouse X");
        float rotateVertical = Input.GetAxis("Mouse Y");
        transform.RotateAround(player.transform.position, -Vector3.up, rotateHorizontal * sensitivity); 
        transform.RotateAround(Vector3.zero, transform.right, rotateVertical * sensitivity); 
    }
}
