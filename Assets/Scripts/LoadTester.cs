using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadTester : MonoBehaviour
{
    public GameEvent UpdateHUD;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerActions>())
        {
            Save player = SaveSystem.LoadPlayer();

            var PlayerInfo = other.GetComponent<PlayerStatus>().Player;

            PlayerInfo.Lives = player.Lives;
            PlayerInfo.ExtraHit = player.ExtraHit;
            PlayerInfo.Woompa = player.Woompa;
            UpdateHUD.Raise();
        }
    }
}
