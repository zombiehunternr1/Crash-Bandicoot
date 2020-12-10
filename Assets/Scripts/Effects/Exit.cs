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
        Effect.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
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
