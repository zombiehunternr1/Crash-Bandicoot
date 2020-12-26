using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public GameEvent PlayerHit;
    public PlayerActions Player;

    public bool Instakill;

    private float Invulnerable = 3;
    private float Blink = 0.2f;
    private bool CanHit = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerActions>())
        {
            Player = other.GetComponent<PlayerActions>();
            PlayerGotHit();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerActions>())
        {
            Player = collision.gameObject.GetComponent<PlayerActions>();
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
                if (Player.GetComponent<PlayerStatus>().Player.ExtraHit <= 0)
                {
                    Player.GetComponent<PlayerActions>().CanMove = false;
                    PlayerHit.Raise();
                }
                else
                {
                    if (CanHit)
                    {
                        StartCoroutine(Tempinvulnerability(Invulnerable, 0.2f));
                        Player.GetComponent<PlayerStatus>().Player.ExtraHit--;
                    }
                }
            }
            else
            {
                //Kill enemy.
            }
        }
    }

    //This coroutine temporarely turns the mesh of the player on and off again until the Invulnerable time isn't greater then 0 anymore. After that it enables all the meshes and set the bool CanHit back to true.
    IEnumerator Tempinvulnerability(float Duration, float Blink)
    {
        CanHit = false;
        SkinnedMeshRenderer Mesh = Player.gameObject.GetComponentInChildren<SkinnedMeshRenderer>();  

        while(Invulnerable > 0f)
        {
            if(Invulnerable > 0f)
            {
                Mesh.enabled = !Mesh.enabled;
                Invulnerable -= Time.deltaTime;
            }
        }
        Mesh.enabled = true;
        yield return null;
    }
}
