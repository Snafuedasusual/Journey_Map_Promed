using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassOrNot : MonoBehaviour
{
    [SerializeField] CheckDialogue _checkDial;

    [SerializeField] GameObject[] Dosens;

    [SerializeField] EvilAppears _evilAppears;

    [SerializeField] Entrance _entrance;

    [SerializeField] Stages[] _stages;

    [SerializeField] GameObject Player;

    [SerializeField] GameObject startPlrPos;

    private void Update()
    {

    }

    void Answer()
    {
        if (_checkDial.finalBossAnswer == 2)
        {
            Debug.Log("work");
            for (int i = 0; i < Dosens.Length; i++)
            {
                for(int j = 0; j < Dosens[i].transform.childCount; j++)
                {
                    if (Dosens[i].transform.GetChild(j).CompareTag("HasInteracted"))
                    {
                        Dosens[i].transform.GetChild(j).gameObject.SetActive(false);
                    }
                }
            }
            _entrance._stage = 0;
            _entrance._dosenST4.transform.position = _entrance._startPos.transform.position;

            _evilAppears.ultimateEvilColor = _evilAppears.transparent;
            _evilAppears.initializerStages = 0;

            Player.transform.position = startPlrPos.transform.position;

            for (int f = 0; f < _stages.Length; f++)
            {
                _stages[f].interactedDosens = 0;
            }
            _checkDial.finalBossAnswer = 0;
        }
    }

    public void answerNo()
    {
        if(transform.gameObject.activeSelf == true)
        {
            for (int i = 0; i < Dosens.Length; i++)
            {
                for (int j = 0; j < Dosens[i].transform.childCount; j++)
                {
                    if (Dosens[i].transform.GetChild(j).CompareTag("HasInteracted"))
                    {
                        Dosens[i].transform.GetChild(j).gameObject.SetActive(false);
                    }
                }
            }
            _entrance._stage = 0;
            _entrance._dosenST4.transform.position = _entrance._startPos.transform.position;

            _evilAppears.ultimateEvilColor = _evilAppears.transparent;
            _evilAppears.initializerStages = 0;

            Player.transform.position = startPlrPos.transform.position;

            for (int f = 0; f < _stages.Length; f++)
            {
                _stages[f].interactedDosens = 0;
            }
            _checkDial.finalBossAnswer = 0;
        }
    }
}
