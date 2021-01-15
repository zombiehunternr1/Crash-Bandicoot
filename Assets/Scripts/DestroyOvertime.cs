using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOvertime : MonoBehaviour
{
    public float TimeBeforeDestroy = 1f;
    
    private void Start()
    {
        Destroy(gameObject, TimeBeforeDestroy);
    }
}
