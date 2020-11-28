using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [HideInInspector]
    public int CrateValue;

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
        Debug.Log("Hit Interactable Top");
    }

    void Bottom()
    {
        Debug.Log("Hit Interactable Bottom");
    }

    void Attack()
    {
        Debug.Log("Spin attack Interactable");
    }
}
