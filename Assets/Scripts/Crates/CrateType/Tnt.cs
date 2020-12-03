using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tnt : MonoBehaviour
{
    private MeshRenderer[] GetChildren;
    private List<MeshRenderer> Countdown = new List<MeshRenderer>();
    private Renderer TntRend;

    void Awake()
    {
        TntRend = GetComponent<Renderer>();
        GetChildren = GetComponentsInChildren<MeshRenderer>();

        foreach(MeshRenderer Mat in GetChildren)
        {
            Countdown.Add(Mat);
        }
        Countdown.RemoveAt(0);
    }

    public void Activate()
    {
        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        TntRend.enabled = false;
        yield return new WaitForSeconds(1);
        Countdown[0].enabled = false;
        yield return new WaitForSeconds(1);
        Countdown[1].enabled = false;
        yield return new WaitForSeconds(1);
        Debug.Log("Explode!");
    }
}
