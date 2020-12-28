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
            if(Player.ExtraHit < 3)
            {
                Player.ExtraHit++;
                if (Player.ExtraHit == 1)
                {
                    gameObject.GetComponent<BoxCollider>().enabled = false;
                    gameObject.transform.parent = other.transform;
                    var X = Vector3.zero;
                    X.x = 1.5f;
                    gameObject.transform.localPosition = X;
                    transform.rotation = other.transform.rotation;
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
