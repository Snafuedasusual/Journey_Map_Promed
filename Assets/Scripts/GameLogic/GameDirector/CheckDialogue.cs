using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDialogue : MonoBehaviour
{
    [SerializeField] DialoguePanel _dialPan;

    public int finalBossAnswer;

    private void Update()
    {
        CheckIfFinalBoss();
    }

    void CheckIfFinalBoss()
    {
        if (_dialPan.transmitter != null)
        {
            if (_dialPan.transmitter.IsUltimateEvil() == true && _dialPan.dialoguePlace.activeSelf == true)
            {
                if (_dialPan.BinaryChoiceAnswer() == true)
                {
                    Debug.Log("yes");
                    finalBossAnswer = 1;
                }
                if (_dialPan.BinaryChoiceAnswer() == false)
                {
                    Debug.Log("no");
                    finalBossAnswer = 2;
                }
                else
                {
                    finalBossAnswer = 0;
                }

            }
        }
    }

}
