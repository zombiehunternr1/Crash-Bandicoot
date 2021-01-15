using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AkuAku : MonoBehaviour
{
    public PlayerInfo PlayerInfo;
    public GameObject WithdrawSFX;
    public AudioSource AddSource;
    public AudioSource WithdrawSource;
    public AudioSource InvincibilitySource;

    [HideInInspector]
    public bool NotInvinsible = true;
    [HideInInspector]
    public bool Flashing;

    private Stage[] Children;
    private LevelManager Manager;
    private SkinnedMeshRenderer CrashSkin;
    private PlayerActions Player;
    private bool Done;
    private float TimeRemaining = 20f;

    public void Awake()
    {
        Player = FindObjectOfType<PlayerActions>();
        CrashSkin = Player.GetComponentInChildren<SkinnedMeshRenderer>();
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

    private void AddAkuAku()
    {       
        if (PlayerInfo.ExtraHit < 3)
        {           
            PlayerInfo.ExtraHit++;
            if (PlayerInfo.ExtraHit == 1)
            {
                AddSource.Play();
                PositionFirstMask();
            }
            else if (PlayerInfo.ExtraHit == 2)
            {
                Player.GetComponentInChildren<AkuAku>().AddSource.Play();
                Destroy(gameObject);
                Player.GetComponentInChildren<AkuAku>().Children[1].gameObject.SetActive(true);
            }
            else if (PlayerInfo.ExtraHit == 3)
            {
                Player.GetComponentInChildren<AkuAku>().InvincibilitySource.Play();
                if (NotInvinsible)
                {
                    Player.GetComponentInChildren<AkuAku>().NotInvinsible = false;
                    Player.CanHit = false;
                    Player.GetComponentInChildren<AkuAku>().transform.localPosition = new Vector3(0, -0.2f, 0.7f);
                    Player.GetComponentInChildren<AkuAku>().StartCoroutine(Player.GetComponentInChildren<AkuAku>().InvinsibilityTimer(TimeRemaining));
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            Instantiate(WithdrawSFX);
            Destroy(gameObject);
        }
    }

    public void WithdrawAkuAku()
    {
        if(PlayerInfo.ExtraHit < 3)
        {
            Player.GetComponentInChildren<AkuAku>().WithdrawSource.Play();
            PlayerInfo.ExtraHit--;
            if(PlayerInfo.ExtraHit == 0)
            {
                Instantiate(WithdrawSFX);
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
            Player.GetComponentInChildren<AkuAku>().WithdrawSource.Play();
            PlayerInfo.ExtraHit--;
            Player.GetComponentInChildren<AkuAku>().transform.localPosition = new Vector3(1.5f, 0, 0);
            transform.rotation = Player.transform.rotation;
        }
    }

    public IEnumerator InvinsibilityTimer(float TimeRemaining)
    {
        Flashing = true;
        Player.PlayerAnimator.SetBool("Flashing", Flashing);
        yield return new WaitForSeconds(TimeRemaining);
        Flashing = false;
        Player.PlayerAnimator.SetBool("Flashing", Flashing);
        Player.GetComponentInChildren<AkuAku>().WithdrawAkuAku();
        Player.GetComponentInChildren<AkuAku>().NotInvinsible = true;
        Player.GetComponentInChildren<AkuAku>().Player.CanHit = true;
        yield return null;
    }

    public void PositionFirstMask()
    {
        Children[1].gameObject.SetActive(false);
        gameObject.GetComponent<BoxCollider>().enabled = false;
        gameObject.transform.parent = Player.transform;
        var X = Vector3.zero;
        X.x = 1.5f;
        gameObject.transform.localPosition = X;
        transform.rotation = Player.transform.rotation;
    }
}
