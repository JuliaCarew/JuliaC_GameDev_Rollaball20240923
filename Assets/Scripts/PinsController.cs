using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PinsController : MonoBehaviour
{
    public Vector3[] initialPinPositions;
    public Quaternion[] initialPinRotations;
    public Rigidbody[] pinRigidbodies;
    public GameObject[] pins;
    public bool isKnocked;

    void Awake()
    {
        isKnocked = false; 
        //Debug.Log("Awake: pin state reset");
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
    //public void OnCollisionEnter(Collision collision) // FIXME: no debug logs being called
    //{
    //    if (collision.gameObject.CompareTag("Ball"))
    //    {
    //        Debug.Log("Ball collided with pin");
    //        foreach (GameObject pin in pins)
    //        {
    //            if (!pin.GetComponent<PinState>().isKnocked) // Only count knocked pins once
    //            {
    //                Debug.Log($"Pin {pin} is knocked");
    //                pin.GetComponent<PinState>().isKnocked = true;
    //                score++; // Increment score for each knocked pin
    //                Debug.Log($"Pin knocked! Current score: {score}"); 
    //            }
    //        }
    //    }
    //}
    public void ResetPins() 
    {
        // Reset pins to initial positions and set their state back to unknocked
        for (int i = 0; i < pins.Length; i++)
        {
            pins[i].transform.position = initialPinPositions[i];
            pins[i].transform.rotation = initialPinRotations[i];

            Rigidbody rb = pinRigidbodies[i];
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.Sleep();
            }

            pins[i].GetComponent<PinState>().isKnocked = false; // Reset knocked state
        }
    }
}
// pins need to be counted after more of a delay, so wait about 2 seconds for everything to fall over
// pins still not being added & displayed as score