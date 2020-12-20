using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public GemColour GemColour;
    public GameObject[] Gems;
    public int ID;

    private void Start()
    {
        EnableGem(GemColour);
    }

    public void EnableGem ( GemColour gemColour )
    {
        GemColour = gemColour;
        switch (GemColour)
        {
            case GemColour.WhiteBox:
                Gems[0].SetActive(true);
                break;

            case GemColour.Red:
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

            case GemColour.WhiteHidden:
                Gems[7].SetActive(true);
                break;
        }
    }
}

public enum GemColour 
{ 
    WhiteBox,
    Red,
    Green,
    Blue,
    Yellow,
    Orange,
    Purple,
    WhiteHidden
}
