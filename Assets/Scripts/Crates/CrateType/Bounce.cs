﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    public PlayerActions Player;
    public GameEvent CrateDestroyed;

    //Once this function gets called it bounces the player up.
    public void Up()
    {
        Player.BounceUp();
    }
    //Once this function gets called it bounces the player down.
    public void Down()
    {
        Player.BounceUp();
    }

    //Once this function gets called it raises the crate destroyed event and disables the gameobject.
    public void breakCrate()
    {
        CrateDestroyed.Raise();
        gameObject.SetActive(false);
    }
}
