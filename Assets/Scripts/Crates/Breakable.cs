using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    private Bounce Bouncing;
    private BreakAmount BreakOverTime;
    private int JumpAmount;
    private CrateBase Crate;

    void Awake()
    {
        Crate = GetComponent<CrateBase>();
        Bouncing = GetComponent<Bounce>();
        BreakOverTime = GetComponent<BreakAmount>();
    }

    public void CrateAction(int CrateSide)
    {
        switch (CrateSide)
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
            if (BreakOverTime)
            {
                JumpAmount++;
                Bouncing.Up();
                BreakOverTime.BreakOverTime(JumpAmount);
                return;
            }
            Bouncing.Up();
            return;
        }
        else
        {
            Crate.BounceUp();
        }
    }

    void Bottom()
    {
        if (Bouncing)
        {
            JumpAmount++;
            Bouncing.Down();
            BreakOverTime.BreakOverTime(JumpAmount);
        }
        else
        {
            Crate.BounceDown();
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
