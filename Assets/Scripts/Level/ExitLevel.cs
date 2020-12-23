using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{
    public GemCollected Gems;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerActions>())
        {
            //LevelData.Instance.Data.GemsCollected = Gems.GemsCollected.ToArray();
            SaveSystem.SaveProgress(other.GetComponent<PlayerStatus>().Player, LevelData.Instance.Data);
            //SceneManager.LoadScene("Testscene");
        }
    }
}
