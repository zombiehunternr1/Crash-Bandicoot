using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public GameEventTransform CheckPointReached;
    public GameEvent DestroyedCrate;
    [HideInInspector]
    public bool hasSet = false;

    private Transform CheckPointPosition;

    private void Awake()
    {
        CheckPointPosition = transform;
    }

    public void SetCheckpoint()
    {
        if (!hasSet)
        {
            hasSet = true;
            Debug.Log(CheckPointPosition);
            CheckPointReached.RaiseTransform(transform);
            if (GetComponent<Breakable>())
            {
                DestroyedCrate.Raise();
                gameObject.SetActive(false);
            }
            else if (GetComponent<Interactable>())
            {
                gameObject.SetActive(false);
            }
        }
    }
}
