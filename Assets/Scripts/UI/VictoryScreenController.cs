using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryScreenController : MonoBehaviour
{
    [SerializeField] GameObject victory;
    [SerializeField] Image screen;
    [SerializeField] GameObject victoryText;
    [SerializeField] GameObject tryAgain;
    [SerializeField] float fadeTime;
    [SerializeField] PlayerLogic plrLogic;
    private int initiializerStages = 0;
    private IEnumerator activeCorout;

    private void Update()
    {
        ScreenInitializer();
    }

    public void ScreenTransparent()
    {
        victory.SetActive(false);
        victoryText.SetActive(false);
        tryAgain.SetActive(false);
        screen.color = new Color(0f,0f,0f,255f);
        plrLogic.canWalk = true;
        initiializerStages = 0;
    }

    void ScreenInitializer()
    {
        if (initiializerStages == 1)
        {
            activeCorout = FadeOut();
            StartCoroutine(activeCorout);
            Debug.Log("Work");
        }
        if (initiializerStages == 2)
        {
            victoryText.SetActive(true);
            tryAgain.SetActive(true);
        }
    }

    IEnumerator FadeOut()
    {
        
        float fadeColor = screen.color.a;
        if (screen.color.a < 1.0f)
        {
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / fadeTime)
            {
                Color newColor = new Color(0, 0, 0, Mathf.Lerp(fadeColor, 1.0f, t));
                screen.color = newColor;
                yield return null;
            }
            activeCorout = null;
            initiializerStages++;
        }
    }

    public void victoryClick()
    {
        if(transform.gameObject.activeSelf == true)
        {
            initiializerStages++;
            plrLogic.canWalk = false;
            victory.SetActive(true);
        }
    }
}
