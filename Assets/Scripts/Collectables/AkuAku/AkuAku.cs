using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AkuAku : MonoBehaviour
{
    public PlayerInfo PlayerInfo;
    private LevelManager Manager;
    public Stage[] Children;

    private PlayerActions Player;

    public void Awake()
    {
        Children = GetComponentsInChildren<Stage>();
        Manager = FindObjectOfType<LevelManager>();
        Manager.AkuAkuCrateSpawns.Add(this);
        Children[1].gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Manager.AkuAkuCrateSpawns.Remove(this);
        if (other.GetComponent<PlayerActions>())
        {
            Player = other.GetComponent<PlayerActions>();
            AddAkuAku();
        }
    }

    public void AddAkuAku()
    {
        if (PlayerInfo.ExtraHit < 3)
        {
            PlayerInfo.ExtraHit++;
            if (PlayerInfo.ExtraHit == 1)
            {
                Children[1].gameObject.SetActive(false);
                gameObject.GetComponent<BoxCollider>().enabled = false;
                gameObject.transform.parent = Player.transform;
                var X = Vector3.zero;
                X.x = 1.5f;
                gameObject.transform.localPosition = X;
                transform.rotation = Player.transform.rotation;
            }
            else if (PlayerInfo.ExtraHit == 2)
            {
                Destroy(gameObject);
                Player.GetComponentInChildren<AkuAku>().Children[1].gameObject.SetActive(true);
            }
            else if(PlayerInfo.ExtraHit == 3)
            {
                Destroy(gameObject);
                Player.GetComponentInChildren<AkuAku>().transform.localPosition = new Vector3(0, -0.2f, 0.7f);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void WithdrawAkuAku()
    {
        if(PlayerInfo.ExtraHit < 3)
        {
            PlayerInfo.ExtraHit--;
            if(PlayerInfo.ExtraHit == 0)
            {
                Destroy(Player.GetComponentInChildren<AkuAku>().gameObject);
            }
            else if(PlayerInfo.ExtraHit == 1)
            {
                if (Player.GetComponentInChildren<AkuAku>())
                {
                    Player.GetComponentInChildren<AkuAku>().Children[1].gameObject.SetActive(false);
                }              
            }
            else if(PlayerInfo.ExtraHit == 2)
            {
                Player.GetComponentInChildren<AkuAku>().gameObject.transform.parent = Player.transform;
                var X = Vector3.zero;
                X.x = 1.5f;
                Player.GetComponentInChildren<AkuAku>().gameObject.transform.localPosition = X;
                transform.rotation = Player.GetComponentInChildren<AkuAku>().transform.rotation;
            }
        }
    }
}
