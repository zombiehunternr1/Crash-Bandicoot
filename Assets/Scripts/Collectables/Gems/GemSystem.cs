using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemSystem : MonoBehaviour
{
    public List<int> CollectedIds = new List<int>();
    public SpawnColorGem[] GemHolders;
    public BoxCounter[] BoxCounters;
    public GameObject gemPrefab;
    public GemCollected GemCollectionSO;

    private int LastUsedIndex;

    public void SpawnGems()
    {
        CollectedIds = GemCollectionSO.GemsCollected;

        GemHolders = GameObject.FindObjectsOfType<SpawnColorGem>();
        for (int i = 0; i < GemHolders.Length; i++)
        {
            GemHolders[i].ID = i;
            LastUsedIndex = i;
        }
        LastUsedIndex++;

        BoxCounters = GameObject.FindObjectsOfType<BoxCounter>();
        for (int i = 0; i < BoxCounters.Length; i++)
        {
            BoxCounters[i].ID = LastUsedIndex;
        }
        LastUsedIndex += 2;

        foreach (var x in GemHolders)
        {
            if (!CollectedIds.Contains(x.ID))
            {
                var obj = Instantiate(gemPrefab, x.transform.position, Quaternion.identity);
                obj.GetComponent<Gem>().Enable(x.Gem.Colour);
                obj.GetComponent<Gem>().ID = x.ID;
                obj.SetActive(true);
            }
        }

        foreach (var x in BoxCounters)
        {
            x.transform.parent.gameObject.SetActive(false);
            if (!CollectedIds.Contains(x.ID))
            {
                x.transform.parent.gameObject.SetActive(true);
            }
        }
    }
}
