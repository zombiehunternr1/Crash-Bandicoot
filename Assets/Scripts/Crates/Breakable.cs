using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    [HideInInspector]
    public int CrateValue;

    private Bounce Bouncing;

    void Awake()
    {
        Bouncing = GetComponent<Bounce>();
    }

    public void CrateAction(int CrateSide)
    {
        CrateValue = CrateSide;

        switch (CrateValue)
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
        if (Bouncing)
        {
            Bouncing.Up();
        }
    }

    void Bottom()
    {
        if (Bouncing)
        {
            Bouncing.Down();
        }
    }

    void Forward()
    {
        Debug.Log("Hit breakable Forward");
    }

    void Back()
    {
        Debug.Log("Hit breakable Back");
    }

    void Left()
    {
        Debug.Log("Hit breakable Left");
    }

    void Right()
    {
        Debug.Log("Hit breakable Right");
    }

    void Attack()
    {
        Debug.Log("Spin attack breakable");
    }
}
