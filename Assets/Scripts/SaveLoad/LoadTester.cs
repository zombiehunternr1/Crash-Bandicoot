using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Save;

public class LoadTester : MonoBehaviour
{
    public GameEvent UpdateHUD;
    public GemCollected Gems;

    private PlayerInfo PlayerInfo;
    private GemSystem GemSystem;

    private void Start()
    {
        TestingLoad();
        GemSystem = FindObjectOfType<GemSystem>();
        GemSystem.SpawnGems();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerActions>())
        {
            SceneManager.LoadScene("Testscene");
        }
    }

    private void TestingLoad()
    {
        PlayerInfo = FindObjectOfType<PlayerStatus>().Player;

        Save player = SaveSystem.LoadProgress();

        PlayerInfo.Lives = player.Lives;
        PlayerInfo.ExtraHit = player.ExtraHit;
        PlayerInfo.Woompa = player.Woompa;
        Level[] level = player.Levels;
        UpdateHUD.Raise();
        Gems.GemsCollected = new List<int>();
        LevelData.Instance.Levels = level.ToList();
        LevelData.Instance.SetLevel();
        Gems.GemsCollected = LevelData.Instance.Data.GemsCollected.ToList();
    }
}
