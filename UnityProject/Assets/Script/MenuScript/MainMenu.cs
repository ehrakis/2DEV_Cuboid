using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Script;

public class MainMenu : MonoBehaviour {

    public bool timerOn;
    public void Play()
    {
        timerOn = false;
        nextScene();
    }

    public void ScoreBoard()
    {
        SceneManager.LoadScene("ScoreMenu");
    }

    public  void PlayWithTimer()
    {
        timerOn = true;
        nextScene();
    }

    public void nextScene()
    {
        GameObject timer = GameObject.FindWithTag("PlayerSettings");
        (timer.GetComponent(typeof(PlayerSettings)) as PlayerSettings).timerOn = timerOn;
        DontDestroyOnLoad(timer);
        SceneManager.LoadScene("Level1");
    }

    public void Quit()
    {
        Application.Quit();
    }

}
