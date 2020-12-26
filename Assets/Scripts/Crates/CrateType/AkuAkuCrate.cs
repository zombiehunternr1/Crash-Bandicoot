using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AkuAkuCrate : MonoBehaviour
{
    public GameEvent DestroyedCrate;
    public GameObject AkuAku;
    public PlayerInfo Player;
    private Transform Crate;

    private void Awake()
    {
        Crate = GetComponent<Transform>();
    }

    public void DropItem()
    {
        var Temp = Instantiate(AkuAku, Crate.position, Crate.rotation);
        Temp.transform.parent = Crate.parent;
        DestroyedCrate.Raise();
        gameObject.SetActive(false);
    }

    public void AutoAdd()
    {
        if(Player.ExtraHit != 3)
        {
            Player.ExtraHit++;
        }
        DestroyedCrate.Raise();
        gameObject.SetActive(false);
    }
}
