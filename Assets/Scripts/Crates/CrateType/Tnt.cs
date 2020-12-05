using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tnt : MonoBehaviour
{
    public GameEvent CrateDestroyed;
    public ParticleSystem ExplosionEffect;
    private ParticleSystem Explode;
    private Transform Crate;
    private MeshRenderer[] GetChildren;
    private List<MeshRenderer> Countdown = new List<MeshRenderer>();
    private Renderer TntRend;
    private bool HasExploded = false;

    //Gets the transform position of itself and stores it in the Transform variable Crate.
    //Gets all the MeshRenderers from it's children and itself and stores it in it's own variables.
    //Goes over each child object and adds it to the MeshRenderer list and removes the first one at the end because that one is the parent object.
    void Awake()
    {
        Crate = GetComponent<Transform>();
        TntRend = GetComponent<Renderer>();
        GetChildren = GetComponentsInChildren<MeshRenderer>();

        foreach(MeshRenderer Mat in GetChildren)
        {
            Countdown.Add(Mat);
        }
        Countdown.RemoveAt(0);
    }

    //Once this function gets called it starts the coroutine StartCountdown.
    public void Activate()
    {
        StartCoroutine(StartCountdown());
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

    //This coroutine disables each meshrenderer after a certain amount of seconds has passed.
    //Once it reaches the last one it disables the gameobject, instanciates the explosion effect, calls the ExploteCrate function.
    private IEnumerator StartCountdown()
    {
        TntRend.enabled = false;
        yield return new WaitForSeconds(1);
        Countdown[0].enabled = false;
        yield return new WaitForSeconds(1);
        Countdown[1].enabled = false;
        yield return new WaitForSeconds(1);
        ExplodeCrate();     
    }
}
