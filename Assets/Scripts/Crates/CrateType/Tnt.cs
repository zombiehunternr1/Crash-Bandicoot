using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tnt : MonoBehaviour
{
    public GameEvent CrateDestroyed;
    private MeshRenderer[] GetChildren;
    private List<MeshRenderer> Countdown = new List<MeshRenderer>();
    private Renderer TntRend;

    //Gets all the MeshRenderers from it's children and itself and stores it in it's own variables.
    //Goes over each child object and adds it to the MeshRenderer list and removes the first one at the end because that one is the parent object.
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

    //Once this function gets called it starts the coroutine StartCountdown.
    public void Activate()
    {
        StartCoroutine(StartCountdown());
    }

    //This coroutine disables each meshrenderer after a certain amount of seconds has passed.
    //Once it reaches the last one it disables the gameobject and raises the event.
    private IEnumerator StartCountdown()
    {
        TntRend.enabled = false;
        yield return new WaitForSeconds(1);
        Countdown[0].enabled = false;
        yield return new WaitForSeconds(1);
        Countdown[1].enabled = false;
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
        CrateDestroyed.Raise();
    }
}
