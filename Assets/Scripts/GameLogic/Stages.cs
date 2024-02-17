using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stages : MonoBehaviour
{
    [SerializeField] GameObject Blocker;
    [SerializeField] Object[] Dosens;

    [SerializeField] private bool isST5;

    public int interactedDosens = 0;

    private IEnumerator activeCorout;


    private void Update()
    {
        ChecksDosens();
    }

    void ChecksDosens()
    {
        if (isST5 == false)
        {
            if (activeCorout == null && isST5 == false)
            {
                activeCorout = CheckForInteraction();
                StartCoroutine(activeCorout);
            }

            if (interactedDosens == Dosens.Length)
            {
                Blocker.SetActive(false);
            }
            if (interactedDosens < Dosens.Length)
            {
                Blocker.SetActive(true);
            }
        }
    }

    IEnumerator CheckForInteraction()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        for(int i = 0; i < Dosens.Length; i++)
        {
            if (Dosens[i].HasInteracted().activeSelf == true)
            {
                interactedDosens++;
            }
        }
        activeCorout = null;
    }


}
