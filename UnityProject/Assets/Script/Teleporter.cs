using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Equals("Cuboid"))
        {
            RectangularPrism player = other.gameObject.GetComponent<RectangularPrism>();
            if (player.GetState() == 1)
            {
                Vector3 firstposition = transform.GetChild(0).position;
                Vector3 secondpostion = transform.GetChild(1).position;
                player.Split(new Vector3(firstposition.x,0.5f,firstposition.z), new Vector3(secondpostion.x,0.5f, secondpostion.z));
            }
        }
    }

}
