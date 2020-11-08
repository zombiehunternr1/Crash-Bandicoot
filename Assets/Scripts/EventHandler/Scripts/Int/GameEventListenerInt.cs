using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class IntEvent : UnityEvent<int> { }

public class GameEventListenerInt : MonoBehaviour
{
    [SerializeField] private GameEventInt _gameEventInt;
    [SerializeField] private IntEvent _responseInt;

    void Start()
    {
        if(_responseInt == null)
        {
            _responseInt = new IntEvent();
        }
    }

    void OnEnable()
    {
        _gameEventInt.RegisterIntEvent(this);
    }

    void OnDisable()
    {
        _gameEventInt.UnregisterIntListener(this);
    }

    public void OnEventIntRaise(int value)
    {
        _responseInt.Invoke(value);
    }
}