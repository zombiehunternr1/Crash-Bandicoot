using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public GameEvent PlayerHit;

    public bool Instakill;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerActions>())
        {
            if (Instakill)
            {
                //Add remove invincibility functionality.
                other.GetComponent<PlayerActions>().CanMove = false;
                other.GetComponent<PlayerStatus>().Player.ExtraHit = 0;
                PlayerHit.Raise();
            }
            else
            {
                if(other.GetComponent<PlayerStatus>().Player.ExtraHit != 3)
                {
                    if (other.GetComponent<PlayerStatus>().Player.ExtraHit <= 0)
                    {
                        other.GetComponent<PlayerActions>().CanMove = false;
                        PlayerHit.Raise();
                    }
                    else
                    {
                        other.GetComponent<PlayerStatus>().Player.ExtraHit--;
                    }
                }
                else
                {
                    //Kill enemy.
                }
            }         
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerActions>())
        {
            if (Instakill)
            {
                //Add remove invincibility functionality.
                collision.gameObject.GetComponent<PlayerActions>().CanMove = false;
                collision.gameObject.GetComponent<PlayerStatus>().Player.ExtraHit = 0;
                PlayerHit.Raise();
            }
            else
            {
                if (collision.gameObject.GetComponent<PlayerStatus>().Player.ExtraHit != 3)
                {
                    if (collision.gameObject.GetComponent<PlayerStatus>().Player.ExtraHit <= 0)
                    {
                        collision.gameObject.GetComponent<PlayerActions>().CanMove = false;
                        PlayerHit.Raise();
                    }
                    else
                    {
                        collision.gameObject.GetComponent<PlayerStatus>().Player.ExtraHit--;
                    }
                }
                else
                {
                    //Kill enemy.
                }
            }
        }
    }
}
