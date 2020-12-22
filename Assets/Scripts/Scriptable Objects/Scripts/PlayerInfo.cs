﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName="Playerinfo", menuName ="ScriptableObjects/Player")]
[System.Serializable]
public class PlayerInfo : ScriptableObject
{
    public int Lives;
    public int ExtraHit;
    public int Woompa;
}
