using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public GameEvent PlayerHit;
    public bool Instakill;

    private PlayerActions Player;
    private float Invulnerable = 3;
    private float Blink = 1f;
    private bool CanHit = true;

    private Renderer[] Rend;

    private void Awake()
    {
        Player = FindObjectOfType<PlayerActions>();
        Rend = Player.GetComponentsInChildren<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerActions>())
        {
            PlayerGotHit();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerActions>())
        {
            PlayerGotHit();
        }
    }

    //When this fucntion gets called it checks if it's an instakill for the player or not. If it is it ignores the players invincibility and kills the player. It also sets the extrahit count back to 0 and raises the event Playerhit.
    //If not then it checks if the players doesn't have more then 3 extra hit points, if so it means the player is in invinsibility mode and can't be killed so the enemy dies. If not it checks if the extra hit count is lower or equal to 0.
    //If so it means the player dies so the canmove will be set to false and the event PlayerHit will get raised. If not it checks if CanHit is true. If so it starts the coroutine invulnerability and takes of one extra hit from the player.
    public void PlayerGotHit()
    {
        Player = FindObjectOfType<PlayerActions>();
        if (Instakill)
        {
            //Add remove invincibility functionality.
            Player.CanMove = false;
            Player.GetComponent<PlayerStatus>().Player.ExtraHit = 0;
            PlayerHit.Raise();
        }
        else
        {
            if (Player.GetComponent<PlayerStatus>().Player.ExtraHit != 3)
            {
                if (Player.GetComponent<PlayerStatus>().Player.ExtraHit == 0)
                {
                    Player.GetComponent<PlayerActions>().CanMove = false;
                    PlayerHit.Raise();
                }
                else
                {
                    if (CanHit)
                    {
                        Player.GetComponent<PlayerStatus>().Player.ExtraHit--;
                        StartCoroutine(TempInvulnerability());
                        //Kill enemy if allowed.
                    }
                }
            }
            else
            {
                //Kill enemy if allowed.
            }
        }
    }

    //This coroutine Displays the player being hit and is temporarely invulnerability to other enemies to give the player a fair change to save himself for a short period of time.
    IEnumerator TempInvulnerability()
    {
        CanHit = false;
        while (Invulnerable > 0)
        {
            Invulnerable -= Time.deltaTime;
            foreach (Renderer rend in Rend)
            {
                rend.enabled = !rend.enabled;
                new WaitForSeconds(Blink);
            }
            yield return null;
        }
        foreach (Renderer rend in Rend)
        {
            rend.enabled = true;
        }
        CanHit = true;
        Invulnerable = 3;
        yield return null;
    }
}
