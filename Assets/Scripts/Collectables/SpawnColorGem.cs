using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnColorGem : MonoBehaviour
{
    public int ID;
    public GemProperty Gem;
}

[System.Serializable]
public class GemProperty
{
    public GemColour Colour;
}

public enum GemColour { NONE, Red, White, Green, Blue, Yellow, Orange, Purple }

