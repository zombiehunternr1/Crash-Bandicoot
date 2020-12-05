using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public GameEvent CheckPointReached;
    public GameEvent DestroyedCrate;

    public void SetCheckpoint()
    {
        DestroyedCrate.Raise();
        CheckPointReached.Raise();
        gameObject.SetActive(false);
    }
}
