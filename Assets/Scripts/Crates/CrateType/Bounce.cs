﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    public GameObject Player;

    public void Up()
    {
        Player.GetComponent<PlayerActions>().BounceUp();
    }

    public void Down()
    {
        Player.GetComponent<PlayerActions>().BounceDown();
    }
}
