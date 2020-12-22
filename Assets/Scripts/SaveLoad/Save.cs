using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save
{
    public int Lives;
    public int ExtraHit;
    public int Woompa;

    public Save (PlayerInfo Player)
    {
        Lives = Player.Lives;
        ExtraHit = Player.ExtraHit;
        Woompa = Player.Woompa;
    }
}
