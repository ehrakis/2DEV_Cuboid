using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoftSwitch : AbstractCube
{
    void OnCollisionEnter(Collision other)
    {
        ToggleBridgeTile();
    }
}
