using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxCounter : MonoBehaviour
{
    [HideInInspector]
    public Text BoxCount;
    public int ID;
    private CheckAmount Parent;
    private LevelManager LevelManager;
    private AudioSource BreakCrate;

    void Awake()
    {
        BreakCrate = GetComponent<AudioSource>();
        LevelManager = GetComponentInParent<LevelManager>();
        Parent = GetComponentInParent<CheckAmount>();
        BoxCount = GetComponentInChildren<Text>();
    }

    //Keeps setting the BoxCount text to the objects position.
    void FixedUpdate()
    {
        Vector3 Pos = Camera.main.WorldToScreenPoint(this.transform.position);
        BoxCount.transform.position = Pos;        
    }
   
    //Once this function gets called it checks if the CurrentCrates total is equal to the TotalCrates amount.
    //If so it means the player has broken all the boxes and the gem can be placed at the crates location.
    //Afterwards it disables the boxcount text, disables the boxcollider on the parent object and disables it's meshrenderer.
    public void SpawnGem()
    {
        if (LevelManager.CurrentCrates == LevelManager.TotalCrates.Count)
        {
            BreakCrate.Play();
            var x = Instantiate(FindObjectOfType<GemSystem>().gemPrefab, transform.position, Quaternion.identity).GetComponent<Gem>();
            x.ID = ID;
            x.Enable(GemColour.White);
            x.gameObject.SetActive(true);
            BoxCount.GetComponent<Text>().enabled = false;
            Parent.GetComponent<BoxCollider>().enabled = false;
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }         
    }

    //This function first checks if the BoxCount isn't empty. If not then it updates the UI text that is being displayed on the total count crate.
    public void UpdateSpawnGemUI()
    {
        if(BoxCount != null)
        {
            BoxCount.text = LevelManager.CurrentCrates + "/" + LevelManager.TotalCrates.Count.ToString();
        }
    }  
}
