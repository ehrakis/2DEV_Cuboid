using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MouvementsCounter : MonoBehaviour {

    private int mouvements = 0;
    public Text MouvementsDisplay;
    
    void DisplaySocre()
    {
        MouvementsDisplay.text = "Mouvements : " + mouvements.ToString();
    }

    public void IncreaseMouvements()
    {
        mouvements++;
        DisplaySocre();
    }

    private void Start()
    {
        GameObject[] inGameDisplays = GameObject.FindGameObjectsWithTag("GameManager");
        if(inGameDisplays.Length > 1 && mouvements == 0)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
        MouvementsDisplay = GameObject.Find("MouvementsNumber").GetComponent<Text>();

    }
}
