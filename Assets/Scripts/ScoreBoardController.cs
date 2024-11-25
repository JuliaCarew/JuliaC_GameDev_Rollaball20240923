using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreBoardController : MonoBehaviour
{
    public PinsController pinsController;
    public PinState pinState;

    // Score UI
    [SerializeField] private TextMeshProUGUI[] frameA;
    [SerializeField] private TextMeshProUGUI[] frameB;
    [SerializeField] private TextMeshProUGUI[] frameT;

    // GameObjects & Text
    public TextMeshProUGUI ScoreSheet;
    public GameObject ScoreBoardCanvas;
    public GameObject accuracyText;

    // score vriables
    private int[,] frameScores = new int[10, 2]; // ints for each frame score structure
    private int[] frameTotals = new int[10]; // Total score per frame
    private int currentFrame = 0; 
    private int currentThrow = 0; // 0 for first throw, 1 for second throw
    //private bool waitingForThrow = false;
    private float accuracy;
    int score = PinState.score;

    void Update()
    {
        // Display the score for the current frame
        if (currentFrame < 10)
        {
            // Update score display for the current frame
            if (currentThrow == 0)
            {
                frameA[currentFrame].text = score.ToString(); // Show score for first throw
                //Debug.Log($"displaying score: {score} of FrameA - {currentFrame}");
            }
            else if (currentThrow == 1)
            {
                frameB[currentFrame].text = score.ToString(); // Show score for second throw
                //Debug.Log($"displaying score: {score} of FrameB - {currentFrame}");
            }

            // Update total score after both throws
            if (currentThrow == 1)
            {
                frameTotals[currentFrame] = frameScores[currentFrame, 0] + frameScores[currentFrame, 1];
                frameT[currentFrame].text = frameTotals[currentFrame].ToString(); // Display total for current frame
                //Debug.Log($"displaying Total score: {score} of FrameT - {currentFrame}");
            }
        }
    }

    public void UpdateScore(int knockedPins)
    {
        if (currentFrame >= 10)
        {
            Debug.LogWarning("All frames are completed. No more score updates allowed.");
            return;
        }

        if (currentThrow == 0) // First throw
        {
            frameScores[currentFrame, 0] = knockedPins;
            currentThrow = 1;

            // Handle strike (end frame immediately & reset pins)
            if (knockedPins == 10 && currentFrame < 9)
            {
                frameTotals[currentFrame] = frameScores[currentFrame, 0];
                frameT[currentFrame].text = frameTotals[currentFrame].ToString();
                currentFrame++;
                currentThrow = 0;
                pinsController.ResetPins(); 
            }
        }
        else if (currentThrow == 1) // Second throw
        {
            frameScores[currentFrame, 1] = knockedPins;
            frameTotals[currentFrame] = frameScores[currentFrame, 0] + frameScores[currentFrame, 1];
            frameT[currentFrame].text = frameTotals[currentFrame].ToString(); // Display total score
            
            // Move to the next frame after second throw
            currentFrame++;
            currentThrow = 0;
            pinsController.ResetPins(); // Reset pins after second throw
        }
    }
}
