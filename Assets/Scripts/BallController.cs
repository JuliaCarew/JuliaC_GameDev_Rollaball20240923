using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public GameObject ball;
    public Rigidbody rb_ball;
    public GameObject aimGuide;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject holdKeyObject;
    public bool ballStopped;
    public float speed = 0; 
    private int score;
    private float ballVeloityMagnitude;
    public bool heldKey;
    public float holdDuration = 0;

    private Vector3 scaleChange, positionChange;

    // Start is called before the first frame update
    private void Start()
    {
        ballStopped = true;
        score = 0;
        SetScoreText();
        winTextObject.SetActive(false);
        holdKeyObject.SetActive(false); //somehow always displaying??
    }
    // Update is called once per frame
    void Update()
    {
        //ballVeloityMagnitude = rb_ball.velocity.magnitude;
        ballShoot();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "OutOfBounds")
        {
            Debug.Log("Out of Bounds");
            SetBallToStart();
        }
    }
    private void ballShoot()
    {
        if (Input.GetKeyDown("space") && holdDuration < 5f)
        {
            ballStopped = false;
            //rb_ball.AddForce(aimGuide.transform.forward * 25, ForceMode.VelocityChange);
            rb_ball.AddForce(new Vector3(0, 0, 30), ForceMode.Impulse);
            heldKey = false;
        }
        //set up hold shoot - display ui w it
        else 
        {
            ballStopped = false;
            heldKey = true;
            holdKeyObject.SetActive(true);
            //Add multiplicative force to rb_ball * holdDuration ? 
        }
    }
    void SetScoreText()
    {
        countText.text = "Count: " + score.ToString();
        if (score >= 8)
        {
            winTextObject.SetActive(true);
        }
    }
    public void SetBallToStart() //not triggering
    {
        Transform startPosition = GameObject.FindWithTag("StartPosition").transform;
        rb_ball.transform.position = startPosition.transform.position;
        rb_ball.transform.rotation = startPosition.transform.rotation;
    }
}
