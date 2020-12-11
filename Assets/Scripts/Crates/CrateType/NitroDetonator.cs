using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NitroDetonator : MonoBehaviour
{
    public GameObject LevelCrates;
    public GameObject InactiveNitrocrate;
    public GameObject Effect;
    private GameObject InactiveDetonator;
    private Nitro[] NitroCrates;
    private List<Nitro> NitroCrateList = new List<Nitro>();

    //Gets all the objects that has the Nitro script attached to itself, if so it stores it in the array NitroCrates.
    //Afterwards it loops over the array and adds it to the gameobject to the list NitroCrateList.
    void Awake()
    {
        NitroCrates = LevelCrates.GetComponentsInChildren<Nitro>();

        foreach(Nitro crate in NitroCrates)
        {
            NitroCrateList.Add(crate);
        }
    }

    //Once this function gets called it instanciates the effect, goes over every nitro crate in the list and if it isn't enmpty it blows up the crate.
    //Afterwards it instanciates a deactivated nitro detonator crate at the same position as the detonator and disables the detonator crate.
    public void DestroyAllNitroCrates()
    {
        Instantiate(Effect, transform.position + (Vector3.down * 0.5f), Effect.transform.rotation);

        foreach (Nitro crate in NitroCrateList)
        {
            if (crate != null)
            {
                crate.GetComponent<Nitro>().ExplodeCrate();         
            }           
        }
        InactiveDetonator = Instantiate(InactiveNitrocrate, transform.position, transform.rotation);
        gameObject.SetActive(false);
    }

    //Once this function gets called it re-enables the detonaor crate and destroys the deactivated detonator crate.
    public void ResetDetonator()
    {
        gameObject.SetActive(true);
        Destroy(InactiveDetonator);
    }
}
