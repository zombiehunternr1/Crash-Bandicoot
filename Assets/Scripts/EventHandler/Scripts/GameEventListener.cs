using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [SerializeField] private GameEvent _gameEvent;
    [SerializeField] private UnityEvent _response;

    void OnEnable()
    {
        _gameEvent.RegisterListener(this);
    }

    void OnDisable()
    {
        _gameEvent.UnregisterListener(this);
    }

    public void OnEventRaised()
    {
        _response.Invoke();
    }
}
