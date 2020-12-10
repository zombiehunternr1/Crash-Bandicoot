using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Woompa : MonoBehaviour
{
    public GameEvent AddWoompa;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerActions>())
        {
            AddWoompa.Raise();
            Destroy(gameObject);
        }
    }
}
