using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private SphereCollider ExplosionArea;
    private int ExplodeCrate = 8;

    //Gets the collider and stores it in the variable ExplosionArea.
    //Afterwards it destroys itself after 1 second.
    void Awake()
    {
        ExplosionArea = GetComponent<SphereCollider>();
        Destroy(gameObject, 1f);
    }

    //Checks if anything has entered the trigger area.
    //If so it checks what entered it to send out the appropiate responds.
    private void OnTriggerEnter(Collider other)
    {
        //If it's an crate it gets the breakable or interactable script component and calls the function CrateAction.
        if (other.GetComponent<Breakable>())
        {
            var ExplodeAction = other.GetComponent<Breakable>();
            ExplodeAction.CrateAction(ExplodeCrate);
        }
        else if (other.GetComponent<Interactable>())
        {
            var ExplodeAction = other.GetComponent<Interactable>();
            ExplodeAction.CrateAction(ExplodeCrate);
        }
        else if (other.GetComponent<EnemyBase>())
        {
            Debug.Log("I am an enemy");
        }
        else if (other.GetComponent<PlayerActions>())
        {
            //Build in check to see if the player has invinsibility active
            //If so it shouldn't kill the player.
            //If the player has an extra hit available destroy the extra hit.
            Debug.Log("I am the player");
        }
    }
}