using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PinsController : MonoBehaviour
{
    public GameObject BowlingPin;
    public GameObject Player;

    public static int score = 0;

    private void OnTriggerEnter(Collider other)
    {   
        var Pins = GameObject.FindGameObjectsWithTag("Pin");
        var Player = GameObject.FindGameObjectWithTag("Player");


        if (other.gameObject.tag == "OutOfBounds")
        {
            //Debug.Log("Out of Bounds");
            BowlingPin.SetActive(false);
        }
        foreach (GameObject BowlingPin in Pins)
        {
            // if collide w pin, check if pin is down (++score)/ score is not counting properly
            if (other.gameObject.tag == "Player")
            {
                Debug.Log("Hit pin");
                score++;
            }
        }
       
    }
}
