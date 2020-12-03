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

    private int CurrentCrates;
    private List<Breakable> TotalCrates = new List<Breakable>();
    private Breakable[] CratesInLevel;

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
    //If so it means the player has broken all the boxes and the gem can be instanciated.
    //Afterwards it destroys the text element and it's parent.
    public void SpawnGem()
    {
        if(CurrentCrates == TotalCrates.Count)
        {
            Instantiate(Gem, transform.position, transform.rotation);           
            Destroy(BoxCount);
            Destroy(Parent);
        }
    }

    //Once this function gets called it increases the current crate count by one and calls the function UpdateUI.
    public void AddCrate()
    {
        CurrentCrates++;
        UpdateUI();
    }

    //This function updates the UI text that is being displayed on the total count crate.
    private void UpdateUI()
    {
        BoxCount.text = CurrentCrates + " / " + TotalCrates.Count.ToString();
    }

    //Once this function gets called it checks which crates were inactive.
    //foreach crate that is inactive it descreases the current crate count by one and sets the crate back to enabled.
    public void ResetCurrentAmount()
    {
        foreach(Breakable crate in CratesInLevel)
        {
            if (crate.enabled == false)
            {
                CurrentCrates--;
                crate.enabled = true;
            }
        }
    }
}
