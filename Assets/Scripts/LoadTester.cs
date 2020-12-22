using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadTester : MonoBehaviour
{
    public GameEvent UpdateHUD;
    public GemCollected Gems;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerActions>())
        {
            Save player = SaveSystem.LoadProgress();

            var PlayerInfo = other.GetComponent<PlayerStatus>().Player;

            PlayerInfo.Lives = player.Lives;
            PlayerInfo.ExtraHit = player.ExtraHit;
            PlayerInfo.Woompa = player.Woompa;
            UpdateHUD.Raise();

            for(int i = 0; i < player.GemsCollected.Count; i++)
            {
                Gems.GemsCollected.Add(i);
            }
        }
    }
}
