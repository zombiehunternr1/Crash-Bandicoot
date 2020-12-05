using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public GameEvent CheckPointReached;
    public GameEvent DestroyedCrate;
    [HideInInspector]
    public bool hasSet = false;

    public void SetCheckpoint()
    {
        if (!hasSet)
        {
            hasSet = true;
            DestroyedCrate.Raise();
            CheckPointReached.Raise();
            gameObject.SetActive(false);
        }
    }
}
