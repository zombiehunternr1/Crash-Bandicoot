using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalCrate : MonoBehaviour
{
    public GameEvent CrateReset;
    private float CurrentHeight;
    private float PreviousHeight;
    private bool FallingDown = false;

    private void FixedUpdate()
    {
        if (PreviousHeight > transform.position.y)
        {
            FallingDown = true;
        }
        else if (PreviousHeight < transform.position.y)
        {
            FallingDown = false;
        }
        CurrentHeight = transform.position.y;
        PreviousHeight = CurrentHeight;
    }

    public void IsFallingDown()
    {
        if (FallingDown)
        {
            CrateReset.Raise();
        }
    }
}
