using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaveRotationScript : MonoBehaviour {

    public Transform parent;

    private int state { get; set; }
    private float xSize = 0;
    private float zSize = 0;

    public void SetState(int value)
    {
        if(value == 1)
        {
            state = 1;
            xSize = 0;
            zSize = 0;
        }
        else if (value == 2)
        {
            state = 2;
            xSize = 0.5f;
            zSize = 0;
        }
        else if (value == 3)
        {
            state = 3;
            xSize = 0;
            zSize = 0.5f;
        }
    }

    private void changeState(Vector3 direction)
    {
        if(state == 1 && (direction == Vector3.right || direction == Vector3.left))
        {
            SetState(2);
        }
        else if(state == 1 && (direction == Vector3.forward || direction == Vector3.back))
        {
            SetState(3);
        }
        else if((state == 2 && (direction == Vector3.right || direction == Vector3.left))|| (state == 3 && (direction == Vector3.forward || direction == Vector3.back)))
        {
            SetState(1);
        }
    }

    private float ChooseAddValue(Vector3 direction)
    {
        if (state != 1)
        {
            if (direction == Vector3.right || direction == Vector3.left)
            {
                return xSize;
            }
            else
            {
                return zSize;
            }
        }
        else
        {
            return 0;
        }
    }

    public void Rotate(Vector3 direction, Vector3 rotation)
    {
        float addValue;
        addValue = ChooseAddValue(direction);
        parent.Translate(direction * (addValue +0.5f));
        transform.parent = parent;
        transform.RotateAround(parent.position, rotation, 90);
        transform.parent = null;
        changeState(direction);
        addValue = ChooseAddValue(direction);
        parent.Translate(direction * (addValue + 0.5f));
    }

    // Use this for initialization
    void Start () {
        SetState(1);
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
