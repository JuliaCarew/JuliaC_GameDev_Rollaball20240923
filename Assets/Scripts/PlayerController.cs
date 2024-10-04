using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public bool cubeIsOnTheGround = true;
    private Vector3 scaleChange, positionChange;

    private int count;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
    }

    void OnMove (InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 8)
        {
            winTextObject.SetActive(true);
        }
    }   
    
    private void OnCollisionEnter(Collision collision)
    {
        //makes sure cube is on the ground before bounce is triggered
        if (collision.gameObject.tag == "Ground")
        {
            cubeIsOnTheGround = true;
        }
        //bounces cube on Y axis
        if (collision.gameObject.CompareTag("BounceButton"))
        {
            rb.AddForce(new Vector3(0, 8, 0), ForceMode.Impulse);
            cubeIsOnTheGround = false;
        }
        if (collision.gameObject.CompareTag("BoostButton"))
        {
            rb.AddForce(new Vector3(0, 0, 8), ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }      
    }
}
