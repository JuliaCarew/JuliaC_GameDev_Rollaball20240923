using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    // Important GameObjects
    public GameObject ball;
    public Rigidbody rb_ball;
    public GameObject BowlingPin;
    public GameObject Aim;
    // private Transform Aim;
    // UI
    public TextMeshProUGUI ScoreSheet;
    public GameObject winTextObject;
    public GameObject holdKeyObject;
    public GameObject accuracyText;
    public GameObject TitleScreen;
    public GameObject StartButton;
    public GameObject QuitButton;
    public GameObject StartText;
    public GameObject QuitText;
    public GameObject TitleText;

    // Score UI
    [SerializeField] TextMeshProUGUI Frame1A;
    [SerializeField] TextMeshProUGUI Frame1B;
    [SerializeField] TextMeshProUGUI Frame1T;

    public GameObject ScoreBoardCanvas;
    // Variables
    public bool ballStopped;
    public float ballMagnitudeStopThreshold = 0.1f;
    public float ballStopCheckDelay = 0.5f;
    public float speed; // = Vector3.Magnitude(rb_ball.velocity); !!!
    Vector3 lastPosition = Vector3.zero;
    // score vriables
    public int score;
    public int frameTotal;
    public int throws;
    public int frame;
    // hold key variables
    public bool heldKey;
    public float holdDuration = 0;
    private float accuracy;
    public bool pinDown;

    private Vector3 scaleChange, positionChange;

    private void Start()
    {
        MenuUI();
        ballStopped = true;
        score = 0;
        throws = 0;
        winTextObject.SetActive(false);
        holdKeyObject.SetActive(false);

        Transform Aim = GameObject.FindWithTag("Aim").transform;

        pinDown = false;
        if (Input.GetKeyDown("enter"))
        {
            StartGame();
        }
    }
    void Update()
    {
        // ballVelocityMagnitude = rb_ball.velocity.magnitude;
        ballShoot();
        ScoreUpdate();
        if (throws == 2) // reset pins & ball, move to next frame
        {
            ResetAll();
        }
        if (frame == 10) // When you play 10 frames, add score + show accuracy
        {
            GameOver();
        }
        lastPosition = transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {   // reset ball position if out of bounds
        if(other.gameObject.tag == "OutOfBounds")
        {
            Debug.Log("Out of Bounds");
            SetBallToStart();
            ballStopped = true;
            throws++;         
        }        
    }
    private void OnCollisionEnter(Collision collision)
    {   // if collide w pin, check if pin is down (++score)
        if (collision.gameObject.tag == "Pin")
        {
            Debug.Log("Hit pin");
            pinDown = true;
            score++;
        }
    }
    private void ballShoot()
    {
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
        throws = 0;
        frame++;
    }
    public void GameOver()
    {
        ResetAll();
        winTextObject.SetActive(true);
        accuracy = (score / 300) * 100;
        //accuracyText = accuracy; // need to convert the float to gameobject ??
        accuracyText.SetActive(true);
    }
    public void AimGuide()
    {
        // Get the direction the camera is facing, constrained to the Y-axis only
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0; 

        // Calculate the new rotation that matches the camera's facing direction
        Quaternion targetRotation = Quaternion.LookRotation(cameraForward);

        // Smoothly interpolate between the current rotation and the target rotation
        Aim.transform.rotation = Quaternion.Slerp(Aim.transform.rotation, targetRotation, Time.deltaTime * 50f);
    }
    public void ScoreUpdate()
    {
        // get char from current frame
        // replace char with current score
        //move to next char

        //parse string to convert it to int to be added later
        static int ParseString(string Frame1A)
        {
            int num = 0;
            num = int.Parse(Frame1A);
            return num;
        }

        Transform found = ScoreBoardCanvas.transform.Find("FrameScores/Frame 2/Frame2A");
        if (found && found.TryGetComponent<TextMeshProUGUI>(out var frametext)) {
            frametext.text = "0";
        }
        //Frame1A.text = $"{score}";
        //Frame1B.text = $"{score}";
        //// int frametotal is frame A + B
        ////frameTotal = Frame1A + Frame1B;
        //// other frametotals are previous frametotal plus current frametotal
        //Frame1T.text = $"{score}";
    }
    public void MenuUI()
    {
        TitleScreen.SetActive(true);
        StartText.SetActive(true);
        QuitText.SetActive(true);
        TitleText.SetActive(true);
        StartButton.SetActive(true);
        QuitButton.SetActive(true);

        Debug.Log("MenuUI()");
    }
    public void StartGame()
    {
        TitleScreen.SetActive(false);
        StartText.SetActive(false);
        QuitText.SetActive(false);
        TitleText.SetActive(false);
        StartButton.SetActive(false);
        QuitButton.SetActive(false);

        Debug.Log("StartGame()");
    }
}
//if ball velocity reaches certain point, reset position (it gets too slow before reset is recognized)