using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreBoardController : MonoBehaviour
{
    public PinsController PinsController;

    // Score UI
    [SerializeField] TextMeshProUGUI Frame1A;
    [SerializeField] TextMeshProUGUI Frame1B;
    [SerializeField] TextMeshProUGUI Frame1T;

    // GameObjects & Text
    public TextMeshProUGUI ScoreSheet;
    public GameObject ScoreBoardCanvas;
    public GameObject accuracyText;

    // score vriables
    public int frameTotal;
    public int throws;
    public int frame;
    private float accuracy;

    // Update is called once per frame
    void Update()
    {
        ScoreUpdate();
    }
    public void ScoreUpdate()
    {
        // get char from current frame
        // replace char with current score
        //move to next char

        //parse string to convert it to int to be added later
        static int ParseString(string Frame1A)
        {
            int num = 0;
            num = int.Parse(Frame1A);
            return num;
        }

        Transform found = ScoreBoardCanvas.transform.Find("FrameScores/Frame 1/Frame1A");
        if (found && found.TryGetComponent<TextMeshProUGUI>(out var frametext))
        {
            frametext.text = $"{PinsController.score}";
        }
        //Frame1A.text = $"{score}";
        //Frame1B.text = $"{score}";
        //// int frametotal is frame A + B
        ////frameTotal = Frame1A + Frame1B;
        //// other frametotals are previous frametotal plus current frametotal
        //Frame1T.text = $"{score}";
    }
    public void AccuracyUpdate()
    {
        //accuracy = (score / 300) * 100;
        //accuracyText = accuracy; // need to convert the float to gameobject ??
        accuracyText.SetActive(true);
    }
}
