﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakAmount : MonoBehaviour
{
    public GameEvent CrateDestroyed;

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
            CrateDestroyed.Raise();
            gameObject.SetActive(false);
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
            CrateDestroyed.Raise();
            gameObject.SetActive(false);
            //Give player apples and break crate
        }         
    }

    //Once this function gets called it has been hit by either an explosion or an enemy.
    public void BreakCrate()
    {
        CrateDestroyed.Raise();
        gameObject.SetActive(false);
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
