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
    public GameObject ball;
    public Rigidbody rb_ball;
    public GameObject BowlingPin;
    public GameObject Aim;
    // private Transform Aim;
    // UI
    public GameObject winTextObject;
    public GameObject holdKeyObject;

    // Variables
    public bool ballStopped;
    public float ballMagnitudeStopThreshold = 0.1f;
    public float ballStopCheckDelay = 0.5f;
    public float speed; // = Vector3.Magnitude(rb_ball.velocity); !!!
    Vector3 lastPosition = Vector3.zero;
    
    // hold key variables
    public bool heldKey;
    public float holdDuration = 0;

    private Vector3 scaleChange, positionChange;

    private void Start()
    {
        ballStopped = true;

        winTextObject.SetActive(false);
        holdKeyObject.SetActive(false);

        Transform Aim = GameObject.FindWithTag("Aim").transform;

    }
    void Update()
    {
        // ballVelocityMagnitude = rb_ball.velocity.magnitude;
        ballShoot();
       /* if (throws == 2) // reset pins & ball, move to next frame
        {
            ResetAll();
        }
        if (frame == 10) // When you play 10 frames, add score + show accuracy
        {
            GameOver();
        }*/
        lastPosition = transform.position;
    }
    private void OnDrawGizmos()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {   // reset ball position if out of bounds
        if(other.gameObject.tag == "OutOfBounds")
        {
            Debug.Log("Out of Bounds");
            SetBallToStart();
            ballStopped = true;
            //throws++;         
        }        
    }
    /*void PlayerInput()
    {
        //recieve player's input from axis position of mouse
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0 , Input.GetAxisRaw("Vertical"));
    }*/
    private void ballShoot()
    {
        //recieve player's input from axis position of mouse
       // Vector3 shootRotation = new Vector3
        //    (Input.GetAxisRaw("Horizontal"), 0,0);

        // get variable input from PlayerInput()
        // set transform.rotation/forward/LookAt to input's vector
       // rb_ball.transform.forward = shootRotation;

        // shoot direction needs to take input at the X rotation, 0, 0 ??
        // need to make work so the ball's forward position is the mouse position
        //Vector3 shootDirection = new Vector3(shootRotation, 0,0);

        Aim.gameObject.SetActive(false);
        rb_ball.isKinematic = false;
        
        // shoot ball is press space
        if (Input.GetKeyDown("space") && holdDuration < 5f)
        {
            ballStopped = false;
            rb_ball.AddForce(new Vector3(0, 0, 50), ForceMode.Impulse);
            rb_ball.AddForce(Aim.transform.forward * 25, ForceMode.VelocityChange);
            //heldKey = false;
        }
        //set up hold shoot - display ui w it
        else 
        {
            ballStopped = true;
            heldKey = true;
            //holdKeyObject.SetActive(true);
            //Add multiplicative force to rb_ball * holdDuration ? 
        }
        //if(speed < 1f) !!!
        //{
        //    SetBallToStart();
        //}
    }
    public void SetBallToStart() //not triggering
    {
        StopBall();
        Transform startPosition = GameObject.FindWithTag("StartPosition").transform;
        rb_ball.transform.position = startPosition.transform.position;
        rb_ball.transform.rotation = startPosition.transform.rotation;
        //ballStopped = true;
    }
    public void StopBall()
    {
        rb_ball.isKinematic = true;
        rb_ball.isKinematic = false;

        rb_ball.velocity = Vector3.zero;
        rb_ball.angularVelocity = Vector3.zero;

        rb_ball.Sleep();
        Debug.Log("ballStopped = true");
        
    }
    public void ResetAll()
    { // delete all pins
        SetBallToStart();
        var Pins = GameObject.FindGameObjectsWithTag("Pin");
        foreach (GameObject BowlingPin in Pins)
        {
            BowlingPin.SetActive(false);
        }
        //Vector3 orginalPosition = GameObject.FindWithTag("Pin").transform.position;
        //gameObject.transform.position = orginalPosition;
        //throws = 0;
        //frame++;
    }
    public void GameOver()
    {
        ResetAll();
        winTextObject.SetActive(true);       
    }
}
//if ball velocity reaches certain point, reset position (it gets too slow before reset is recognized)