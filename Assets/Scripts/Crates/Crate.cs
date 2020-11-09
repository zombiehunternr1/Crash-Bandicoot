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

        switch (value)
        {
            case 1: print("I'm the top side");
                break;
            case 2: print("I'm the bottom side");
                break;
            case 3: print("I'm the forward side");
                break;
            case 4: print("I'm the back side");
                break;
            case 5: print("I'm the left side");
                break;
            case 6: print("I'm the right side");
                break;
            case 7: print("I'm the spin attack");
                break;

        }
    }
}
