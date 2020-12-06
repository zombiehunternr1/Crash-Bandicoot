using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAfterDeath : MonoBehaviour
{
    public GameEvent CrateReset;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerActions>())
        {
            CrateReset.Raise();
        }
    }
}
