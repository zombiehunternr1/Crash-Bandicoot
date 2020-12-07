using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalCrate : MonoBehaviour
{
    public GameEvent PlayerHit;
    public GameEvent CrateReset;
    private float CurrentHeight;
    private float PreviousHeight;
    private bool FallingDown = false;

    private void Awake()
    {
        StartCoroutine(IsBouncing());
    }

    public void IsFallingDown()
    {
        if (FallingDown)
        {
            CrateReset.Raise();
            PlayerHit.Raise();
        }
    }

    IEnumerator IsBouncing()
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
        yield return new WaitForSeconds(.1f);
        StartCoroutine(IsBouncing());
    }
}
