using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AkuAku : MonoBehaviour
{
    public PlayerInfo PlayerInfo;
    private LevelManager Manager;
    private Stage[] Children;
    [HideInInspector]
    public DamagePlayer KillPlayer;
    //[HideInInspector]
    public bool NotInvinsible = true;

    private PlayerActions Player;
    private bool Done;
    private float TimeRemaining = 20f;

    public void Awake()
    {
        Children = GetComponentsInChildren<Stage>();
        Manager = FindObjectOfType<LevelManager>();
        KillPlayer = FindObjectOfType<DamagePlayer>();
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

    private void AddAkuAku()
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
            else if (PlayerInfo.ExtraHit == 3)
            {
                if (NotInvinsible)
                {
                    Player.GetComponentInChildren<AkuAku>().NotInvinsible = false;
                    KillPlayer.CanHit = false;
                    Destroy(gameObject);
                    Player.GetComponentInChildren<AkuAku>().transform.localPosition = new Vector3(0, -0.2f, 0.7f);
                    StartCoroutine(Player.GetComponentInChildren<AkuAku>().InvinsibilityTimer());
                }
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
        }
        else
        {
            PlayerInfo.ExtraHit--;
            Player.GetComponentInChildren<AkuAku>().transform.localPosition = new Vector3(1.5f, 0, 0);
            transform.rotation = Player.transform.rotation;
        }
    }

    public IEnumerator InvinsibilityTimer()
    {
        var AkuAku = Player.GetComponentInChildren<AkuAku>();
        if (!Done)
        {
            while (TimeRemaining > 0)
            {
                TimeRemaining -= Time.deltaTime;
            }
            TimeRemaining = 20f;
            Done = true;
            StartCoroutine(InvinsibilityTimer());
        }
        else
        {
            yield return new WaitForSeconds(TimeRemaining);
            AkuAku.WithdrawAkuAku();
            AkuAku.NotInvinsible = true;
            AkuAku.KillPlayer.CanHit = true;
        }
        yield return null;
    }
}
