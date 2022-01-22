using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void PlayGame()
    {
        PlayerPrefs.SetInt("score_one", 0);
        PlayerPrefs.SetInt("score_two", 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadStore()
    {
        SceneManager.LoadScene(2);
    }
}
