using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PinsController : MonoBehaviour
{
    public static int score = 0;
    public Vector3[] initialPinPositions; // Array to store initial pin positions
    public Quaternion[] initialPinRotations; // Array to store initial pin rotations
    public Rigidbody[] pinRigidbodies;
    public GameObject[] pins;
    public bool isKnocked;

    void Awake()
    {
        isKnocked = false; // Tracks if the pin has been knocked down
    }
    void Start()
    {
        // Get all pins at the start of the game and store their initial positions/rotations
        pins = GameObject.FindGameObjectsWithTag("Pin");
        initialPinPositions = new Vector3[pins.Length];
        initialPinRotations = new Quaternion[pins.Length];
        pinRigidbodies = new Rigidbody[pins.Length];

        for (int i = 0; i < pins.Length; i++)
        {
            initialPinPositions[i] = pins[i].transform.position;
            initialPinRotations[i] = pins[i].transform.rotation;
            pinRigidbodies[i] = pins[i].GetComponent<Rigidbody>();
        }
    }

    public void OnCollisionEnter(Collision collision)
    {   
        if (collision.gameObject.CompareTag("Player")) // move this check to PinState?
        {
            // Check if the pin is knocked over
            foreach (GameObject pin in pins)
            {               
                // Ensure we count the pin only once
                if (!pin.GetComponent<PinState>().isKnocked)
                {
                    Debug.Log("pin isKnocked"); // not appearing 
                    pin.GetComponent<PinState>().isKnocked = true;
                    score++;
                }
            }
        }
    }
    public void ResetPins()
    {
        Debug.Log("resetting pins");

        for (int i = 0; i < pins.Length; i++)
        {
            pins[i].transform.position = initialPinPositions[i];
            pins[i].transform.rotation = initialPinRotations[i];

            // Reset the rigidbody physics to stop all movement
            Rigidbody rb = pinRigidbodies[i];
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.Sleep(); // Puts the rigidbody to sleep
            }

            // Reset the "knocked" state for all pins
            pins[i].GetComponent<PinState>().isKnocked = false;
        }

        // Reset the score for the new throw
        score = 0;    
    }
}
// delay a few seconds before resetting the pins at each frame,
// maybe on-screen UI showing frame complete, moving on to frame _
// reverse the isKnocked check, if isKnocked = true, increment score
// & in PinsState, check collision with the player