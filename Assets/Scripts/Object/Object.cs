using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    [SerializeField] private ObjectTrigger objTrig;
    [SerializeField] private Sprite objFlag;
    [SerializeField] private Sprite objImage;
    [SerializeField] private GameObject obj;
    [SerializeField] private bool isDosen;
    [SerializeField] private bool isDosenST5;
    [SerializeField] private bool isUltimateEvil;
    [SerializeField] private bool canBinaryChoice;
    [SerializeField] private string objName;
    [SerializeField] private GameObject hasInteracted;

    private GameObject activeReceiver;
    private Object activeTransmitter;

    [SerializeField] private string[] _speech;
    [SerializeField] private int[] _binaryChoiceLine;

    PlayerInput textPanel;

    private Vector3 defSize = new Vector3 (1.05f, 1.05f, 1.05f);
    private Vector3 sizeAdd = new Vector3(0.1f, 0.1f, 0.1f);

    private bool onLine = false;

    private void Update()
    {
        CheckConversation();
    }

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

        CheckFlags(plr);
        DialoguePanel dialPan = activeReceiver.GetComponent<DialoguePanel>();

        if (onLine == true &&  plr != null)
        {
            dialPan.dialoguePlace.SetActive(true);
            dialPan.npcSpeech = _speech;
            dialPan.binaryChoiceLine = _binaryChoiceLine;
            dialPan._aktif = true;
            dialPan.text_Debounce_initial = false;
            dialPan.transmitter = activeTransmitter;
            dialPan.plr = activeReceiver;
            dialPan.transmitterName = objName;
            dialPan.transmitterFlag = objFlag;
            dialPan.transmitterImage = objImage;
            dialPan.conversationDone = false;
        }
        if (onLine == false && plr != null)
        {
            dialPan._aktif = false;
            dialPan.text_Debounce_initial = false;
            dialPan.text_Debounce_initial = false;
            dialPan._text.text = " ";
            dialPan._aktifIndex = 0;
            dialPan.transmitter = null;
            dialPan.plr = null;

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
            dialPan.transmitterName = null;
            dialPan.transmitterFlag = null;
            dialPan.transmitterImage = null;
            dialPan.dialoguePlace.SetActive(false);
            activeTransmitter = null;
            plr.GetComponent<PlayerLogic>().canWalk = true;
            activeReceiver = null;
        }
        if(isUltimateEvil == true && onLine == true && plr != null)
        {
            plr.GetComponent<PlayerLogic>().canWalk = false;
            dialPan.dialoguePlace.SetActive(true);
            dialPan.npcSpeech = _speech;
            dialPan.binaryChoiceLine = _binaryChoiceLine;
            dialPan._aktif = true;
            dialPan.text_Debounce_initial = false;
            dialPan.transmitter = activeTransmitter;
            dialPan.plr = activeReceiver;
            dialPan.transmitterName = objName;
            dialPan.transmitterFlag = objFlag;
            dialPan.transmitterImage = objImage;
            dialPan.conversationDone = false;
        }
        if (isUltimateEvil == true && onLine == false && plr != null)
        {
            dialPan._aktif = false;
            dialPan.text_Debounce_initial = false;
            dialPan.text_Debounce_initial = false;
            dialPan._text.text = " ";
            dialPan._aktifIndex = 0;
            dialPan.transmitter = null;
            dialPan.plr = null;

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
            dialPan.transmitterName = null;
            dialPan.transmitterFlag = null;
            dialPan.transmitterImage = null;
            dialPan.dialoguePlace.SetActive(false);
            activeTransmitter = null;
            plr.GetComponent<PlayerLogic>().canWalk = true;
            activeReceiver = null;
        }

    }

    SpriteRenderer CheckFlags(GameObject plr)
    {
        GameObject FlagLogicObj = null;
        SpriteRenderer FlagVisual = null;


        for (int i = 0; i < plr.transform.childCount; i++)
        {
            if (plr.transform.GetChild(i).gameObject.CompareTag("FlagLogic"))
            {
                FlagLogicObj = plr.transform.GetChild(i).gameObject;
                FlagVisual = FlagLogicObj.GetComponent<SpriteRenderer>();
            }
            else
            {
                return null;
            }
        }

        return FlagVisual;

    }

    public string[] DialogueStrings()
    {
        return _speech;
    }

    public int[] BinaryChoiceLines()
    {
        return _binaryChoiceLine;
    }

    public bool IsDosenST5()
    {
        return isDosenST5;
    }

    public bool CanBinaryChoice()
    {
        return canBinaryChoice;
    }

    public GameObject HasInteracted()
    {
        return hasInteracted;
    }

    void CheckConversation()
    {
        if (activeReceiver != null && hasInteracted != null && hasInteracted.activeSelf == false && isDosen == true)
        {
            DialoguePanel dialPan = activeReceiver.GetComponent<DialoguePanel>();
            if (dialPan.conversationDone == true)
            {
                hasInteracted.SetActive(true);
            }
        }
    }

    public bool IsUltimateEvil()
    {
        return isUltimateEvil;
    }
}
