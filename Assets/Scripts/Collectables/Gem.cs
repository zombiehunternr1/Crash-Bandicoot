using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public CheckAmount BoxCounter;
    public ParticleSystem Effect;

    private void Awake()
    {
        BoxCounter = GetComponentInParent<CheckAmount>();
        Instantiate(Effect, transform.position, transform.rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerActions>())
        {
            Instantiate(Effect, transform.position, transform.rotation);
            if (BoxCounter != null)
            {
                Destroy(BoxCounter.gameObject);
            }            
            Destroy(gameObject);
        }
    }
}
