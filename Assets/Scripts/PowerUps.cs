using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PowerUps : MonoBehaviour
{
    // get the player gameobject
    // get the player gameobject's rigidbody component from the BallController script
    public GameObject powerUp;
    // power up 1 - BIG
    Vector3 scaleChange, positionChange;
    public float playerScale = 1f; // playerScale needs to equate to the player gameobject's size

    public void Awake()
    {
        int randomPowerUp = UnityEngine.Random.Range(0,4); // not working ??

    }
    void OnTriggerEnter(Collider other) // make a coroutine so that it runs indepentantly & on a set time ?
    {
        // list of power up objects (so that it's expandable)
        // spawn random power up gameobject at a random time (between 30 and 60 seconds)

        if (other.gameObject.tag == "Player")
        {
            int powerReturned = 0; // take this out later & reference int from PowerUp
            PowerUp(powerReturned);
            powerUp.SetActive(false);
            //Debug.Log("Player picked up a random power!");
        }
    }
    int PowerUp(int randomPowerUp)
    {
        int powerReturned = randomPowerUp;

        if (randomPowerUp == 0)
        {
            // make the playerRef 20% larger
            // scaleChange = new Vector3(.2f, .2f, .2f) playerRef * by 1.2
            // positionChange = new Vector3(only transform position on the y until aligned with the floor)

            // playerRef.transform.localScale += scaleChange;
            // playerRef.transform.position += positionChange;
            Debug.Log("Player picked up the power BIG!");

        }
        if (randomPowerUp == 1)
        {
            // triple the player
            // instantiate two player gameobjects at an offset
            Debug.Log("Player picked up the power TRIO!");
        }
        if (randomPowerUp == 2)
        {
            // make the player smaller
            //call player gameobject & * scale by 0.2
            Debug.Log("Player picked up the power SMALL!");
        }
        if (randomPowerUp == 3)
        {
            // teleport the player to a bonus level with insinite pins
            // load a new 'bonus level' scene (so there is no need to change position of a lot of game objects)
            // give infinite throws
            // take the player back to the scene after 10 seconds
            Debug.Log("Player picked up the power FRENZY!");
        }
        return powerReturned;
    }
}
