using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreBoard : MonoBehaviour {
    
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

	void Start () {
        Dictionary<string, int> scores = ScoreRW.ReadHighScore().highScore;

        var scoreList = scores.ToList();
        scoreList.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));

        int scoresToShow = (scoreList.Count >= 10) ? 10 : scores.Keys.Count;
        for(int i = 0; i < scoresToShow; i++)
        {
            Transform child = transform.GetChild(i);
            child.GetChild(0).GetComponent<TextMeshProUGUI>().text = scoreList[i].Key;
            child.GetChild(1).GetComponent<TextMeshProUGUI>().text = scoreList[i].Value.ToString();
        }
    }

}
