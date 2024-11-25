using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour
{
    // Important GameObjects
    public Camera cameraRef;
    public GameObject ball;
    public Rigidbody rb_ball;
    public GameObject Aim;

    // UI
    public GameObject winTextObject;
    public GameObject holdKeyObject;

    // Variables
    public bool ballStopped;
    public float ballMagnitudeStopThreshold = 0.1f;

    // hold key variables
    public float maxHoldDuration = 5f;
    public float forceMultiplier = 50f;
    public float holdDuration = 0f;
    // References
    public PinsController pinsController;
    public ScoreBoardController scoreBoardController;
     private PowerUps powerUps;
    
    private void Start()
    {
        ballStopped = true;

        winTextObject?.SetActive(false);
        holdKeyObject?.SetActive(false);
    }
    void Update()
    {
        AimAtMouse();
        ballShoot();
        CheckBallSpeed();
    }

    private void AimAtMouse()
    {
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = cameraRef.ScreenPointToRay(Input.mousePosition);

        if (groundPlane.Raycast(ray, out float rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            Vector3 direction = (point - ball.transform.position).normalized;

            // Rotate the ball to face the mouse position
            ball.transform.forward = new Vector3(direction.x, 0, direction.z);
        }
    }
    private void OnTriggerEnter(Collider other)
    {   // reset ball position if out of bounds
        if(other.gameObject.tag == "OutOfBounds")
        {
            //Debug.Log("Out of Bounds");
        }
        if (other.gameObject.tag == "PickUp")
        {
            //Debug.Log("Power up obtained!");
            //PowerUp(powerReturned);
        }
    }

    private void ballShoot()
    {
       if (Input.GetMouseButton(0)) // Hold left mouse button
        {
            //Debug.Log($"Set ball speed: {holdDuration}");
            holdDuration += Time.deltaTime;
            holdDuration = Mathf.Clamp(holdDuration, 0, maxHoldDuration);

            holdKeyObject?.SetActive(true);
            // TODO: get tween for holdkey UI
        }

        if (Input.GetMouseButtonUp(0)) // Release left mouse button
        {
            if (ballStopped)
            {
                //Debug.Log("Ball shot");
                ballStopped = false;
                rb_ball.isKinematic = false; 

                float force = holdDuration * forceMultiplier;
                rb_ball.AddForce(ball.transform.forward * force, ForceMode.Impulse);

                holdDuration = 0f;
                holdKeyObject?.SetActive(false);
            }
        }
    }

    private void CheckBallSpeed()
    {
        //Debug.Log("Check Ball Speed called");

        if (!ballStopped && rb_ball.velocity.magnitude < ballMagnitudeStopThreshold)
        {
            //Debug.Log("Ball slowed down, resetting...");
            ballStopped = true;
            Invoke(nameof(ResetBallAndPins), 2f); // Add a delay for resetting
        }
    }
    void ResetBallAndPins()
    {
        //Debug.Log("Reset Ball And Pins called");

        if (pinsController == null || scoreBoardController == null)
        {
            return;
        }

        // Reset the pins and ball before updating the score (wait a few seconds for pins to fall)
        pinsController.ResetPins();
        SetBallToStart();

        // Update the score after resetting
        scoreBoardController.UpdateScore(PinState.score);
    }
    public void SetBallToStart()
    {
        //Debug.Log("Set Ball To Start called");
        StopBall();
        Transform startPosition = GameObject.FindWithTag("StartPosition")?.transform;

        if (startPosition != null)
        {
            rb_ball.transform.position = startPosition.position;
            rb_ball.transform.rotation = startPosition.rotation;
        }
    }
    public void StopBall()
    {
        //Debug.Log("Stop Ball called");
        rb_ball.isKinematic = false; // Ensure it can respond to velocity changes
        rb_ball.velocity = Vector3.zero;
        rb_ball.angularVelocity = Vector3.zero;
        rb_ball.isKinematic = true; // Restore kinematic state if needed
        //Debug.Log("Ball stopped.");
    }
}