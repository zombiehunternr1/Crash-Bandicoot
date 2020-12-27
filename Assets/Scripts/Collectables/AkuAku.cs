using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AkuAku : MonoBehaviour
{
    public PlayerInfo Player;
    private LevelManager Manager;

    public void Awake()
    {
        Manager = FindObjectOfType<LevelManager>();
        Manager.AkuAkuCrateSpawns.Add(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        Manager.AkuAkuCrateSpawns.Remove(this);
        if (other.GetComponent<PlayerActions>())
        {
            if(Player.ExtraHit < 3)
            {
                Player.ExtraHit++;
                if(Player.ExtraHit == 1)
                {
                    gameObject.GetComponent<BoxCollider>().enabled = false;
                    gameObject.transform.position = new Vector3(other.transform.position.x + 1.5f, other.transform.position.y, other.transform.position.z);
                    gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    gameObject.transform.parent = other.transform;
                }
                else
                {
                    //Replace this with an new AkuAku material
                    gameObject.GetComponent<BoxCollider>().enabled = false;
                    gameObject.transform.position = new Vector3(other.transform.position.x + 1.5f, other.transform.position.y, other.transform.position.z);
                    gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    gameObject.transform.parent = other.transform;
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
