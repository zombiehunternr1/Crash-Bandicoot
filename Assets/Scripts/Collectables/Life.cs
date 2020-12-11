using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    public GameEvent AddLife;

    private SphereCollider Collider;

    private void Awake()
    {
        Collider = GetComponent<SphereCollider>();
        Collider.enabled = false;
        StartCoroutine(EnableTrigger());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerActions>())
        {
            AddLife.Raise();
            Destroy(gameObject);
        }
    }

    IEnumerator EnableTrigger()
    {
        yield return new WaitForSeconds(1);
        Collider.enabled = true;
    }
}
