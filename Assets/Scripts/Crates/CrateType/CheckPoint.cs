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

    private Transform GetcurrentCratePosition;

    //Stores the Transform position of the checkpoint in the transform CheckPointPosition.
    private void Awake()
    {
        GetcurrentCratePosition = transform;
    }

    //When this function gets called it checks if the checpoint hasn't already been set yet. If not it sets so the game knows the player has already hit this checkpoint.
    //It then checks if the player either hit a breakable or interactable checkpoint. If it hits either of them it raises the transform event with the checkpointposition and stores it in the players
    //current checkpointreached transform. Afterward it deactivates itself. The only difference between the breakable and interable crate is that a breakable needs to count towards to currently broken box count
    //and the interactable does not. After breaking the checkpoint crate either a metal crate for interactable or a broken checkpoint crate for breakable gets instanciated on the checkpoints position.
    public void SetCheckpoint()
    {
        if (!hasSet)
        {
            hasSet = true;
            if (GetComponent<Breakable>())
            {
                DestroyedCrate.Raise();               
                Instantiate(BrokenCheckpoint, transform.position, transform.rotation);
                CheckPointReached.RaiseTransform(GetcurrentCratePosition);
                gameObject.SetActive(false);
            }
            else if (GetComponent<Interactable>())
            {               
                Instantiate(MetalCrate, transform.position, transform.rotation);
                GetcurrentCratePosition.position += Vector3.up * 1.5f;
                CheckPointReached.RaiseTransform(GetcurrentCratePosition);
                gameObject.SetActive(false);
            }
        }
    }
}
