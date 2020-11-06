using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="GameEvent", menuName ="ScriptableObjects/GameEvents/GameEvent")]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> _listeners = new List<GameEventListener>();

    public void Raise()
    {
        for(int i = _listeners.Count - 1; i >= 0; i--)
        {
            _listeners[i].OnEventRaised();
        }
    }

    public void RegisterListener(GameEventListener newListener)
    {
        _listeners.Add(newListener);
    }

    public void UnregisterListener(GameEventListener listenerToRemove)
    {
        _listeners.Remove(listenerToRemove);
    }
}
