using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class TransformEvent : UnityEvent<Transform> { }

public class GameEventListenerTransform : MonoBehaviour
{
    [SerializeField] private GameEventTransform _gameEventTransform;
    [SerializeField] private TransformEvent _responseTransform;

 /*   void Start()
    {
        if (_responseTransform == null)
        {
            _responseTransform = new TransformEvent();
        }
    }
    */

    void OnEnable()
    {
        _gameEventTransform.RegisterTransfromEvent(this);
    }

    void OnDisable()
    {
        _gameEventTransform.UnregisterTransformListener(this);
    }

    public void OnEventTransformRaised(Transform value)
    {
        _responseTransform.Invoke(value);
    }
}
