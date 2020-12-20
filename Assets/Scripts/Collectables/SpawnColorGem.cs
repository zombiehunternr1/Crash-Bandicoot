using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnColorGem : MonoBehaviour
{
    public GemCollected GemCollection;
    public GemSystem GemsAvailable;
    public GemColour gemColour;

    void Start()
    {
        for (int i = 0; i < GemsAvailable.Gems.Count; i++)
        {
            if (GemCollection.GemsCollected.Contains(i))
            {
                if (GemsAvailable.Gems[i].GetComponent<Gem>().GemColour.Equals(GemColour.Blue))
                {
                    Destroy(gameObject);
                }
                else if (GemsAvailable.Gems[i].GetComponent<Gem>().GemColour.Equals(GemColour.Green))
                {
                    Destroy(gameObject);
                }
                else if (GemsAvailable.Gems[i].GetComponent<Gem>().GemColour.Equals(GemColour.Orange))
                {
                    Destroy(gameObject);
                }
                else if (GemsAvailable.Gems[i].GetComponent<Gem>().GemColour.Equals(GemColour.Purple))
                {
                    Destroy(gameObject);
                }
                else if (GemsAvailable.Gems[i].GetComponent<Gem>().GemColour.Equals(GemColour.Red))
                {
                    Destroy(gameObject);
                }
                else if (GemsAvailable.Gems[i].GetComponent<Gem>().GemColour.Equals(GemColour.WhiteHidden))
                {
                    Destroy(gameObject);
                }
                else if (GemsAvailable.Gems[i].GetComponent<Gem>().GemColour.Equals(GemColour.Yellow))
                {
                    Destroy(gameObject);
                }   
            }
        }
        GemSystem.Instance.SpawnGem(this.transform.position, gemColour);
    }
}
