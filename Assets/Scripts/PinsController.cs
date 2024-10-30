using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinsController : MonoBehaviour
{
    public GameObject BowlingPin;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {   // reset ball position if out of bounds
        var Pins = GameObject.FindGameObjectsWithTag("Pin");

        //foreach (GameObject BowlingPin in Pins)
        //{
            if (other.gameObject.tag == "OutOfBounds")
            {
                Debug.Log("Out of Bounds");
                BowlingPin.SetActive(false);
            }
        //}      
    }
}
