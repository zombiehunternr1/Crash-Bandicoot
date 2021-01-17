using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakAmount : MonoBehaviour
{
    public GameEvent CrateDestroyed;
    public GameEvent UpdateUI;
    public PlayerActions Player;
    public PlayerInfo AddWoompa;
    public GameObject WoompaSFX;
    public GameObject LiveSFX;
    public GameObject BreakCrateSFX;
    [HideInInspector]
    public bool Activated = false;

    int GiveWoompa = 3;
    int TotalBounce = 5;
    float StartTime = 0f;
    float MaxTime = 5f;

    private void Awake()
    {
        Player = FindObjectOfType<PlayerActions>();
    }

    //Checks if the player already jumped on top of the crate.
    //If not it starts the Timer coroutine.
    public void BreakOverTime(int BounceCount)
    {
        if (!Activated)
        {
            //Checks if the starttime is greater or equal to the maxtime.
            //If so it means the crates were reset so the player should be allowed to jump on it again like it was the players first time.
            if(StartTime >= MaxTime)
            {
                StartTime = 0f;               
            }
            StartCoroutine(Timer());
        }
        //Checks if the starttime is greater the the maxtime. If so this means the player has exceeded the allowed time to jump on the crate.
        //That means the player will bounce of the crate, the cratedestroyed event will be raised, the gameobject will be deactivated and gives the player some Woompa fruit and the TimerWoompa to 5.
        if (StartTime > MaxTime)
        {
            Instantiate(BreakCrateSFX);
            CrateDestroyed.Raise();
            gameObject.SetActive(false);
            for(int i = 0; i < GiveWoompa; i++)
            {
                Instantiate(WoompaSFX);
                Player.TimerWoompa = 5f;
                AddWoompa.Woompa++;              
                UpdateUI.Raise();
            }
        }
        //Checks if the bouncecount is smaller then the totalbounce. If so that means the player jumped on the crate again in the allowed timespan.
        //This will reset the TimerWoompa to 5, startTime back to 0 and give the player some Woompa Fruit.
        //It also checks if the amount is smaller or equal to 99. If so it adds the woompa fruit. If not it sets the amount to 0, set the timerlife to 5 and adds a life to the player.
        //Afterwards it raises the event UpdateUI.
        else if(BounceCount < TotalBounce)
        {
            for (int i = 0; i < GiveWoompa; i++)
            {
                Instantiate(WoompaSFX);
                if (AddWoompa.Woompa <= 99)
                {
                    AddWoompa.Woompa++;
                }
                else
                {
                    Instantiate(LiveSFX);
                    Player.TimerLife = 5f;
                    AddWoompa.Woompa = 0;
                    AddWoompa.Lives++;                   
                }
                Player.TimerWoompa = 5f;
                AddWoompa.Woompa++;
                UpdateUI.Raise();
            }
            StartTime = 0f;
        }
        //Checks if the BounceCount is greater or equal to the TotalBounce. If so this means the player has jumped the allowed maximum times on the crate.
        //This will bounce the player of the crate, and then stop the Timer coroutine, raises the CrateDestroyed event, disables the gameobject.
        //It also checks if the amount is smaller or equal to 99. If so it adds the woompa fruit. If not it sets the amount to 0, set the timerlife to 5 and adds a life to the player.
        //Afterwards it raises the event UpdateUI.
        else if (BounceCount >= TotalBounce)
        {
            for (int i = 0; i < GiveWoompa; i++)
            {
                Instantiate(WoompaSFX);
                if (AddWoompa.Woompa <= 99)
                {
                    AddWoompa.Woompa++;
                }
                else
                {
                    Instantiate(LiveSFX);
                    Player.TimerLife = 5f;
                    AddWoompa.Woompa = 0;
                    AddWoompa.Lives++;
                }
                Player.TimerWoompa = 5f;
                AddWoompa.Woompa++;
                UpdateUI.Raise();
            }
            Instantiate(BreakCrateSFX);
            StopCoroutine(Timer());
            CrateDestroyed.Raise();
            gameObject.SetActive(false);
        }         
    }

    //Once this function gets called it has been hit by either an explosion or an enemy.
    public void BreakCrate()
    {
        Instantiate(BreakCrateSFX);
        StopCoroutine(Timer());
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
