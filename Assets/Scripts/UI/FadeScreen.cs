using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreen : MonoBehaviour
{
    [SerializeField] GameObject fadeScreen;
    private bool isFading = false;


    public IEnumerator FadeOut(float fadeTime, float rColor, float gColor, float bColor)
    {
        Color fadeColor = fadeScreen.GetComponent<Image>().color;
        float fadeAmount;

        isFading = true;
        if (fadeColor.a == 0f|| (fadeColor.a - 0f) < 0.006f)
        {
            while (fadeColor.a < 1f)
            {
                fadeAmount = fadeColor.a + (fadeTime * Time.deltaTime);

                fadeColor = new Color(rColor, gColor, bColor, fadeAmount);

                fadeScreen.GetComponent<Image>().color = fadeColor;
                
                yield return null;
            }
            isFading = false;
        }

    }

    public IEnumerator FadeIn(float fadeTime, float rColor, float gColor, float bColor)
    {
        Color fadeColor = fadeScreen.GetComponent<Image>().color;
        float fadeAmount;

        isFading = true;
        if (fadeColor.a == 1f || (fadeColor.a - 0f) > -0.006f)
        {
            while (fadeColor.a > 0f)
            {
                fadeAmount = fadeColor.a - (fadeTime * Time.deltaTime);
                fadeColor = new Color(fadeColor.r, fadeColor.g, fadeColor.b, fadeAmount);
                fadeScreen.GetComponent<Image>().color = fadeColor;
                yield return null;
            }
        }
        isFading = false;
    }
    public bool IsFading()
    {
        return isFading;
    }
}
