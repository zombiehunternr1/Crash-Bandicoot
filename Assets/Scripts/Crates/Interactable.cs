using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
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

    void Attack()
    {
        Debug.Log("Spin attack Interactable");
    }
}
