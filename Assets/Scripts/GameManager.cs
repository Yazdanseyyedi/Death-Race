using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUi;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        pauseMenuUi.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void PauseGame()
    {
        pauseMenuUi.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void GameEnd()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        Debug.Log("Loading menu");
        //SceneManager.LoadScene("Menu");
    }
    public void SettingMenu()
    {
        Debug.Log("Setting menu");
        //SceneManager.LoadScene("Setting");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
}

//if (Input.GetKey(KeyCode.Escape))
//{
//    FindObjectOfType<GameManager>().Restart();
//}
