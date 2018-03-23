using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRotationScript : MonoBehaviour {

    public Transform parent;

    public void Rotate(Vector3 direction, Vector3 rotation)
    {
        parent.Translate(direction * 0.5f);
        transform.parent = parent;
        transform.RotateAround(parent.position, rotation, 90);
        transform.parent = null;
        parent.Translate(direction * 0.5f);
    }

	// Use this for initialization
	void Start () {
        //transform.parent = parent;
        transform.parent = null;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Rotate(Vector3.left, new Vector3(0, 0, 1));
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Rotate(Vector3.right, new Vector3(0, 0, -1));
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Rotate(Vector3.forward, new Vector3(1, 0, 0));
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Rotate(Vector3.back, new Vector3(-1, 0, 0));
        }
    }
}
