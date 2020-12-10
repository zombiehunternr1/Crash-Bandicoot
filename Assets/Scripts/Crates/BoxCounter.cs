using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxCounter : MonoBehaviour
{
    public Text BoxCount;
    public GameObject Boxes;
    public GameObject Gem;
    public GameObject Parent;
    public GameObject NitroDetonator;
    public GameEvent ResetPlayerPosition;
    public Interactable Interact;
    public PlayerActions Player;
    public RectTransform FadePanel;
    private int FadingSpeed = 1;

    private bool Fading = true;
    private Breakable BreakableCrate;
    private BreakAmount BreakAmountCrate;
    private Nitro NitroCrate;
    private Tnt TntCrate;
    private CheckPoint CheckpointCrate;
    private Activator ActivatorCrate;
    private int CurrentCrates;
    private List<Breakable> TotalCrates = new List<Breakable>();
    private Breakable[] CratesInLevel;
    private GameObject LevelGem;

    //Gets all the breakable crates and stores them in the variable CratesInLevel.
    //Then goes over each crate and adds that crate to the TotalCrates list.
    //Once that is done it displays how many crates the player has currently broken and how many there are in the level.
    void Start()
    {
        CratesInLevel = Boxes.GetComponentsInChildren<Breakable>();

        foreach(Breakable crate in CratesInLevel)
        {
            TotalCrates.Add(crate);
        }
        BoxCount.text = CurrentCrates + " / " +  TotalCrates.Count.ToString();
    }

    //Keeps setting the BoxCount text to the objects position.
    void FixedUpdate()
    {
        Vector3 Pos = Camera.main.WorldToScreenPoint(this.transform.position);
        BoxCount.transform.position = Pos;        
    }

    //Once this function gets called it checks if the CurrentCrates total is equal to the TotalCrates amount.
    //If so it means the player has broken all the boxes and the gem can be instanciated and stored in the GameObject LevelGem.
    //Afterwards it disables the boxcount text, disables the boxcollider on the parent object and disables it's meshrenderer.
    public void SpawnGem()
    {
        if(CurrentCrates == TotalCrates.Count)
        {
            LevelGem = Instantiate(Gem, transform.position, transform.rotation, this.transform);
            BoxCount.GetComponent<Text>().enabled = false;
            Parent.GetComponent<BoxCollider>().enabled = false;
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;

        }
    }

    //Once this function gets called it increases the current crate count by one and calls the function UpdateUI.
    public void AddCrate()
    {
        CurrentCrates++;
        UpdateSpawnGemUI();
    }

    //This function updates the UI text that is being displayed on the total count crate.
    private void UpdateSpawnGemUI()
    {
        BoxCount.text = CurrentCrates + " / " + TotalCrates.Count.ToString();
    }

    //Once this function gets called it sets the CanMove bool to false, starts the coroutine FadeToBack and checks if the totalcrates list has crates in it.
    //If it doesn't it skills the whole reset progress. If it does it checks if the crate is disabled.
    //foreach crate that is inactive it descreases the current crate count by one and sets the crate back to enabled.
    //Afterwards we call the function UpdateUI, checks if boxcount and parent are not empty. If not it sets the boxcount active and enables the parent's boxcollider.
    public void ResetCurrentAmount()
    {
        Player.CanMove = false;
        StartCoroutine(FadeToBlack());
        foreach (Breakable crate in TotalCrates)
        {
            if(crate != null)
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
        UpdateSpawnGemUI();
        if(Interact != null)
        {
            if (Interact.GetComponent<Activator>())
            {
                ActivatorCrate = Interact.GetComponent<Activator>();
                ActivatorCrate.DeactivateCrates();
            }
        }
        if(NitroDetonator != null)
        {
            NitroDetonator.GetComponent<NitroDetonator>().ResetDetonator();
        }
        if (BoxCount != null)
        {
            BoxCount.GetComponent<Text>().enabled = true;
        }
        if(Parent != null)
        {
           Parent.GetComponent<BoxCollider>().enabled = true;                       
        }
        this.gameObject.GetComponent<MeshRenderer>().enabled = true;
        Destroy(LevelGem);            
    }

    //If this function gets called it means the player has reached a checkpoint meaning all the crates he broke before in this level are now permanently added to the CurrentCrates.
    public void CheckPointReached()
    {
        foreach (Breakable crate in TotalCrates)
        {
            if(crate != null)
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
            //Checks if the alpha color of the image is greater or equal to 1. If so it means the screen is completely black and we can call the coroutine FadeToOpaque
            //and raises the event ResetPlayerPosition.
            if (FadePanel.GetComponent<Image>().color.a >= 1)
            {
                ResetPlayerPosition.Raise();
                StartCoroutine(FadeToOpaque());            
            }
        }
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
            Player.CanMove = true;
        }
    }
}
