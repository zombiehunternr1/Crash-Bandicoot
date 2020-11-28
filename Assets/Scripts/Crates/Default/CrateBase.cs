using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateBase : MonoBehaviour
{
    [HideInInspector]
    public int CrateSide;

    private enum CrateDirection { None, Top, Bottom, Forward, Back, Left, Right, Spin }
    int SideValue;

    public void CrateDirectionHit(CollisionSideDetection PlayerHit)
    {
        SideValue = PlayerHit.SideHitValue;

        switch (SideValue)
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
        CrateSide = Convert.ToInt32(CrateDirection.Top);
    }

    void Bottom()
    {
        CrateSide = Convert.ToInt32(CrateDirection.Bottom);
    }

    void Forward()
    {
        CrateSide = Convert.ToInt32(CrateDirection.Forward);
    }

    void Back()
    {
        CrateSide = Convert.ToInt32(CrateDirection.Back);
    }

    void Left()
    {
        CrateSide = Convert.ToInt32(CrateDirection.Left);
    }

    void Right()
    {
        CrateSide = Convert.ToInt32(CrateDirection.Right);
    }

    void Attack()
    {
        CrateSide = Convert.ToInt32(CrateDirection.Spin);
    }
}
