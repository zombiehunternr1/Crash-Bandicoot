using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateBase : MonoBehaviour
{
    [HideInInspector]
    public int CrateSide;
    [HideInInspector]
    public bool GravityEnabled = false;

    private enum CrateDirection { None, Top, Bottom, Forward, Back, Left, Right, Spin, Invincibility }
    private Interactable Interact;
    private Breakable Break;
    private Rigidbody Rb;

    void Awake()
    {
        Rb = GetComponent<Rigidbody>();
        Interact = GetComponent<Interactable>();
        Break = GetComponent<Breakable>();
        if (Rb.useGravity)
        {
            GravityEnabled = true;
        }
    }

    public void CrateDirectionHit(int PlayerSideHit, Vector3 PlayerVelocity)
    {
        switch (PlayerSideHit)
        {
            //Top
            case 1:
                Top(PlayerVelocity);
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
            //Invincibility
            case 8:
                Invincibility();
                break;
        }
    }

    void Top(Vector3 PlayerVelicoty)
    {
        gameObject.GetComponent<BoxCollider>().material.staticFriction = 1;      
        CrateSide = Convert.ToInt32(CrateDirection.Top);

        if (Interact)
        {
            Interact.CrateAction(CrateSide);
            return;
        }
        if (Break && (PlayerVelicoty.y < 0.01f) || (PlayerVelicoty.y > 0.01f))
        {
            Break.CrateAction(CrateSide);
            return;
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

    void Invincibility()
    {
        CrateSide = Convert.ToInt32(CrateDirection.Invincibility);
        if (Interact)
        {
            Interact.CrateAction(CrateSide);
        }
        if (Break)
        {
            Break.CrateAction(CrateSide);
        }
    }

    public void BounceUpPlayer()
    {
        Rb.velocity = new Vector3(Rb.velocity.x, 0);
        Rb.AddForce(new Vector3(0, 400));
    }

    public void BounceDownPlayer()
    {
        Rb.velocity = new Vector3(Rb.velocity.x, 0);
        Rb.AddForce(new Vector3(0, -400));
    }

    public void BounceUpCrate()
    {
        Rb.velocity = new Vector3(Rb.velocity.x, 0);
        Rb.AddForce(new Vector3(0, 600));
    }
}
