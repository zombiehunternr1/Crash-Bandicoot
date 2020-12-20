using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnColorGem : MonoBehaviour
{
    public GemColour gemColour;

    void Start()
    {
        GemSystem.Instance.SpawnGem(this.transform.position, gemColour);
    }
}
