using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxCounter : MonoBehaviour
{
    public Text BoxCount;
    public GameObject Boxes;

    private List<Breakable> TotalCrates = new List<Breakable>();
    private Renderer Rend;

    //Gets all the breakable crates and stores them in the variable CratesInLevel.
    //Then goes over each crate and adds that crate to the TotalCrates list.
    //Once that is done it displays how many crates the player has currently broken and how many there are in the level.
    void Start()
    {
        var CratesInLevel = Boxes.GetComponentsInChildren<Breakable>();

        foreach(Breakable crate in CratesInLevel)
        {
            TotalCrates.Add(crate);
        }
        BoxCount.text = "0 / " + TotalCrates.Count.ToString();
    }

    //Keeps setting the BoxCount text to the objects position.
    void FixedUpdate()
    {
        Vector3 Pos = Camera.main.WorldToScreenPoint(this.transform.position);
        BoxCount.transform.position = Pos;        
    }
}
