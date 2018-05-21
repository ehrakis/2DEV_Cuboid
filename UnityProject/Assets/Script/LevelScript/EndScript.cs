using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Assets.Script;
using UnityEngine.SceneManagement;
using TMPro;

public class EndScript : MonoBehaviour {

    public Text score;
    public TMP_InputField playerName;
    private int scoreValue;

    public void SaveScore()
    {
        if (!playerName.text.Equals(""))
        {
            ScoreRW.WriteHighScore(playerName.text, scoreValue);
            Destroy(GameObject.FindWithTag("GameManager"));
            Destroy(GameObject.FindWithTag("PlayerSettings"));
            SceneManager.LoadScene("ScoreMenu");
        }
    }

    void Start()
    {
        scoreValue = GameObject.FindWithTag("Counter").GetComponent<InGameUIManager>().GetScore();
        score.text = scoreValue.ToString();
    }
}
