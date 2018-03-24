using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRotationScript : MonoBehaviour {

    private int CollisionNumber = 0;
    private int DeplacementNumber = 0;
    private bool AllowInput = true;
    private int FrameWithoutContact = 0;
    private Vector3 LastMouvement;

    public int GetDeplcementNumber()
    {
        return DeplacementNumber;
    }

    public void Expulse()
    {
        if (++FrameWithoutContact >= 3)
        {
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().isKinematic = false;
            if (LastMouvement == Vector3.back)
                GetComponent<Rigidbody>().AddTorque(Vector3.left * 10);
            else if (LastMouvement == Vector3.forward)
                GetComponent<Rigidbody>().AddTorque(Vector3.right * 10);
            else if (LastMouvement == Vector3.left)
                GetComponent<Rigidbody>().AddTorque(Vector3.forward * 10);
            else if (LastMouvement == Vector3.right)
                GetComponent<Rigidbody>().AddTorque(Vector3.back * 10);
        }
    }

    public void TestStability()
    {
        if (CollisionNumber < 1)
        {
            AllowInput = false;
            Expulse();
        }
        else
        {
            AllowInput = true;
            FrameWithoutContact = 0;
        }
    }

    private void IncreaseMouvementNumber()
    {
        DeplacementNumber++;
    }

    private Vector3 SetRotationPoint(Vector3 direction)
    {
        return new Vector3(transform.position.x, 0, transform.position.z) + (direction *  0.5f);
    }

    public void Rotate(Vector3 direction, Vector3 rotation)
    {
        LastMouvement = direction;
        CollisionNumber = 0;
        transform.RotateAround(SetRotationPoint(direction), rotation, 90);
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
    private void LateUpdate()
    {
        TestStability();
    }
}
