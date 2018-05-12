using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongSwitch : AbstractCube
{
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Equals("Cuboid"))
        {
            RectangularPrism player = other.gameObject.GetComponent<RectangularPrism>();
            if (player.GetState() == 1)
            {
                ToggleBridgeTile();
            }
        }
    }

}
