using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage3StandOff : MonoBehaviour
{
    [SerializeField] private GameObject playerPos;
    [SerializeField] private GameObject dosenSt3;
    [SerializeField] private GameObject fadeScreen;
    [SerializeField] private FadeScreen s_fadeScreen;
    [SerializeField] private CameraBehaviour camPlr;
    [SerializeField] private CinemachineVirtualCamera cam1;
    [SerializeField] private Transform camPos;

    [Header("---Fade Properties---")]
    [SerializeField] private float rColor;
    [SerializeField] private float bColor;
    [SerializeField] private float gColor;
    [SerializeField] private float fadeTime;

    private bool canProceed = true;
    private bool isEnabled = false;
    private GameObject detectedPlr;

    private DialoguePanel dialPan;
    private PlayerLogic plrLogic;


    private Color transparent;
    private Color opaque;

    [SerializeField] private int _stage = 0;

    public delegate void SpeakBegin(bool act, GameObject plr, Object objectSpeech);
    public SpeakBegin activate;


    private void Awake()
    {
        transparent = new Color(rColor, gColor, bColor, 0f);
        opaque = new Color(rColor, gColor, bColor, 1);
    }

    private void Update()
    {
        GunfightatNewAustin(detectedPlr);
    }
    void GunfightatNewAustin(GameObject plr)
    {
        
        if ((fadeScreen.GetComponent<Image>().color.a - opaque.a) < 0.006f && _stage == 1 && isEnabled == true && canProceed == true)
        {
            StartCoroutine(s_fadeScreen.FadeIn(fadeTime, rColor, gColor, bColor));
            if(s_fadeScreen.IsFading() == false && _stage == 1)
            {
                _stage++;
                dialPan.dialoguePlace.SetActive(true);
                dialPan._aktif = true;
                dialPan.text_Debounce_initial = false;
            }
        }
    }

    IEnumerator Wait_for_Seconds(float seconds)
    {
        canProceed = false;
        yield return new WaitForSeconds(seconds);
        canProceed = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (s_fadeScreen.IsFading() == false && _stage == 0)
            {
                StartCoroutine(s_fadeScreen.FadeOut(fadeTime, rColor, gColor, bColor));
                detectedPlr = collision.gameObject;
                if(_stage == 0 && isEnabled == false)
                {
                    plrLogic = detectedPlr.GetComponent<PlayerLogic>();
                    GetDialogue(collision.gameObject);
                    StartCoroutine(InitializerGunfight());
                }
            }

        }
    }

    void GetDialogue(GameObject plr)
    {
        dialPan = plr.GetComponent<DialoguePanel>();
        Object dosenSpeechLines = dosenSt3.GetComponent<Object>();
        dialPan.npcSpeech = dosenSpeechLines.DialogueStrings();
        if (dosenSpeechLines.BinaryChoiceLines() != null)
        {
            dialPan.binaryChoiceLine = dosenSpeechLines.BinaryChoiceLines();
        }
    }

    int GetAktifIndex()
    {
        return dialPan._aktifIndex;
    }

    IEnumerator InitializerGunfight()
    {
        PlayerLogic plrLogic = detectedPlr.GetComponent<PlayerLogic>();
        yield return new WaitForSecondsRealtime(1.5f);
        detectedPlr.transform.position = playerPos.transform.position;
        plrLogic.canWalk = false;
        cam1.LookAt = camPos.transform;
        cam1.Follow = camPos.transform;
        StartCoroutine(Wait_for_Seconds(0.5f));
        _stage++;
        isEnabled = true;
    }

    public void ReturnDialogue()
    {
        if(isEnabled == true)
        {
            plrLogic.canWalk = true;
            cam1.LookAt = detectedPlr.transform;
            cam1.Follow = detectedPlr.transform;
            dialPan.npcSpeech = null;
            if(dialPan.binaryChoiceLine != null)
            {
                dialPan.binaryChoiceLine = null; 
            }
            plrLogic = null;
            detectedPlr = null;
            isEnabled = false;
        }
    }
}