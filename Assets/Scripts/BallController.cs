using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class BallController : MonoBehaviour
{
    public GameObject ball;
    public Rigidbody rb_ball;
    public GameObject aimGuide;
    public TextMeshProUGUI ScoreSheet;
    public GameObject winTextObject;
    public GameObject holdKeyObject;
    public Text accuracyText;
    public bool ballStopped;
    public float speed = 0; 
    private int score;
    private int throws;
    private float ballVeloityMagnitude;
    public bool heldKey;
    public float holdDuration = 0;
    private float accuracy = 55;

    private Vector3 scaleChange, positionChange;

    // Start is called before the first frame update
    private void Start()
    {
        ballStopped = true;
        score = 0;
        throws = 0;
        SetScoreText();
        winTextObject.SetActive(false);
        holdKeyObject.SetActive(false); //somehow always displaying??
    }
    // Update is called once per frame
    void Update()
    {
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
            rb_ball.AddForce(new Vector3(0, 0, 30), ForceMode.Impulse);
            heldKey = false;
            throws++;
        }
        //set up hold shoot - display ui w it
        else 
        {
            ballStopped = false;
            heldKey = true;
            //holdKeyObject.SetActive(true);
            //Add multiplicative force to rb_ball * holdDuration ? 
        }
    }
    void SetScoreText()
    {
        //ScoreSheet.text = "TEST" + score.ToString();
        if (score > 10)
        {
            winTextObject.SetActive(true);
            accuracy = (score / 300) * 100;
            accuracyText.text = accuracy.ToString();
        }
    }
    public void SetBallToStart() //not triggering
    {
        Transform startPosition = GameObject.FindWithTag("StartPosition").transform;
        rb_ball.transform.position = startPosition.transform.position;
        rb_ball.transform.rotation = startPosition.transform.rotation;
    }
}
