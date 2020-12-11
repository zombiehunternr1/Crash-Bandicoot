using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExclamationMark : MonoBehaviour
{
    //Destroys the gameobject after 1 second.
    private void Awake()
    {
        Destroy(gameObject, 3f);
    }
}
