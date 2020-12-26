using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AkuAku : MonoBehaviour
{
    public PlayerInfo Player;
    private LevelManager Manager;

    public void Awake()
    {
        Manager = FindObjectOfType<LevelManager>();
        Manager.AkuAkuCrateSpawns.Add(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        Manager.AkuAkuCrateSpawns.Remove(this);
        if (other.GetComponent<PlayerActions>())
        {
            if(Player.ExtraHit != 3)
            {
                Player.ExtraHit++;
                gameObject.GetComponent<BoxCollider>().enabled = false;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
