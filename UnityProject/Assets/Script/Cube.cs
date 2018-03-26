using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script;

public class Cube : AbstractCubeMouvement
{
    private bool isSelected = false;

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

    void ShowParticle()
    {
        GameObject CubeParticlePrefab = Resources.Load("Animation/CubeChangeParticle") as GameObject;
        print(CubeParticlePrefab);
        GameObject CubeParticleGO = Instantiate(CubeParticlePrefab);
        ParticleSystem CubeParticle = CubeParticleGO.GetComponent<ParticleSystem>();
        CubeParticle.Play();
        CubeParticle.transform.position = transform.position + new Vector3(0, 0.5f, 0);
        Destroy(CubeParticleGO, 1f);
    }

    public void Select()
    {
        isSelected = true;
        ShowParticle();
    }

    public void DeSelect()
    {
        isSelected = false;
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

    public void ChangeCube()
    {
        Cube[] allCube = FindObjectsOfType<Cube>();
        foreach(Cube part in allCube)
        {
            if (!part.Equals(this)){
                Input.ResetInputAxes();
                part.Select();
                DeSelect();                
            }
        }
    }

    override
    public void TestMouvement()
    {
        if (isSelected)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) && GetAllowInput())
            {
                Rotate(Vector3.left, new Vector3(0, 0, 1));
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && GetAllowInput())
            {
                Rotate(Vector3.right, new Vector3(0, 0, -1));
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) && GetAllowInput())
            {
                Rotate(Vector3.forward, new Vector3(1, 0, 0));
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && GetAllowInput())
            {
                Rotate(Vector3.back, new Vector3(-1, 0, 0));
            }
            else if(Input.GetKeyDown(KeyCode.Space) && GetAllowInput())
            {
                ChangeCube();
            }
        }
        
    }

    // Use this for initialization
    void Start() {
        //transform.parent = parent;
        transform.parent = null;

        /*
         * The next part is use for testing 
         * in the game cube 2 will be deselect
         * when cube are make
         */ 
        if (transform.name == "Cube1")
        {
            Select();
        }
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
