using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventRaiserTransform : MonoBehaviour
{
    [SerializeField] GameEventTransform _gameEventTransform;
    
    public void RaiseTransformEvent(Transform value)
    {
        _gameEventTransform.RaiseTransform(value);
    }
}
