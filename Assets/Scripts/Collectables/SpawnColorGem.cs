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
        GemSystem.Instance.SpawnGem(this.transform.position, gemColour);
        for (int i = 0; i < GemsAvailable.Gems.Count; i++)
        {
            if (GemCollection.GemsCollected.Contains(i))
            {
                if (GemsAvailable.Gems[i].GetComponent<Gem>().GemColour.Equals(GemColour.Blue))
                {
                    GemsAvailable.Gems[i].gameObject.SetActive(false);
                    Destroy(gameObject);
                }
                else if (GemsAvailable.Gems[i].GetComponent<Gem>().GemColour.Equals(GemColour.Green))
                {
                    GemsAvailable.Gems[i].gameObject.SetActive(false);
                    Destroy(gameObject);
                }
                else if (GemsAvailable.Gems[i].GetComponent<Gem>().GemColour.Equals(GemColour.Orange))
                {
                    GemsAvailable.Gems[i].gameObject.SetActive(false);
                    Destroy(gameObject);
                }
                else if (GemsAvailable.Gems[i].GetComponent<Gem>().GemColour.Equals(GemColour.Purple))
                {
                    GemsAvailable.Gems[i].gameObject.SetActive(false);
                    Destroy(gameObject);
                }
                else if (GemsAvailable.Gems[i].GetComponent<Gem>().GemColour.Equals(GemColour.Red))
                {
                    GemsAvailable.Gems[i].gameObject.SetActive(false);
                    Destroy(gameObject);
                }
                else if (GemsAvailable.Gems[i].GetComponent<Gem>().GemColour.Equals(GemColour.WhiteHidden))
                {
                    GemsAvailable.Gems[i].gameObject.SetActive(false);
                    Destroy(gameObject);
                }
                else if (GemsAvailable.Gems[i].GetComponent<Gem>().GemColour.Equals(GemColour.Yellow))
                {
                    GemsAvailable.Gems[i].gameObject.SetActive(false);
                    Destroy(gameObject);
                }              
            }
        }
    }
}
