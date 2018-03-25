using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script;

public class CubeRotationScript : AbstractCubeMouvement
{

    override
    public void TestStability()
    {
        if (CollisionNumber < 1)
        {
            DenyMouvement();
            Expulse();
        }
        else
        {
            AllowMouvement();
            FrameWithoutContact = 0;
        }
    }

    private Vector3 SetRotationPoint(Vector3 direction)
    {
        return new Vector3(transform.position.x, 0, transform.position.z) + (direction *  0.5f);
    }

    override
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
        TestMouvement();
    }
    private void LateUpdate()
    {
        TestStability();
    }
}
