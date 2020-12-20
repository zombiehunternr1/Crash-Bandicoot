using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAmount : MonoBehaviour
{
    private BoxCounter Counter;

    void Start()
    { 
        Counter = GetComponentInChildren<BoxCounter>();   
    }

    //Checks if the object it's colliding with is the player.
    //If so it checks if the CurrentCrates is equal to the TotalCrates.
    //If so it means the player has destroyed all the crates in the level and the gem can be instanciated.
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<PlayerActions>())
        {
            Counter.SpawnGem();
        }
    }

    //Checks if the object it's colliding with is the player.
    //If so it checks if the CurrentCrates is equal to the TotalCrates.
    //If so it means the player has destroyed all the crates in the level and the gem can be instanciated.
    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.GetComponent<PlayerActions>())
        {
            Counter.SpawnGem();
        }
    } 
}
