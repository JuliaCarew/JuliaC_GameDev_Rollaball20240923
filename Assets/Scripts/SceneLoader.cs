using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameObject TitleScreen;
    public GameObject StartButton;
    public GameObject QuitButton;
    public GameObject GameOverScreen;
    public GameObject GameOverText;

    // Start is called before the first frame update
    void Start()
    {
        TitleScreen.SetActive(true);
        StartButton.SetActive(true);
        QuitButton.SetActive(true);

        GameOverScreen.SetActive(false);
        GameOverText.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
        Debug.Log("StartGame");
    }
    public void GameOver()
    {
        TitleScreen.SetActive(true);
        GameOverScreen.SetActive(true);
        GameOverText.SetActive(true);
    }
    // need the title screen to disappear when Start is pressed, and reappear when GameOver
}
