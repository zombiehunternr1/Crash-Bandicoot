using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load : MonoBehaviour
{
    public GameEvent UpdateHUD;
    public PlayerInfo PlayerProgress;
    public GemCollected Gems;

    public void LoadProgress()
    {
        Save player = SaveSystem.LoadProgress();

        PlayerProgress.Lives = player.Lives;
        PlayerProgress.ExtraHit = player.ExtraHit;
        PlayerProgress.Woompa = player.Woompa;
        UpdateHUD.Raise();

        Gems.GemsCollected = new List<int>();

        for (int i = 0; i < player.GemsCollected.Count; i++)
        {
            Gems.GemsCollected.Add(i);
        }
    }
}
