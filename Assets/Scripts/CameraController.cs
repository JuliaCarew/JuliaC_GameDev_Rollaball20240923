using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    /*// clamping maximum look range
    float yMin = 50.0f;
    float yMax = 50.0f;
    // camera transform based on these variables
    public Transform lookAt;
    public Transform player;
    // variables to use in Vector3 equation
    public float distance = 8f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    public float sensitivity = 1.0f;
    Vector3 offset = new Vector3(0, 4, 0);
    Camera camera;
    Vector3 lookAtPosition = new Vector3(0, 2, -27);*/
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
    private void LateUpdate()
    {

      /*  // make the camera look at the designated position
        transform.LookAt(lookAtPosition);
        // make the camera look at the mouse position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.forward = (mousePos - transform.position).normalized;
        // getting current mouse axis, then position, & moving with sensitivity
        currentX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        currentY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        //transform the cameras X rotation based on the mouse position
        Vector3 directionToPlayer = (lookAt - transform.position);

        directionToPlayer = directionToPlayer.normalized;

        transform.forward = directionToPlayer;

        Vector3 directionFacing = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        transform.position = lookAt.position + offset + rotation * directionFacing;
        transform.LookAt(lookAt.position);*/
    }
}
