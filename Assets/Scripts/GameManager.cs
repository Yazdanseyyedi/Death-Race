using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool GameIsPaused = false;
    public bool GameHasEnd = false;

    public GameObject pauseMenuUi;
    public GameObject endMenuUi;

    public Text score1;
    public Text score2;
    public Text cycle1;
    public Text cycle2;
    public Text winner;
    public Text looser;
    public Text damage1;
    public Text damage2;

    public PlayerController pc;
    public SecondPlayerController spc;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                ResumeGame();
                //GameIsPaused = false;
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
        if (!GameHasEnd)
        {
            GameHasEnd = true;
            Time.timeScale = 0f;

            if (pc.isWinner)
            {
                winner.text = "car1";
                looser.text = "car2";
                pc.score += 100;
                score1.text = pc.score.ToString();
                score2.text = spc.score.ToString();
                cycle1.text = pc.cycleCounter.ToString();
                cycle2.text = spc.cycleCounter.ToString();
                damage1.text = pc.damage.ToString();
                damage2.text = spc.damage.ToString();
            }
            else
            {
                winner.text = "car2";
                looser.text = "car1";
                spc.score += 100;
                score1.text = spc.score.ToString();
                score2.text = pc.score.ToString();
                cycle1.text = spc.cycleCounter.ToString();
                cycle2.text = pc.cycleCounter.ToString();
                damage1.text = spc.damage.ToString();
                damage2.text = pc.damage.ToString();
            }
            endMenuUi.SetActive(true);
            //Application.Quit();
        }
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        GameHasEnd = false;
        SceneManager.LoadScene("StartMenuScene");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
        GameIsPaused = false;
        GameHasEnd = false;
        spc.damage = 0;
        pc.damage = 0;
    }
}

//if (Input.GetKey(KeyCode.Escape))
//{
//    FindObjectOfType<GameManager>().Restart();
//}
