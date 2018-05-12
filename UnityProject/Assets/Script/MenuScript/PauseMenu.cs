using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    private static bool gamePaused = false;
    public GameObject pauseMenuUI;
    private GameObject player;

    public static bool getGamePaused()
    {
        return gamePaused;
    }

    private static void setGamePaused(bool value)
    {
        gamePaused = value;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        setGamePaused(false);
        player.SetActive(true);
        player = null;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        setGamePaused(true);
        GameObject player = GameObject.FindWithTag("Cuboid");
        if (player == null)
        {
            player = GameObject.FindWithTag("Cube");
        }
        this.player = player;
        player.SetActive(false);
        Time.timeScale = 0f;
    }

    public void mainMenu()
    {
        Resume();
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }

	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (getGamePaused())
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
	}
}
