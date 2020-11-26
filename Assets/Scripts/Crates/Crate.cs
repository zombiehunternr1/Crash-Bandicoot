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
            //Top
            case 1:
                Top();
                break;
            //Botom
            case 2:
                Bottom();
                break;
            //Forward
            case 3:
                Forward();
                break;
            //Back
            case 4:
                Back();
                break;
            //Left
            case 5:
                Left();
                break;
            //Right
            case 6:
                Right();
                break;
            //Spin
            case 7:
                Attack();
                break;
        }
    }

    void Top()
    {
        Debug.Log("Top");
    }

    void Bottom()
    {
        Debug.Log("Bottom");
    }

    void Forward()
    {
        Debug.Log("Forward");
    }

    void Back()
    {
        Debug.Log("Back");
    }

    void Left()
    {
        Debug.Log("Left");
    }

    void Right()
    {
        Debug.Log("Right");
    }

    void Attack()
    {
        Debug.Log("Spin");
    }
}
