using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Questionmark : MonoBehaviour
{
    public GameObject Woompa;
    public GameObject Life;
    public GameObject WoompaSFX;
    public GameObject LifeSFX;
    public GameEvent DestroyedCrate;
    public int DropWoompaAmount;

    public bool Lives;
    public bool Woompas;
    public bool AutoAdd;
    private PlayerInfo Player;

    private void Awake()
    {
        Player = GetComponent<Breakable>().Player;
    }

    public void DropItems()
    {
        if (Woompas)
        {
            if (!AutoAdd)
            {
                for (int i = 0; i < DropWoompaAmount; i++)
                {
                    var x = Random.Range(-0.5f, 0.5f);
                    var z = Random.Range(-0.5f, 0.5f);
                    Instantiate(Woompa, new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z), transform.rotation);
                }
            }
            else
            {
                for (int i = 0; i < DropWoompaAmount; i++)
                {
                    Instantiate(WoompaSFX);
                    Player.Woompa++;
                }
            }
            DestroyedCrate.Raise();
            gameObject.SetActive(false);

        }
        else if (Lives)
        {
            if (!AutoAdd)
            {
                Instantiate(Life, transform.position, transform.rotation);
            }
            else
            {
                Instantiate(LifeSFX);
                Player.Lives++;
            }
            DestroyedCrate.Raise();
            gameObject.SetActive(false);
        }
    }

    public void BreakCrate()
    {
        DestroyedCrate.Raise();
        gameObject.SetActive(false);
    }
}
