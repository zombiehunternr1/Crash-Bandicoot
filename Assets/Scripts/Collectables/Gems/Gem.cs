using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public GameObject[] Gems;
    public int ID;

    public void Enable(GemColour colour)
    {
        switch (colour)
        {
            case GemColour.Red:
                Gems[0].SetActive(true);
                break;
            case GemColour.White:
                Gems[1].SetActive(true);
                break;
            case GemColour.Green:
                Gems[2].SetActive(true);
                break;
            case GemColour.Blue:
                Gems[3].SetActive(true);
                break;
            case GemColour.Yellow:
                Gems[4].SetActive(true);
                break;
            case GemColour.Orange:
                Gems[5].SetActive(true);
                break;
            case GemColour.Purple:
                Gems[6].SetActive(true);
                break;
        }
    }
}