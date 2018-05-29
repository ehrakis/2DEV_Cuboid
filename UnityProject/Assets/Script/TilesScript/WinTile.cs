using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class WinTile : MonoBehaviour {

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Equals("Cuboid"))
        {
            RectangularPrism player = other.gameObject.GetComponent<RectangularPrism>();
            if (player.GetState() == 1)
	    {
		    Scene scene = SceneManager.GetActiveScene();
                if(scene.name == "Level10")
                {
                    SceneManager.LoadScene("End");
                }
                else
                {

                    char levelnumber = scene.name[scene.name.Length - 1];
                    int levelNumberInt = (int)char.GetNumericValue(levelnumber) + 1;
                    string nextScene = "Level" + levelNumberInt;

                    DontDestroyOnLoad(GameObject.Find("InGameDisplay"));

                    SceneManager.LoadScene(nextScene);
                }
            }
        }
    }
}
