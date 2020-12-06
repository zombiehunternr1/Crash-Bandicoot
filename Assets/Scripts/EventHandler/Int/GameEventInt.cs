using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent", menuName = "ScriptableObjects/GameEvents/GameEventInt")]
public class GameEventInt : ScriptableObject
{
    private List<GameEventListenerInt> _Intlisteners = new List<GameEventListenerInt>();

    public void RaiseInt(int value)
    {
        for (int i = _Intlisteners.Count - 1; i >= 0; i--)
        {
            _Intlisteners[i].OnEventIntRaise(value);
        }
    }

    public void RegisterIntEvent(GameEventListenerInt newIntListener)
    {
        _Intlisteners.Add(newIntListener);
    }

    public void UnregisterIntListener(GameEventListenerInt ListenerIntRemove)
    {
        _Intlisteners.Remove(ListenerIntRemove);
    }
}
