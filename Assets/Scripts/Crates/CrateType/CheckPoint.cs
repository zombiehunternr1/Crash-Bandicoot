using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public GameEventTransform CheckPointReached;
    public GameEvent DestroyedCrate;
    [HideInInspector]
    public bool hasSet = false;

    public GameObject MetalCrate;
    public GameObject BrokenCheckpoint;

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
                Instantiate(BrokenCheckpoint, transform.position + (Vector3.up * .1f), transform.rotation);
                gameObject.SetActive(false);
            }
            else if (GetComponent<Interactable>())
            {
                Instantiate(MetalCrate, transform.position, transform.rotation);
                gameObject.SetActive(false);
            }
        }
    }
}
