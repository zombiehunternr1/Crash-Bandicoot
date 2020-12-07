using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public CheckAmount BoxCounter;

    private void Awake()
    {
        BoxCounter = GetComponentInParent<CheckAmount>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerActions>())
        {
            Destroy(BoxCounter.gameObject);
            Destroy(gameObject);
        }
    }
}
