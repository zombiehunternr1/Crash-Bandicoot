using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tnt : MonoBehaviour
{
    public GameEvent CrateDestroyed;
    public ParticleSystem ExplosionEffect;
    [HideInInspector]
    public bool HasExploded = false;
    //[HideInInspector]
    public bool HasStarted = false;
    private Animation TntCountdown;
    private ParticleSystem Explode;
    private Transform Crate;
    private MeshRenderer[] GetChildren;
    private List<MeshRenderer> Countdown = new List<MeshRenderer>();

    //Gets the animation that is attached to itself and stores it in the variable TntCoundown.
    //Gets the transform position of itself and stores it in the Transform variable Crate.
    //Gets all the MeshRenderers from it's children and itself and stores it in it's own variables.
    //Goes over each child object and adds it to the MeshRenderer list and disables the mesh renderer.
    //Afterwards it gets the first in the list and enables the mesh renderer.
    void Awake()
    {
        TntCountdown = GetComponent<Animation>();
        Crate = GetComponent<Transform>();
        GetChildren = GetComponentsInChildren<MeshRenderer>();

        foreach(MeshRenderer Mat in GetChildren)
        {
            Countdown.Add(Mat);
            Mat.enabled = false;
        }
        Countdown[0].enabled = true;
    }

    //Once this function gets called it checks if the bool Hasstarted is false. If so it plays the animation.
    public void Activate()
    {
        if (!HasStarted)
        {
            TntCountdown.Play();
            //StartCoroutine(StartCountdown());
        }
        
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
    //This function sets the HasExploded boolean to false, stops the coroutine StartCountdown, enables the first tnt mesh and disables the other onces.
    public void ResetCountdown()
    {
        HasExploded = false;
        StopCoroutine(StartCountdown());
        Countdown[0].enabled = true;
        Countdown[1].enabled = false;
        Countdown[2].enabled = false;
        Countdown[3].enabled = false;
    }

    //This coroutine disables each meshrenderer after a certain amount of seconds has passed.
    //Once it reaches the last one it disables the gameobject, instanciates the explosion effect, calls the ExploteCrate function.
    private IEnumerator StartCountdown()
    {
        HasStarted = true;
        Countdown[0].enabled = false;
        Countdown[1].enabled = true;
        yield return new WaitForSeconds(1);
        Countdown[1].enabled = false;
        Countdown[2].enabled = true;
        yield return new WaitForSeconds(1);
        Countdown[2].enabled = false;
        Countdown[3].enabled = true;
        yield return new WaitForSeconds(1);
        ExplodeCrate();     
    }
}
