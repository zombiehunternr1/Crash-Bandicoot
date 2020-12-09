using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public ParticleSystem ExitEffect;

    private ParticleSystem Effect;

    private void Awake()
    {
        Effect = Instantiate(ExitEffect, transform.position, ExitEffect.transform.rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerActions>())
        {
            Effect.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerActions>())
        {
            Effect.Stop();
        }
    }
}
