using System.Collections;
using Assets.Script;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class ScoreRW{

	public static HighScore ReadHighScore()
    {
        
        if (File.Exists(Application.persistentDataPath + "/highscore.cub"))
        {
            BinaryFormatter binaryFormater = new BinaryFormatter();
            FileStream scoreFile = new FileStream(Application.persistentDataPath + "/highscore.cub", FileMode.Open);
            HighScore highScoreTable = (HighScore)binaryFormater.Deserialize(scoreFile);
            scoreFile.Close();
            return highScoreTable;
        }
        else {
            return new HighScore();
        }
    }

    public static void WriteHighScore(string playerName, int score)
    {
        BinaryFormatter binaryFormater = new BinaryFormatter();
        HighScore currentScoreTable = ReadHighScore();
        if (!currentScoreTable.highScore.ContainsKey(playerName) || (currentScoreTable.highScore.ContainsKey(playerName) && currentScoreTable.highScore[playerName] > score))
        {
            FileStream scoreFile = new FileStream(Application.persistentDataPath + "/highscore.cub", FileMode.Create);
            HighScore updatedScore = new HighScore(playerName, score, currentScoreTable);
            binaryFormater.Serialize(scoreFile, updatedScore);
            scoreFile.Close();
        }
    }

}
