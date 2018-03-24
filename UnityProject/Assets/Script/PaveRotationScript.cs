using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaveRotationScript : MonoBehaviour {

    private Vector3 RotationPoint;
    private int State = 1;
    private int DeplacementNumber = 0;
    private float xSize = 0;
    private float zSize = 0;
    private int CollisionNumber = 0;
    private bool AllowInput = true;
    private int FrameWithoutContact = 0;
    private Vector3 LastMouvement;

    public int GetState()
    {
        return State;
    }

    public int GetDeplcementNumber()
    {
        return DeplacementNumber;
    }

    private void IncreaseMouvementNumber()
    {
        DeplacementNumber++;
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

    private void SetRotationPoint(Vector3 direction)
    {
        RotationPoint = new Vector3(transform.position.x, 0, transform.position.z) + (direction * (ChooseAddValue(direction) + 0.5f));
    }

    public void Expulse()
    {
        if(++FrameWithoutContact >= 3)
        {
            //Rotate(LastMouvement, LastRotation);
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().isKinematic = false;
            if (LastMouvement==Vector3.down)
                GetComponent<Rigidbody>().AddTorque(Vector3.left*10);
            else if (LastMouvement == Vector3.up)
                GetComponent<Rigidbody>().AddTorque(Vector3.right * 10);
            else if(LastMouvement==Vector3.left)
                GetComponent<Rigidbody>().AddTorque(Vector3.forward * 10);
            else if (LastMouvement == Vector3.right)
                GetComponent<Rigidbody>().AddTorque(Vector3.back * 10);
        }
    }

    public void TestStability()
    {
        if (GetState() != 1 && CollisionNumber < 2)
        {
            AllowInput = false;
            Expulse();
        }
        else if(GetState() != 1 && CollisionNumber == 2)
        {
            AllowInput = true;
            FrameWithoutContact = 0;
        }
        else if (GetState() == 1 && CollisionNumber < 1)
        {
            AllowInput = false;
            Expulse();
        }
        else if (GetState() == 1 && CollisionNumber == 1)
        {
            AllowInput = true;
            FrameWithoutContact = 0;
        }
    }

    public void Rotate(Vector3 direction, Vector3 rotation)
    {
        LastMouvement = direction;
        CollisionNumber = 0;
        SetRotationPoint(direction);
        transform.RotateAround(RotationPoint, rotation, 90);
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
        if (Input.GetKeyDown(KeyCode.LeftArrow) && AllowInput)
        {
            Rotate(Vector3.left, new Vector3(0, 0, 1));
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && AllowInput)
        {
            Rotate(Vector3.right, new Vector3(0, 0, -1));
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && AllowInput)
        {
            Rotate(Vector3.forward, new Vector3(1, 0, 0));
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && AllowInput)
        {
            Rotate(Vector3.back, new Vector3(-1, 0, 0));
        }
    }

    private void LateUpdate()
    {
        TestStability();
    }
}
