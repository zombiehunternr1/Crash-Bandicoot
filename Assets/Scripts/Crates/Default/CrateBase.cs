using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateBase : MonoBehaviour
{
    [HideInInspector]
    public int CrateSide;

    private enum CrateDirection { None, Top, Bottom, Forward, Back, Left, Right, Spin }
    private Interactable Interact;
    private Breakable Break;

    void Awake()
    {
        Interact = GetComponent<Interactable>();
        Break = GetComponent<Breakable>();
    }

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
        if (Interact)
        {
            Interact.CrateAction(CrateSide);
        }
        if (Break)
        {
            Break.CrateAction(CrateSide);
        }
    }

    void Bottom()
    {
        CrateSide = Convert.ToInt32(CrateDirection.Bottom);
        if (Interact)
        {
            Interact.CrateAction(CrateSide);
        }
        if (Break)
        {
            Break.CrateAction(CrateSide);
        }
    }

    void Forward()
    {
        CrateSide = Convert.ToInt32(CrateDirection.Forward);
        if (Interact)
        {
            Interact.CrateAction(CrateSide);
        }
        if (Break)
        {
            Break.CrateAction(CrateSide);
        }
    }

    void Back()
    {
        CrateSide = Convert.ToInt32(CrateDirection.Back);
        if (Interact)
        {
            Interact.CrateAction(CrateSide);
        }
        if (Break)
        {
            Break.CrateAction(CrateSide);
        }
    }

    void Left()
    {
        CrateSide = Convert.ToInt32(CrateDirection.Left);
        if (Interact)
        {
            Interact.CrateAction(CrateSide);
        }
        if (Break)
        {
            Break.CrateAction(CrateSide);
        }
    }

    void Right()
    {
        CrateSide = Convert.ToInt32(CrateDirection.Right);
        if (Interact)
        {
            Interact.CrateAction(CrateSide);
        }
        if (Break)
        {
            Break.CrateAction(CrateSide);
        }
    }

    void Attack()
    {
        CrateSide = Convert.ToInt32(CrateDirection.Spin);
        if (Interact)
        {
            Interact.CrateAction(CrateSide);
        }
        if (Break)
        {
            Break.CrateAction(CrateSide);
        }
    }
}
