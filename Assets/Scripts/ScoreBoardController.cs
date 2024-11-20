using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreBoardController : MonoBehaviour
{
    public PinsController PinsController;

    // Score UI
    [SerializeField] private TextMeshProUGUI[] frameA;
    [SerializeField] private TextMeshProUGUI[] frameB;
    [SerializeField] private TextMeshProUGUI[] frameT;

    // GameObjects & Text
    public TextMeshProUGUI ScoreSheet;
    public GameObject ScoreBoardCanvas;
    public GameObject accuracyText;

    // score vriables
     private int[,] frameScores = new int[10, 2];
    public int[] frameTotals = new int[10];
   private int currentFrame = 0;
    private int currentThrow = 0; // 0 for first throw, 1 for second throw
    private bool waitingForThrow = false;
    private float accuracy;

    void Update()
    {
         if (Input.GetMouseButtonUp(0) && !waitingForThrow) // Simulate a throw with the space key
        {
            StartCoroutine(HandleThrow());
            //int knockedPins = PinsController.score; // Use the score from PinsController
            //UpdateScore(knockedPins);
        }
    }
    private IEnumerator HandleThrow()
    {
        waitingForThrow = true;

        // Simulate the throw and wait for it to finish
        yield return new WaitForSeconds(2f); // Adjust based on actual gameplay mechanics
        int knockedPins = PinsController.score;

        // Update scores for the current frame and throw
        UpdateScore(knockedPins);

        // Reset the pins for the next throw
        PinsController.ResetPins();
        waitingForThrow = false;
    }

    public void UpdateScore(int knockedPins)
    {
        if (currentFrame >= 10)
        {
            Debug.Log("Game over! All frames completed.");
            return;
        }

        // Assign the score to the current throw
        if (currentThrow == 0)
        {
            frameScores[currentFrame, 0] = knockedPins;
            frameA[currentFrame].text = knockedPins.ToString();
            currentThrow = 1;

             // If it's a strike, skip the second throw
            if (knockedPins == 10 && currentFrame < 9)
            {
                UpdateFrameTotal();
                currentFrame++;
                currentThrow = 0;
                PinsController.ResetPins(); // Reset pins for the next frame
             }
        }
        else if (currentThrow == 1)
        {
            frameScores[currentFrame, 1] = knockedPins;
            frameB[currentFrame].text = knockedPins.ToString();

            // Update the frame total
            UpdateFrameTotal();

            // Move to the next frame
            currentFrame++;
            currentThrow = 0;
            PinsController.ResetPins(); // Reset pins for the next frame
        }
    }
 
    private void UpdateFrameTotal()
    {
         int frameTotal = frameScores[currentFrame, 0] + frameScores[currentFrame, 1];
        frameTotals[currentFrame] = frameTotal;

        // Display the total for the current frame
        frameT[currentFrame].text = frameTotal.ToString();
    }
}
