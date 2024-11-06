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
        GameOverScreen.SetActive(false);
        GameOverText.SetActive(false);
        StartButton.SetActive(false);
        QuitButton.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
    public void StartGame()
    {
        TitleScreen.SetActive(false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void GameOver()
    {
        TitleScreen.SetActive(true);
        GameOverScreen.SetActive(true);
        GameOverText.SetActive(true);
    }
    // need the title screen to disappear when Start is pressed, and reappear when GameOver
}
