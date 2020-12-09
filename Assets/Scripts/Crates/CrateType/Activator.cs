﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    public List<GameObject> Crates = new List<GameObject>();
    public GameObject InActiveCrate;
    public Material Inactive;
    public float WaitTillNextActivate;

    private Material[] CrateMaterial;
    private Renderer Rend;
    private GameObject InactiveActivator;

    private void Awake()
    {
        DeactivateCrates();
    }

    //Once this function gets called it instanciates the InActiveCrate object at the same position the Activator crate is.
    //Then it starts the coroutine ActivateOverTime.
    public void ActivateCrates()
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;
        InactiveActivator = Instantiate(InActiveCrate, transform.position, transform.rotation);
        StartCoroutine(ActivateOverTime());
    }
    //Once this function gets called it enables the activator crate and destroys the inactive activator crate.
    //It then goes over each crate in the list and checks if it doesn't return null. If it doesn't it means that crate hasn't been destroyed yet.
    //It then adds the inactive metarial to each crate in the list.
    //It then creates a temporarely filterlist that adds all the gameobjects from the list Crates that don't return null to it's list.
    //Afterwards I set the Crates list equal to the FilterList.
    //If the list is equal to zero it means all the crates have been destroyed so the inactive crate can be placed instead of the activator crate.
    public void DeactivateCrates()
    {
        gameObject.SetActive(true);
        Destroy(InactiveActivator);

        var FilterList = new List<GameObject>();

        foreach (GameObject ChangeCrate in Crates)
        {
            if(ChangeCrate != null)
            {
                if (ChangeCrate.GetComponent<Renderer>())
                {
                    Rend = ChangeCrate.GetComponent<Renderer>();
                    CrateMaterial = Rend.materials;
                    Rend.material = Inactive;
                    ChangeCrate.GetComponent<BoxCollider>().enabled = false;
                    FilterList.Add(ChangeCrate);
                }
                else
                {
                    Rend = ChangeCrate.GetComponentInChildren<Renderer>();
                    CrateMaterial = Rend.materials;
                    Rend.material = Inactive;
                    ChangeCrate.GetComponent<BoxCollider>().enabled = false;
                    FilterList.Add(ChangeCrate);
                }               
            }
        }
        Crates = FilterList;
        if(Crates.Count == 0)
        {
            InactiveActivator = Instantiate(InActiveCrate, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
    
    //Once this coroutine gets called it goes over each crate in the list, changes the metarial from inactive to it's own metarial.
    //Afterwards it disables the activator crate.
    IEnumerator ActivateOverTime()
    {
        foreach (GameObject ChangeCrate in Crates)
        {
            if(ChangeCrate != null)
            {
                if (ChangeCrate.GetComponent<Renderer>())
                {
                    Rend = ChangeCrate.GetComponent<Renderer>();
                    Rend.material = CrateMaterial[0];
                    ChangeCrate.GetComponent<BoxCollider>().enabled = true;
                }
                else
                {
                    Rend = ChangeCrate.GetComponentInChildren<Renderer>();
                    Rend.material = CrateMaterial[0];
                    ChangeCrate.GetComponent<BoxCollider>().enabled = true;
                }
            }       
            yield return new WaitForSeconds(WaitTillNextActivate);
        }
        gameObject.SetActive(false);
    }
}
