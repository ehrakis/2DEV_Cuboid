using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractCube : MonoBehaviour {

    public void ToggleBridgeTile()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(!child.gameObject.activeSelf);
        }
    }
}
