using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="GameEvent", menuName = "ScriptableObjects/GameEvents/GameEventTransform")]
public class GameEventTransform : ScriptableObject
{
    private List<GameEventListenerTransform> _Transformlisteners = new List<GameEventListenerTransform>();

    public void RaiseTransform(Transform value)
    {
        for(int i = _Transformlisteners.Count - 1; i >= 0; i--)
        {
            _Transformlisteners[i].OnEventTransformRaised(value);
        }
    }

    public void RegisterTransfromEvent(GameEventListenerTransform newTransformListener)
    {
        _Transformlisteners.Add(newTransformListener);
    }

    public void UnregisterTransformListener(GameEventListenerTransform listenerTransformToRemove)
    {
        _Transformlisteners.Remove(listenerTransformToRemove);
    }
}
