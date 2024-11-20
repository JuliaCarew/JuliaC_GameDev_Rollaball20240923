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
    
    private void Start()
    {
        if (cameraRef == null)
        {
            cameraRef = Camera.main;
        }

        // Ensure critical references are assigned
        if (ball == null || rb_ball == null)
        {
            Debug.LogError("Ball or Rigidbody reference is missing!");
            return;
        }

        ballStopped = true;

        winTextObject?.SetActive(false);
        holdKeyObject?.SetActive(false);

    }
    void Update()
    {
         if (cameraRef == null || ball == null || rb_ball == null) return;

        AimAtMouse();
        ballShoot();
    }
    /// <summary>
    /// Rotates the player to face the mouse position in world space.
    /// </summary>
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
            Debug.Log("Out of Bounds");
            SetBallToStart();
            ballStopped = true;
        }        
    }
    /// <summary>
    /// Shoots the ball in the direction it's facing, with force based on hold duration.
    /// </summary>
    private void ballShoot()
    {
        if (Input.GetMouseButton(0)) // Hold left mouse button
        {
            holdDuration += Time.deltaTime;
            holdDuration = Mathf.Clamp(holdDuration, 0, maxHoldDuration);

            holdKeyObject?.SetActive(true); // Display holdKey UI
        }

        if (Input.GetMouseButtonUp(0)) // Release left mouse button
        {
            ballStopped = false;
            rb_ball.isKinematic = false;

            float force = holdDuration * forceMultiplier;
            rb_ball.AddForce(ball.transform.forward * force, ForceMode.Impulse);

            holdDuration = 0f; // Reset hold duration
            holdKeyObject?.SetActive(false);
        }

        Aim.gameObject.SetActive(false);
        rb_ball.isKinematic = false;
        
        // shoot ball is press space
        if (Input.GetKeyDown("space") && holdDuration < 5f)
        {
            ballStopped = false;
            rb_ball.AddForce(new Vector3(0, 0, 50), ForceMode.Impulse);
            rb_ball.AddForce(Aim.transform.forward * 25, ForceMode.VelocityChange);
        }
        //set up hold shoot - display ui w it
        else 
        {
            ballStopped = true;
        }
    }

    /// <summary>
    /// Resets the ball's position if its speed falls below the threshold.
    /// </summary>
    private void CheckBallSpeed()
    {
        if (!ballStopped && rb_ball.velocity.magnitude < ballMagnitudeStopThreshold)
        {
            //Debug.Log("CheckBallSpeed running, Resetting position.");
            SetBallToStart();
        }
    }

    /// <summary>
    /// Resets the ball's position and stops its motion.
    /// </summary>
    public void SetBallToStart() 
    {
        StopBall();
        Transform startPosition = GameObject.FindWithTag("StartPosition").transform;

        if (startPosition != null)
        {
            rb_ball.transform.position = startPosition.transform.position;
            rb_ball.transform.rotation = startPosition.transform.rotation;
        }
        
    }
    public void StopBall()
    {
        rb_ball.isKinematic = true;

        rb_ball.velocity = Vector3.zero;
        rb_ball.angularVelocity = Vector3.zero;

        Debug.Log("ballStopped = true");
        
    }
    public void ResetAll()
    {   // delete all pins
        SetBallToStart();
        var Pins = GameObject.FindGameObjectsWithTag("Pin");
        foreach (GameObject BowlingPin in Pins)
        {
            BowlingPin.SetActive(false);
        }
    }
    public void GameOver()
    {
        ResetAll();
        winTextObject.SetActive(true);       
    }
}
//if ball velocity reaches certain point, reset position (it gets too slow before reset is recognized)