using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakAmount : MonoBehaviour
{
    int TotalBounce = 5;
    float StartTime = 0f;
    float MaxTime = 5f;
    bool Activated = false;


    public void BreakOverTime(int BounceCount)
    {
        if (!Activated)
        {
            StartCoroutine(Timer());
        }

        if (StartTime > MaxTime)
        {
            //Give player apples and break crate
        }
        else if(BounceCount < TotalBounce)
        {
            //Give player apples
            StartTime = 0f;
        }
        else if (BounceCount >= TotalBounce)
        {
            StopCoroutine(Timer());
            //Give player apples and break crate
            //Destroy crate.
        }         
    }

    IEnumerator Timer()
    {
        if (!Activated)
        {
            Activated = true;
            while (StartTime < MaxTime)
            {
                StartTime += Time.deltaTime;
                yield return StartTime;
            }
        }    
    }
}
