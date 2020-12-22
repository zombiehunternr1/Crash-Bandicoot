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
            SaveSystem.SaveProgress(other.GetComponent<PlayerStatus>().Player, Gems);
            SceneManager.LoadScene("Testscene");
        }
    }
}
