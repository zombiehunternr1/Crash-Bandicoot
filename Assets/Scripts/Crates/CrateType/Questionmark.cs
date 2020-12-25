using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Questionmark : MonoBehaviour
{
    public GameObject Woompa;
    public GameObject Live;
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
                    var x = Random.Range(-1f, 1f);
                    var z = Random.Range(-1f, 1f);
                    Instantiate(Woompa, new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z), transform.rotation);
                }
            }
            else
            {
                for (int i = 0; i < DropWoompaAmount; i++)
                {
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
                Instantiate(Live, transform.position, transform.rotation);
            }
            else
            {
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
