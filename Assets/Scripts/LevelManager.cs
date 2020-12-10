using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public PlayerActions Player;
    public BoxCounter BoxCounter;
    public RectTransform FadePanel;
    public GameEvent ResetPlayerPosition;

    private int FadingSpeed = 1;
    private bool Fading = true;

    //Once this function gets called it sets the CanMove bool to false, starts the coroutine FadeToBack
    public void PlayerDied()
    {
        Player.CanMove = false;
        StartCoroutine(FadeToBlack());
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
            //Checks if the BoxCounter isn't a null return so we can reset the crates in the level, then raises the ResetPlayerPosition event and starts the coroutine FadeToOpaque.
            if (FadePanel.GetComponent<Image>().color.a >= 1)
            {
                if(BoxCounter != null)
                {
                    BoxCounter.ResetLevel();
                }               
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