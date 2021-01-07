using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GemCollected GemCollection;
    public GemSystem GemsAvailable;
    public GameObject LevelCrates;
    public PlayerActions Player;
    public PlayerInfo PlayerInfo;
    public RectTransform FadePanel;
    public GameEvent ResetPlayerPosition;
    public Text GameOverText;
    public Text BoxCountUI;
    public Text WoompaUI;
    public Text LivesUI;
    [HideInInspector]
    public int CurrentCrates;

    private Breakable BreakableCrate;
    private BreakAmount BreakAmountCrate;
    private Nitro NitroCrate;
    private Tnt TntCrate;
    private CheckPoint CheckpointCrate;
    [HideInInspector]
    public List<Breakable> TotalCrates = new List<Breakable>();
    [HideInInspector]
    public List<BoxCounter> BoxCounters = new List<BoxCounter>();
    [HideInInspector]
    public List<AkuAku> AkuAkuCrateSpawns = new List<AkuAku>();

    private List<Activator> ActivatorCrates = new List<Activator>();
    private List<NitroDetonator> NitroDetanorCrates = new List<NitroDetonator>();   
    private int FadingSpeed = 1;
    private bool Fading = true;
    private SpawnColorGem SpawnColorGem;
    private BoxCounter[] BoxCountersInLevel;
    private Breakable[] CratesInLevel;
    private Activator[] ActivatorsInLevel;
    private NitroDetonator[] NitroDetonatorsInLevel;

    //Goes over all the gameobjects in LevelCrates to find the object that has the script SpawnColorGem attached to it. If so it adds it to the variable SpawnColorGem.
    //Gets all the crates with crate type Breakable, Activator and NitroDetonator and stores them in each individual array.
    //Then goes over each item in their individual array and adds them to their corresponding list.
    //At the end the Boxcounter will be displayed with the current amount of broken crates and the total breakable crates in the level.
    void Start()
    {
        SpawnColorGem = LevelCrates.GetComponentInChildren<SpawnColorGem>();
        CratesInLevel = LevelCrates.GetComponentsInChildren<Breakable>();
        ActivatorsInLevel = LevelCrates.GetComponentsInChildren<Activator>();
        NitroDetonatorsInLevel = LevelCrates.GetComponentsInChildren<NitroDetonator>();
        BoxCountersInLevel = LevelCrates.GetComponentsInChildren<BoxCounter>();

        foreach (Activator ActivatorCrate in ActivatorsInLevel)
        {
            ActivatorCrates.Add(ActivatorCrate);
        }

        foreach(NitroDetonator NitroDetonatorCrate in NitroDetonatorsInLevel)
        {
            NitroDetanorCrates.Add(NitroDetonatorCrate);
        }

        foreach(BoxCounter BoxCounter in BoxCountersInLevel)
        {
            BoxCounters.Add(BoxCounter);
        }

        foreach (Breakable Crate in CratesInLevel)
        {
            TotalCrates.Add(Crate);
        }

        UpdateCrateCount();
        WoompaUI.text = PlayerInfo.Woompa.ToString();
        LivesUI.text = PlayerInfo.Lives.ToString();
    }

    //Once this function gets called it sets the CanMove bool to false, starts the coroutine FadeToBack
    public void PlayerDied()
    {
        Player.CanMove = false;
        StartCoroutine(FadeToBlack());
    }

    //Once this function gets called it increases the current crate count by one and calls the function UpdateUI and UpdateHUD.
    public void AddCrate()
    {
        CurrentCrates++;
        for (int i = 0; i < BoxCounters.Count; i++)
        {
            BoxCounters[i].UpdateSpawnGemUI();
        }
        UpdateHUD();
    }

    //This updates the Crate count display in the crate checker.
    private void UpdateCrateCount()
    {
        for (int i = 0; i < BoxCounters.Count; i++)
        {
            if (BoxCounters[i] != null)
            {
                BoxCounters[i].BoxCount.text = CurrentCrates + "/" + TotalCrates.Count.ToString();
                BoxCountUI.text = CurrentCrates + "/" + TotalCrates.Count.ToString();
            }
        }
    }

    //This function updates the boxcount HUD display.
    public void UpdateHUD()
    {
        if(BoxCountUI != null)
        {
            BoxCountUI.text = CurrentCrates + "/" + TotalCrates.Count.ToString();
        }
        if(WoompaUI != null)
        {
            WoompaUI.text = PlayerInfo.Woompa.ToString();
        }
        if(LivesUI != null)
        {
            LivesUI.text = PlayerInfo.Lives.ToString();
        }        
    }

    //This coroutine switches the panel from opaque to black.
    IEnumerator FadeToBlack()
    {
        //Gets the color component from the panel and stores it in the FadingColor variable.
        Color FadingColor = FadePanel.GetComponent<Image>().color;
        float FadeAmount;
        if (Fading)
        {
            //Keeps looping until the alpha color of the image isn't smaller then 1.
            while (FadePanel.GetComponent<Image>().color.a < 1)
            {
                FadeAmount = FadingColor.a + (FadingSpeed * Time.deltaTime);
                FadingColor = new Color(FadingColor.r, FadingColor.g, FadingColor.g, FadeAmount);
                FadePanel.GetComponent<Image>().color = FadingColor;
                yield return null;
            }
            yield return new WaitForSeconds(FadingSpeed);
            Fading = false;           
            StartCoroutine(FadeToBlack());
        }
        else
        {
            //Checks if the alpha color of the image is greater or equal to 1. If so it means the screen is completely black.
            //Checks if the players lives isn't lower then 0, if not then it takes one life of the player and checks the BoxCounter isn't a null return so we can reset the crates in the level, 
            //then raises the ResetPlayerPosition event and starts the coroutine FadeToOpaque.
            //If the amount of lives is lower then 0 it means the player lost completely and the gameover scene should be shown.
            if (FadePanel.GetComponent<Image>().color.a >= 1)
            {
                if(PlayerInfo.Lives > 0)
                {
                    PlayerInfo.Lives--;                   
                    ResetLevel();
                    ResetPlayerPosition.Raise();
                    StartCoroutine(FadeToOpaque());                                    
                }
                else
                {
                    StartCoroutine(GameOver());
                }               
            }
        }
    }

    //Once this function gets called it checks if the totalcrates list has crates in it.
    //If it doesn't it skills the whole reset progress. If it does it checks if the crate is disabled.
    //foreach crate that is inactive it descreases the current crate count by one and sets the crate back to enabled.
    //Afterwards we call the function UpdateUI, checks if boxcount and parent are not empty. If not it sets the boxcount active and enables the parent's boxcollider.
    //At last ut calls the functions UpdateUIHUD and UpdateCrateHUD to update the hud display and crate count display.
    public void ResetLevel()
    {
        foreach (Breakable crate in TotalCrates)
        {
            if (crate != null)
            {
                if (!crate.isActiveAndEnabled)
                {
                    if (!crate.GetComponent<CheckPoint>())
                    {
                        if (crate.GetComponent<Breakable>())
                        {
                            BreakableCrate = crate.GetComponent<Breakable>();
                            BreakableCrate.HasBounced = false;
                            BreakableCrate.JumpAmount = 0;
                        }
                        if (crate.GetComponent<BreakAmount>())
                        {
                            BreakAmountCrate = crate.GetComponent<BreakAmount>();
                            BreakAmountCrate.Activated = false;
                        }
                        if (crate.GetComponent<Nitro>())
                        {
                            NitroCrate = crate.GetComponent<Nitro>();
                            NitroCrate.HasExploded = false;
                        }
                        if (crate.GetComponent<Tnt>())
                        {
                            TntCrate = crate.GetComponent<Tnt>();
                            TntCrate.ResetCountdown();
                        }
                        CurrentCrates--;
                        crate.gameObject.SetActive(true);
                    }
                }
                else
                {
                    if (crate.GetComponent<CheckPoint>())
                    {
                        CheckpointCrate = crate.GetComponent<CheckPoint>();
                        CheckpointCrate.hasSet = false;
                    }
                }
            }
        }
        if (Player.GetComponentInChildren<AkuAku>())
        {
            var AkuAku = Player.GetComponentInChildren<AkuAku>();

            //StopCoroutine(Player.GetComponentInChildren<AkuAku>().InvinsibilityTimer(TimeRemaining));
            AkuAku.NotInvinsible = true;
            AkuAku.KillPlayer.CanHit = true;
            Destroy(AkuAku.gameObject);
        }

        for(int i = 0; i < AkuAkuCrateSpawns.Count; i++)
        {
            Destroy(AkuAkuCrateSpawns[i].gameObject);
            AkuAkuCrateSpawns.RemoveAt(i);
        }

        for (int i = 0; i < ActivatorCrates.Count; i++)
        {
            ActivatorCrates[i].DeactivateCrates();
        }

        for (int i = 0; i < NitroDetanorCrates.Count; i++)
        {
            NitroDetanorCrates[i].ResetDetonator();
        }
        UpdateCrateCount();
        UpdateHUD();
    }

    //If this function gets called it means the player has reached a checkpoint meaning all the crates he broke before in this level are now permanently added to the CurrentCrates.
    public void CheckPointReached()
    {
        foreach (Breakable crate in TotalCrates)
        {
            if (crate != null)
            {
                if (!crate.isActiveAndEnabled)
                {
                    if (!crate.GetComponent<CheckPoint>())
                    {
                        Destroy(crate.gameObject);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Once overworld scene is added in instead of reloading the scene load in the overworld scene.
    /// </summary>


    //This coroutine displays the game over text to the player, resets the players lives back to 5 and the level gets reloaded.
    IEnumerator GameOver()
    {
        GameOverText.enabled = true;
        yield return new WaitForSeconds(5);
        PlayerInfo.Lives = 5;
        SceneManager.LoadScene(0);
    }

    //This coroutine switches the panel from black to opaque.
    IEnumerator FadeToOpaque()
    {
        //Gets the color component from the panel and stores it in the FadingColor variable.
        Color FadingColor = FadePanel.GetComponent<Image>().color;
        float FadeAmount;
        if (!Fading)
        {
            //Keeps looping until the alpha color of the image isn't smaller then 0.
            while (FadePanel.GetComponent<Image>().color.a > 0)
            {
                FadeAmount = FadingColor.a - (FadingSpeed * Time.deltaTime);
                FadingColor = new Color(FadingColor.r, FadingColor.g, FadingColor.g, FadeAmount);
                FadePanel.GetComponent<Image>().color = FadingColor;
                yield return null;
            }
            yield return new WaitForSeconds(FadingSpeed);
            Fading = true;
            StartCoroutine(FadeToOpaque());
        }
        else
        {
            Player.TimerLife = 5f;
            Player.CanMove = true;
        }
    }
}