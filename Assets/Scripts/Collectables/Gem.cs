using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class Gem : MonoBehaviour
{
    public ParticleSystem Effect;
    public LevelManager LevelManager;
    public Gems GemsCollected;
    public Gems GemsAvailable;

    private string GemIDTempString;
    private string GemID;

    private void Awake()
    {           
        Instantiate(Effect, transform.position, transform.rotation);
        LevelManager = GetComponentInParent<LevelManager>();

        if(GemID == null)
        {
            GemIDTempString = System.Guid.NewGuid().ToString();
            GemIDTempString = Regex.Replace(GemIDTempString, "[-abcdefghijklmnopqrstuvwxyz]", "");
            GemID = GemIDTempString;
            GemsAvailable.GemList.Add(GemIDTempString);
            Debug.Log(GemID);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerActions>())
        {
            GemsCollected.GemList.Add(GemID);
            Instantiate(Effect, transform.position, transform.rotation);
            if(LevelManager != null)
            {
                for (int i = 0; i < LevelManager.BoxCounters.Count; i++)
                {
                    Destroy(LevelManager.BoxCounters[i].GetComponentInParent<CheckAmount>().gameObject);
                }
            }
            Destroy(gameObject);
        }
    }
}
