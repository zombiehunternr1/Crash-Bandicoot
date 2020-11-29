using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakAmount : MonoBehaviour
{
    int TotalBounce = 5;
    float StartTime = 0f;
    float MaxTime = 5f;
    bool Activated;


    public void BreakOverTime(int BounceCount)
    {
        if (!Activated)
        {
            StartTime = 0;
            StartCoroutine(Timer());
            Activated = true;
        }

        if(BounceCount < TotalBounce)
        {           
            //Give player apples
            //reset timer to 0
            Debug.Log(BounceCount);
        }
        else if (BounceCount >= TotalBounce)
        {
            //Give player apples and break crate
            Debug.Log("Crate broken");
        }
        if (StartTime >= MaxTime)
        {
            //Give player apples and break crate
            Debug.Log("Time expired");
        }     
    }

    IEnumerator Timer()
    {
        if (!Activated)
        {
            while (StartTime <= MaxTime)
            {
                StartTime += Time.deltaTime;
                yield return StartTime;
                if(StartTime >= MaxTime)
                {
                    Activated = true;
                }
            }
        }    
    }
}
