using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Woompa : MonoBehaviour
{
    public GameEvent AddWoompa;

    private BoxCollider Collider;

    private void Awake()
    {
        Collider = GetComponent<BoxCollider>();
        Collider.enabled = false;
        StartCoroutine(EnableTrigger());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerActions>())
        {
            AddWoompa.Raise();
            Destroy(gameObject);
        }
    }

    IEnumerator EnableTrigger()
    {
        yield return new WaitForSeconds(1);
        Collider.enabled = true;
    }
}
