using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerActions>())
        {
            SaveSystem.SavePlayer(other.GetComponent<PlayerStatus>().Player);
            SceneManager.LoadScene("Testscene");
        }
    }
}
