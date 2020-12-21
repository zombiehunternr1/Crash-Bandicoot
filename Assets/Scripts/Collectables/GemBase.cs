using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemBase : MonoBehaviour
{
    public ParticleSystem Effect;
    public GemCollected GemCollect;

    private LevelManager LevelManager;
    private Colored ColorGem;
    private Hidden HiddenGem;

    private void Awake()
    {
        ColorGem = GetComponent<Colored>();
        HiddenGem = GetComponent<Hidden>();
        LevelManager = GetComponentInParent<LevelManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerActions>())
        {
            Effect.Play();
            if (!ColorGem)
            {
                if (!HiddenGem)
                {
                    if (LevelManager != null)
                    {
                        GemCollect.GemsCollected.Add(gameObject.GetComponentInParent<Gem>().ID);
                        for (int i = 0; i < LevelManager.BoxCounters.Count; i++)
                        {
                            Destroy(LevelManager.BoxCounters[i].GetComponentInParent<CheckAmount>().gameObject);
                        }
                        DestroyGem();
                        return;
                    }
                    GemCollect.GemsCollected.Add(gameObject.GetComponentInParent<Gem>().ID);
                }
                GemCollect.GemsCollected.Add(gameObject.GetComponentInParent<Gem>().ID);
            }
            else
            {
                GemCollect.GemsCollected.Add(gameObject.GetComponentInParent<Gem>().ID);
            }           
        }
        DestroyGem();
    }

    private void DestroyGem()
    {
        gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
        Destroy(gameObject, 0.5f);
    }
}
