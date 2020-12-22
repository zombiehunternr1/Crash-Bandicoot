using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemBase : MonoBehaviour
{
    public ParticleSystem Effect;
    public GemCollected GemCollect;

    private GemSystem GemsAvailable;
    private LevelManager LevelManager;

    private void Awake()
    {
        LevelManager = GetComponentInParent<LevelManager>();
        GemsAvailable = GetComponentInParent<GemSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerActions>())
        {
            GetComponent<BoxCollider>().enabled = false;
            Effect.Play();
            GemCollect.GemsCollected.Add(this.GetComponentInParent<Gem>().ID);
            StartCoroutine(DisableGem());
        }
    }

    IEnumerator DisableGem()
    {
        GetComponentInChildren<MeshRenderer>().enabled = false;      
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}
