using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script;

public class LevelSettings : MonoBehaviour {

    public float CountdownTime;

    void Start()
    {
        (GameObject.FindWithTag("Counter").GetComponent(typeof(InGameUIManager)) as InGameUIManager).ChangeLevel();
    }
}
