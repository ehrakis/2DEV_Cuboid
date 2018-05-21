using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class HighScore {

    public Dictionary<string, int> highScore = new Dictionary<string, int>();


    public HighScore()
    {

    }

    public HighScore(String name, int score, HighScore oldScor)
    {
        highScore = oldScor.highScore;
        highScore[name] = score;
    }

}
