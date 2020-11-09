using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    public CollisionSideDetection side;

    int value;

    public void CrateHit()
    {
        value = side.value;
        if (value == 1)
        {
            Debug.Log("Hit top");
        }
        if (value == 2)
        {
            Debug.Log("Hit bottom");
        }
        if (value == 3)
        {
            Debug.Log("Hit forward");
        }
        if (value == 4)
        {
            Debug.Log("Hit back");
        }
        if (value == 5)
        {
            Debug.Log("Hit left");
        }
        if (value == 6)
        {
            Debug.Log("Hit right");
        }
        if(value == 7)
        {
            Debug.Log("Spin attack");
        }
    }
}
