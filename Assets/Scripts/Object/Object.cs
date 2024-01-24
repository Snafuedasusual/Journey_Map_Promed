using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class Object : MonoBehaviour
{
    [SerializeField] private ObjectTrigger objTrig;
    [SerializeField] private SpriteRenderer objColor;
    [SerializeField] private GameObject obj;
    [SerializeField] private int numOfSlides;
    [SerializeField] private int defNum = 0;

    [SerializeField] private string[] _speech;

    PlayerInput textPanel;

    private Vector3 defSize = new Vector3 (1.05f, 1.05f, 1.05f);
    private Vector3 sizeAdd = new Vector3(0.1f, 0.1f, 0.1f);

    private bool onLine = false;

    private void OnEnable()
    {
        objTrig.activate = ObjActivate;
    }

    private void OnDisable ()
    {
        objTrig.activate -= ObjActivate;
    }

    void evenTesting(bool stat, bool press)
    {
        defNum++;
        if (defNum <= numOfSlides)
        {
            obj.transform.localScale += sizeAdd;
        }

        if (defNum > numOfSlides)
        {
            defNum = 0;
            obj.transform.localScale = defSize;
        }
    }

    void ObjActivate(bool isOn, GameObject plr)
    {
        onLine = isOn;
        DialoguePanel dialPan = plr.GetComponent<DialoguePanel>();

        if (onLine == true &&  plr != null)
        {
            dialPan.dialoguePlace.SetActive(true);
            dialPan.npcSpeech = _speech;
            dialPan._aktif = true;
            dialPan.text_Debounce_initial = false;
        }
        if (onLine == false && plr != null)
        {
            dialPan.dialoguePlace.SetActive(false);
            dialPan.npcSpeech = null;
            dialPan._aktif = false;
            dialPan.text_Debounce_initial = false;
            dialPan._text.text = " ";
            dialPan._aktifIndex = 0;
            StopCoroutine(dialPan.activeCoroutine);
            dialPan.activeCoroutine = null;
        }
    }

    void unused(bool isOn, GameObject plr)
    {
        onLine = isOn;
        if (onLine == true && plr != null)
        {
            objColor.color = Color.white;
            Debug.Log(plr);
            // textPanel.dialoguePanel.SetActive(true);

        }

        if (onLine == false && plr != null)
        {
            objColor.color = Color.gray;
            Debug.Log(plr);
            //textPanel.dialoguePanel.SetActive(false);
        }
    }






}
