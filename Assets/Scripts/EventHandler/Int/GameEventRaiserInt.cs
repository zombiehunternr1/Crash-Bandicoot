using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventRaiserInt : MonoBehaviour
{
    [SerializeField] GameEventInt _gameEventInt;

    public void RaiseIntEvent(int value)
    {
        _gameEventInt.RaiseInt(value);
    }
}
