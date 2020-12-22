using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemSystem : MonoBehaviour
{
    /// <summary>
    /// This created  astatic instance of this script.
    /// This allows us to access it from anywhere and any script.
    /// To use this you simple call ' GemSystem.Instance.SpawnGem(position, colour); '
    /// </summary>
    public static GemSystem Instance;

    [Header("Gem Objects.")]
    public List<GameObject> Gems;
    public GameObject gemHolder;
    public int MaxGemsToPool = 30;

    void Awake()
    {
        Instance = this;
        //This will spawn all the needed gems for the level / game.
        for (int i = 0; i < MaxGemsToPool; i++)
        {
            var newGem = Instantiate(gemHolder, Vector3.zero, Quaternion.identity);
            newGem.transform.SetParent(transform);
            newGem.GetComponent<Gem>().ID = i;
            newGem.SetActive(false);
            Gems.Add(newGem);
        }
    }

    /// <summary>
    /// Returns an inactive gem from the pooled list.
    /// </summary>
    public GameObject GetGem
    {
        get
        {
            foreach (GameObject go in Gems)
            {
                if (!go.activeSelf)
                    return go;
            }
            return null;
        }
    }

    /// <summary>
    /// Called to spawn a gem.
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public bool SpawnGem(Vector3 position, GemColour gemColour)
    {
        GameObject x = GetGem;
        if (x != null)
        {
            //Enable the gem colour we want.
            x.GetComponent<Gem>().EnableGem(gemColour);

            //Move the particle to the hit position.
            x.transform.position = position;

            //Activate the Gem holder object.
            x.SetActive(true);
            return true;
        }
        else
        {
            Debug.LogError("Unable to spawn gem on position " + position.ToString());
            return false;
        }
    }
}
