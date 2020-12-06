using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventRaiser : MonoBehaviour
{
    [SerializeField] GameEvent _gameEvent;
    
    public void RaiseEvent()
    {
        _gameEvent.Raise();
    }
}
