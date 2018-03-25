using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script;

public class PaveRotationScript : AbstractCubeMouvement
{
    
    /*
     *The State property make it easy to track the cube position 
     *so more easy to know wich rotation to apply on it
     * 
     * State 1 correspond to the vertical state 
     * State 2 correspond to the horizontal state on the x axis
     * State 3 correspond to the horizontal state on the z axis
     */
    private int State = 1;
    private float xSize = 0;
    private float zSize = 0;

    public int GetState()
    {
        return State;
    }

    public void SetState(int value)
    {
        if(value == 1)
        {
            State = 1;
            xSize = 0;
            zSize = 0;
        }
        else if (value == 2)
        {
            State = 2;
            xSize = 0.5f;
            zSize = 0;
        }
        else if (value == 3)
        {
            State = 3;
            xSize = 0;
            zSize = 0.5f;
        }
    }

    private void changeState(Vector3 direction)
    {
        if(State == 1 && (direction == Vector3.right || direction == Vector3.left))
        {
            SetState(2);
        }
        else if(State == 1 && (direction == Vector3.forward || direction == Vector3.back))
        {
            SetState(3);
        }
        else if((State == 2 && (direction == Vector3.right || direction == Vector3.left))|| (State == 3 && (direction == Vector3.forward || direction == Vector3.back)))
        {
            SetState(1);
        }
    }

    /*
     * The addvalue is use to turn the cube whatever it's state is
     */ 
    private float ChooseAddValue(Vector3 direction)
    {
        if (State != 1)
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

    override
    public void TestStability()
    {
        if (GetState() != 1 && CollisionNumber < 2)
        {
            DenyMouvement();
            Expulse();
        }
        else if(GetState() != 1 && CollisionNumber == 2)
        {
            AllowMouvement();
            FrameWithoutContact = 0;
        }
        else if (GetState() == 1 && CollisionNumber < 1)
        {
            DenyMouvement();
            Expulse();
        }
        else if (GetState() == 1 && CollisionNumber == 1)
        {
            AllowMouvement();
            FrameWithoutContact = 0;
        }
    }
    
    private Vector3 SetRotationPoint(Vector3 direction)
    {
        return new Vector3(transform.position.x, 0, transform.position.z) + (direction * (ChooseAddValue(direction) + 0.5f));
    }

    override
    public void Rotate(Vector3 direction, Vector3 rotation)
    {
        LastMouvement = direction;
        CollisionNumber = 0;
        transform.RotateAround(SetRotationPoint(direction), rotation, 90);
        changeState(direction);
        IncreaseMouvementNumber();
    }

    void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.tag == "Tile")
        {
            CollisionNumber++;
        }
    }

    // Use this for initialization
    void Start () {
        transform.parent = null;
    }
	
	// Update is called once per frame
    void Update () {
        TestMouvement();
    }

    private void LateUpdate()
    {
        TestStability();
    }
}
