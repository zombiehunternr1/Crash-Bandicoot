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
    private Colored ColorGem;
    private Hidden HiddenGem;

    private void Awake()
    {
        ColorGem = GetComponent<Colored>();
        HiddenGem = GetComponent<Hidden>();
        LevelManager = GetComponentInParent<LevelManager>();
        GemsAvailable = GetComponentInParent<GemSystem>();
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
                        for (int j = 0; j < GemsAvailable.Gems.Count; j++)
                        {
                            if (GemCollect.GemsCollected.Contains(j))
                            {
                                if (GemsAvailable.Gems[j].GetComponent<Gem>().GemColour.Equals(GemColour.WhiteBox))
                                {
                                    if (GemCollect.GemsCollected.Contains(GemsAvailable.Gems[j].GetComponent<Gem>().ID))
                                    {
                                        for (int i = 0; i < LevelManager.BoxCounters.Count; i++)
                                        {
                                            Destroy(LevelManager.BoxCounters[i].GetComponentInParent<CheckAmount>().gameObject);
                                        }
                                        return;
                                    }
                                    else
                                    {
                                        GemCollect.GemsCollected.Add(gameObject.GetComponentInParent<Gem>().ID);
                                    }
                                }
                            }
                        }
                        DisableGem();
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
        DisableGem();
    }

    private void DisableGem()
    {
        gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
        gameObject.SetActive(false);
    }
}
