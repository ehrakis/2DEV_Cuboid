using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaveRotationScript : MonoBehaviour {

    public Transform parent;

    public int state = 1;
    private int xSize = 0;
    private int zSize = 0;

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
            xSize = 1;
            zSize = 0;
        }
        else if (value == 3)
        {
            state = 3;
            xSize = 0;
            zSize = 1;
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
        else if(state == 2 && (direction == Vector3.right || direction == Vector3.left))
        {
            SetState(1);
        }
        else if (state == 3 && (direction == Vector3.forward || direction == Vector3.back))
        {
            SetState(1);
        }
    }

    public void Rotate(Vector3 direction, Vector3 rotation)
    {
        float addValue;
        if (state != 1)
        {
            if((state == 2 && (direction == Vector3.right || direction == Vector3.left)) || (state == 3 && (direction == Vector3.forward || direction == Vector3.back)))
            {
                addValue = 0.5f;
            }
            else
            {
                addValue = 0f;
            }
        }
        else
        {
            addValue = 0f;
        }
        parent.Translate(direction * (addValue +0.5f));
        transform.parent = parent;
        transform.RotateAround(parent.position, rotation, 90);
        transform.parent = null;
        changeState(direction);
        if (state != 1)
        {
            if ((state == 2 && (direction == Vector3.right || direction == Vector3.left)) || (state == 3 && (direction == Vector3.forward || direction == Vector3.back)))
            {
                addValue = 0.5f;
            }
            else
            {
                addValue = 0f;
            }
        }
        else
        {
            addValue = 0f;
        }
        parent.Translate(direction * (addValue + 0.5f));
    }

    // Use this for initialization
    void Start () {
		
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
