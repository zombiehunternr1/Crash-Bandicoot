using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemEffect : MonoBehaviour
{
    private void Awake()
    {
        Destroy(gameObject, 0.5f);
    }
}
