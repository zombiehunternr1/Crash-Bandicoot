using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nitro : MonoBehaviour
{
    [HideInInspector]
    public bool HasExploded = false;
    public GameEvent CrateDestroyed;
    public ParticleSystem ExplosionEffect;
    private ParticleSystem Explode;
    private Transform Crate;

    void Start()
    {
        Crate = GetComponent<Transform>();
    }

    //Once this function gets called it checks if the crate hasn't already exploded.
    //If not it instanciates the explosion effect on the crates position and raises the crate destroyed event and disables the gameobject.
    public void ExplodeCrate()
    {
        if (!HasExploded)
        {
            HasExploded = true;
            Explode = Instantiate(ExplosionEffect, Crate.transform.position, Crate.transform.rotation);
            CrateDestroyed.Raise();
            gameObject.SetActive(false);
        }
    }
}
