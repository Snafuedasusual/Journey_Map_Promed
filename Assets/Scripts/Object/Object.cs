using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class Object : MonoBehaviour
{
    [SerializeField] private ObjectTrigger objTrig;
    [SerializeField] private Sprite objFlag;
    [SerializeField] private Sprite objImage;
    [SerializeField] private GameObject obj;
    [SerializeField] private bool isDosen;
    [SerializeField] private bool isDosenST5;

    private GameObject activeReceiver;
    private Object activeTransmitter;

    [SerializeField] private string[] _speech;
    [SerializeField] private int[] _binaryChoiceLine;

    PlayerInput textPanel;

    private Vector3 defSize = new Vector3 (1.05f, 1.05f, 1.05f);
    private Vector3 sizeAdd = new Vector3(0.1f, 0.1f, 0.1f);

    private bool onLine = false;

    private void OnEnable()
    {
        objTrig.activate += ObjActivate;
    }

    private void OnDisable ()
    {
        objTrig.activate -= ObjActivate;
    }

    void ObjActivate(bool isOn, GameObject plr, Object objectSpeech)
    {
        onLine = isOn;
        activeReceiver = plr;
        activeTransmitter = objectSpeech;

        DialoguePanel dialPan = activeReceiver.GetComponent<DialoguePanel>();

        if (onLine == true &&  plr != null)
        {
            dialPan.dialoguePlace.SetActive(true);
            dialPan.npcSpeech = _speech;
            dialPan._aktif = true;
            dialPan.text_Debounce_initial = false;
        }
        if (onLine == false && plr != null)
        {
            dialPan._aktif = false;
            dialPan.text_Debounce_initial = false;
            dialPan.text_Debounce_initial = false;
            dialPan._text.text = " ";
            dialPan._aktifIndex = 0;

            if (dialPan.activeCoroutine != null)
            {
                StopCoroutine(dialPan.activeCoroutine);
                dialPan.activeCoroutine = null;
                dialPan.npcSpeech = null;
            }
            else
            {
                dialPan.activeCoroutine = null;
                dialPan.npcSpeech = null;

            }
         
            dialPan.dialoguePlace.SetActive(false);
            activeTransmitter = null;
            activeReceiver = null;
        }

    }

    public string[] DialogueStrings()
    {
        return _speech;
    }

    public int[] BinaryChoiceLines()
    {
        return _binaryChoiceLine;
    }
}
