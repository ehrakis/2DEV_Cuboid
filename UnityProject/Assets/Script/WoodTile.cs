using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script;

public class WoodTile : MonoBehaviour {

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Equals("Cuboid"))
        {
            RectangularPrism player = other.gameObject.GetComponent<RectangularPrism>();
            if(player.GetState() == 1)
            {
                player.DenyMouvement();
                GetComponent<Rigidbody>().useGravity = true;
                GetComponent<Rigidbody>().isKinematic = false;
                other.gameObject.GetComponent<Rigidbody>().useGravity = true;
                player.DenyMouvement();
                player.Fall();
            }
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
