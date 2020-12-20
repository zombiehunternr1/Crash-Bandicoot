﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemBase : MonoBehaviour
{
    public ParticleSystem Effect;
    public LevelManager LevelManager;

    private Colored ColorGem;

    private void Awake()
    {
        ColorGem = GetComponent<Colored>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerActions>())
        {
            Effect.Play();
            if (!ColorGem)
            {
                if (LevelManager != null)
                {
                    for (int i = 0; i < LevelManager.BoxCounters.Count; i++)
                    {
                        Destroy(LevelManager.BoxCounters[i].GetComponentInParent<CheckAmount>().gameObject);
                    }
                }
            }           
            gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
            Destroy(gameObject, 0.5f);
        }
    }
}
