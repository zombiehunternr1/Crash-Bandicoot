using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GemsCollected", menuName = "ScriptableObjects/Gems")]
[System.Serializable]
public class GemCollected : ScriptableObject
{
    public List<int> GemsCollected;
}
