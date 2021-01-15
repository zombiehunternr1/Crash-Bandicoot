using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AkuAkuCrate : MonoBehaviour
{
    public GameEvent DestroyedCrate;
    public GameObject AkuAku;
    public GameObject AkuAkuSFX;
    public PlayerInfo Player;
    public bool AutoAdd = false;
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

    public void AddItem()
    {
        if(Player.ExtraHit != 3)
        {
            Instantiate(AkuAkuSFX);
            var Temp = Instantiate(AkuAku, Crate.position, Crate.rotation);
            Temp.GetComponent<AkuAku>().PositionFirstMask();
            Player.ExtraHit++;
        }
        DestroyedCrate.Raise();
        gameObject.SetActive(false);
    }
}
