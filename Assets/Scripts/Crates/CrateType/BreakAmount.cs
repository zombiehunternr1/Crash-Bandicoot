using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakAmount : MonoBehaviour
{
    int TotalBounce = 5;
    float StartTime = 0f;
    float MaxTime = 5f;
    bool Activated = false;

    //Checks if the player already jumped on top of the crate.
    //If not it starts the Timer coroutine.
    //If the player hasn't jump the maximum amount on the crate it gives the player Woompa fruit and resets the timer.
    //If the player has reached the maximum amount the Timer coroutine will stop and the crate will break.
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
    //Sets the activated boolean to true so the coroutine can't be called again once the player jumps on the crate again.
    //Keeps looping until the StartTime is higher then the MaxTime.
    IEnumerator Timer()
    {
        Activated = true;
        while (StartTime < MaxTime)
        {
            StartTime += Time.deltaTime;
            yield return StartTime;
        }          
    }
}
