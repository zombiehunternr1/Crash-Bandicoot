using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NitroDetonator : MonoBehaviour
{
    public GameObject LevelCrates;
    public GameObject InactiveNitrocrate;
    private GameObject InactiveDetonator;
    private Nitro[] NitroCrates;
    private List<Nitro> NitroCrateList = new List<Nitro>();

    void Awake()
    {
        NitroCrates = LevelCrates.GetComponentsInChildren<Nitro>();

        foreach(Nitro crate in NitroCrates)
        {
            NitroCrateList.Add(crate);
        }
    }

    public void DestroyAllNitroCrates()
    {
        foreach(Nitro crate in NitroCrateList)
        {
            if (crate != null)
            {
                crate.GetComponent<Nitro>().ExplodeCrate();
            }           
        }
        InactiveDetonator = Instantiate(InactiveNitrocrate, transform.position, transform.rotation);
        gameObject.SetActive(false);
        InactiveDetonator.SetActive(true);
    }

    public void ResetDetonator()
    {
        gameObject.SetActive(true);
        Destroy(InactiveDetonator);
    }
}
