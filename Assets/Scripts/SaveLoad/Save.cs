using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save
{
    public int Lives;
    public int ExtraHit;
    public int Woompa;
    public List<int> GemsCollected = new List<int>();

    public Save (PlayerInfo Player, GemCollected Gems)
    {
        Lives = Player.Lives;
        ExtraHit = Player.ExtraHit;
        Woompa = Player.Woompa;

        for (int i = 0; i < Gems.GemsCollected.Count; i++)
        {
            GemsCollected.Add(i);
        }
    }
}
