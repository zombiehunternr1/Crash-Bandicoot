using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateBase : CollisionSideDetection
{
    //[HideInInspector]
    public int CrateSide;

    private enum CrateDirection { None, Top, Bottom, Forward, Back, Left, Right, Spin }

    public void CrateDirectionHit(int PlayerSideHit)
    {
        switch (PlayerSideHit)
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
        Debug.Log(CrateSide);
    }

    void Bottom()
    {
        CrateSide = Convert.ToInt32(CrateDirection.Bottom);
        Debug.Log(CrateSide);
    }

    void Forward()
    {
        CrateSide = Convert.ToInt32(CrateDirection.Forward);
        Debug.Log(CrateSide);
    }

    void Back()
    {
        CrateSide = Convert.ToInt32(CrateDirection.Back);
        Debug.Log(CrateSide);
    }

    void Left()
    {
        CrateSide = Convert.ToInt32(CrateDirection.Left);
        Debug.Log(CrateSide);
    }

    void Right()
    {
        CrateSide = Convert.ToInt32(CrateDirection.Right);
        Debug.Log(CrateSide);
    }

    void Attack()
    {
        CrateSide = Convert.ToInt32(CrateDirection.Spin);
        Debug.Log(CrateSide);
    }
}
