using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Assets.Script;
using UnityEngine.SceneManagement;

public class InGameUIManager : MonoBehaviour {

    private int mouvements = 0;
    public Text MouvementsDisplay;
    public Text timeDisplay;
    private float timeLeft;
    private int totalTimeLeft = 0;
    private string currentLevel;
    private bool isTimerActive;
    public bool gameEnd = false;
    
    public void SetTimerActive()
    {
        isTimerActive = true;
    }

    void DisplaySocre()
    {

        MouvementsDisplay.text = "Moves : " + mouvements.ToString();
    }

    public void IncreaseMouvements()
    {
        mouvements++;
        DisplaySocre();
    }

    public int GetScore()
    {
        return mouvements - (totalTimeLeft / 2);
    }

    public void ChangeLevel()
    {
        if (isTimerActive)
        {
            if (currentLevel == null || currentLevel != SceneManager.GetActiveScene().name)
            {
                currentLevel = SceneManager.GetActiveScene().name;
                totalTimeLeft += (int)Mathf.Round(timeLeft);
                timeLeft = (GameObject.FindWithTag("LevelSettings").GetComponent(typeof(LevelSettings)) as LevelSettings).CountdownTime;
            }
        }
    }

    public void EndGame()
    {
        gameEnd = true;
        totalTimeLeft += (int)Mathf.Round(timeLeft);
        gameObject.GetComponent<PauseMenu>().DisablePause();
    }

    void Start()
    {
        GameObject[] inGameDisplays = GameObject.FindGameObjectsWithTag("GameManager");
        if(inGameDisplays.Length > 1 && mouvements == 0)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
        if(SceneManager.GetActiveScene().name != "End")
        {
            isTimerActive = (GameObject.FindWithTag("PlayerSettings").GetComponent(typeof(PlayerSettings)) as PlayerSettings).timerOn;
            if (!isTimerActive)
            {
                GameObject.FindWithTag("Countdown").SetActive(false);
            }
            else
            {
                ChangeLevel();
            }
        }
        else
        {
            EndGame();
        }
        
    }

    void Update()
    {
        if (isTimerActive && !gameEnd)
        {
            timeLeft -= Time.deltaTime;
            timeDisplay.text = "Time Left : " + Mathf.Round(timeLeft);
            if (timeLeft <= 0)
            {
                SceneManager.LoadScene("GameOver");
                Destroy(gameObject.transform.parent.gameObject);
            }
        }
    }
}
