using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Save;

public class LevelData : MonoBehaviour
{
    public GemCollected Gems;
    public static LevelData Instance;
    void Awake() { Instance = this; }


    public Level Data;

    public List<Level> Levels;

    public void AddLevel()
    {
        if (!Levels.Contains(Data))
        {
            Data.GemsCollected = Gems.GemsCollected.ToArray();
            Levels.Add(Data);
        }
        else
        {
            Data.GemsCollected = Gems.GemsCollected.ToArray();
        }      
    }

    public void SetLevel()
    {
        foreach(Level level in Levels)
        {
            if(level.LevelID == Data.LevelID)
            {
                Data = level;
            }
        }
    }
}
