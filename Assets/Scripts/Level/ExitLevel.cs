using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitLevel : MonoBehaviour
{
    public GemCollected Gems;
    public RectTransform FadePanel;

    private int FadingSpeed = 1;
    private bool Fading = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerActions>())
        {
            other.GetComponent<PlayerActions>().CanMove = false;
            SaveSystem.SaveProgress(other.GetComponent<PlayerStatus>().Player, LevelData.Instance.Data);
            StartCoroutine(FadeToBlack());
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
            SceneManager.LoadScene("TestScene");
        }
    }
}