using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : CollisionSideDetection
{
    //[HideInInspector]
    public int EnemySide;

    private enum EnemyDirection { None, Top, Bottom, Forward, Back, Left, Right, Spin }

    public void EnemyDirectionHit(int PlayerSideHit)
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
        Debug.Log("Enemy");
        EnemySide = Convert.ToInt32(EnemyDirection.Top);
        Debug.Log(EnemySide);
    }

    void Bottom()
    {
        EnemySide = Convert.ToInt32(EnemyDirection.Bottom);
        Debug.Log(EnemySide);
    }

    void Forward()
    {
        EnemySide = Convert.ToInt32(EnemyDirection.Forward);
        Debug.Log(EnemySide);
    }

    void Back()
    {
        EnemySide = Convert.ToInt32(EnemyDirection.Back);
        Debug.Log(EnemySide);
    }

    void Left()
    {
        EnemySide = Convert.ToInt32(EnemyDirection.Left);
        Debug.Log(EnemySide);
    }

    void Right()
    {
        EnemySide = Convert.ToInt32(EnemyDirection.Right);
        Debug.Log(EnemySide);
    }

    void Attack()
    {
        EnemySide = Convert.ToInt32(EnemyDirection.Spin);
        Debug.Log(EnemySide);
    }
}
