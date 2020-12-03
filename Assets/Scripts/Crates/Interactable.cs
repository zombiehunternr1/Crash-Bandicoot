using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private Bounce Bouncing;
    private CrateBase Crate;
    private bool HasBounced = false;

    void Awake()
    {
        Bouncing = GetComponent<Bounce>();
        Crate = GetComponent<CrateBase>();
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
        else
        {
            if (!HasBounced)
            {
                Crate.BounceUp();
                HasBounced = true;
            }            
        }
    }

    void Bottom()
    {
        if (Bouncing)
        {
            Bouncing.Down();
        }
    }

    void Attack()
    {
        Debug.Log("Spin attack Interactable");
    }
}
