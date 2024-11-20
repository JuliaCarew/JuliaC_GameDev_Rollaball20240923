using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PinsController : MonoBehaviour
{
    public static int score = 0;
    private Vector3[] initialPinPositions; // Array to store initial pin positions
    private Quaternion[] initialPinRotations; // Array to store initial pin rotations
    private Rigidbody[] pinRigidbodies;
    private GameObject[] pins; 

    private void Start()
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

     private void OnCollisionEnter(Collision collision)
    {   
         if (collision.gameObject.CompareTag("Player"))
        {
            // Check if the pin is knocked over
            foreach (GameObject pin in pins)
            {
                Rigidbody pinRb = pin.GetComponent<Rigidbody>();

                if (pinRb != null)
                {
                    // Check if the pin is tilted beyond a threshold
                    if (Mathf.Abs(pin.transform.eulerAngles.x) > 5f || Mathf.Abs(pin.transform.eulerAngles.z) > 5f)
                    {
                        // Ensure we count the pin only once
                        if (!pin.GetComponent<PinState>().isKnocked)
                        {
                            pin.GetComponent<PinState>().isKnocked = true;
                            score++;
                        }
                    }
                }
            }
        }
    }
   /// <summary>
    /// Resets all pins to their initial positions and rotations.
    /// </summary>
    public void ResetPins()
    {
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
    // private Vector3 GetInitialPinPosition(GameObject pin)
    // {
    //     // Store or retrieve initial pin positions
    //     return pin.transform.position; // Placeholder, modify as needed to get initial positions
    // }
}
