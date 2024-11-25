using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PinState : MonoBehaviour
{
    public static int score = 0;

    public bool isKnocked = false; // Tracks if the pin has been knocked down

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PinScoreCheck")
        {
            Debug.Log("pin collided with score check trigger");
            isKnocked = true;
            score++;
        }
    }
}
// want to get only score carrying over as a variable, for simplicity
// pins check if trigger, if yes score++
// score gets converted into scoreboard controller
