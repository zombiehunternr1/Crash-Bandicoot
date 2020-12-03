using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    public GameObject Player;

    //Once this function gets called it bounces the player up.
    public void Up()
    {
        Player.GetComponent<PlayerActions>().BounceUp();
    }
    //Once this function gets called it bounces the player down.
    public void Down()
    {
        Player.GetComponent<PlayerActions>().BounceDown();
    }
}
