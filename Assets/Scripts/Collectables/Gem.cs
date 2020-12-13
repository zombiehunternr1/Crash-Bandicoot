using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public ParticleSystem Effect;
    public LevelManager LevelManager;

    private void Awake()
    {
        Instantiate(Effect, transform.position, transform.rotation);
        LevelManager = GetComponentInParent<LevelManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerActions>())
        {
            Instantiate(Effect, transform.position, transform.rotation);
            if(LevelManager != null)
            {
                for (int i = 0; i < LevelManager.BoxCounters.Count; i++)
                {
                    Destroy(LevelManager.BoxCounters[i].GetComponentInParent<CheckAmount>().gameObject);
                }
            }
            Destroy(gameObject);
        }
    }
}
