using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxCounter : MonoBehaviour
{
    public Text BoxCount;
    public GameObject Parent;
    private LevelManager LevelManager;

    void Awake()
    {
        LevelManager = GetComponentInParent<LevelManager>();
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
            GemSystem.Instance.SpawnGem(this.transform.position, GemColour.WhiteBox);
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
            BoxCount.text = LevelManager.CurrentCrates + " / " + LevelManager.TotalCrates.Count.ToString();
        }
    }  
}
