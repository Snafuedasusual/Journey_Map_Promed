using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DialoguePanel : MonoBehaviour
{
    public GameObject dialoguePlace;
    public Text _text;
    public Object transmitter;
    public string transmitterName;
    public GameObject plr;
    public Sprite transmitterImage;
    public Sprite transmitterFlag;

    [SerializeField] private GameObject _continue;
    [SerializeField] private GameObject _back;
    [SerializeField] private GameObject _exit;
    [SerializeField] private GameObject _yes;
    [SerializeField] private GameObject _no;

    [SerializeField] private Text _nameOnText;
    [SerializeField] private Image _bustImage;

    private bool binaryChoiceAnswer;

    public int[] binaryChoiceLine;
    public string[] npcSpeech;
    
    public bool _aktif = false;
    public int _aktifIndex = 0;
    public bool conversationDone = true;

    public IEnumerator activeCoroutine = null;
    
    public bool text_Debounce_initial = false;
    public bool text_Debounce = false;
    private float text_Debouncetime = 0.01f;



    private void Update()
    {
        TextToUI();
        TransmitterDetails();
    }

    private void TextToUI()
    {
        if (_aktif == true && text_Debounce_initial == false && activeCoroutine == null)
        {
            activeCoroutine = Typing();
            StartCoroutine(activeCoroutine);
            text_Debounce_initial = true;
        }
        activeCoroutine = null;
    }

    IEnumerator Typing()
    {
        CanBinaryChoice();
        CanContinueorExitorGoback();
        for (int i = 0; i < npcSpeech[_aktifIndex].Length + 1; i++)
        {
            _text.text = npcSpeech[_aktifIndex].Substring(0,i);
            yield return new WaitForSecondsRealtime(text_Debouncetime);
        } 
        activeCoroutine = null;
    }

    public void PositiveResponse()
    {
        text_Debounce = false;
        if (dialoguePlace.activeSelf == true)
        {
            if (transmitter.IsDosenST5() == true)
            {
                GameObject FlagLogicObj = null;
                SpriteRenderer FlagRenderer = null;
                for (int i = 0; i < plr.transform.childCount; i++)
                {
                    if (plr.transform.GetChild(i).gameObject.CompareTag("FlagLogic"))
                    {
                        FlagLogicObj = plr.transform.GetChild(i).gameObject;
                        FlagRenderer = FlagLogicObj.GetComponentInChildren<SpriteRenderer>();
                        FlagRenderer.sprite = transmitterFlag;
                        
                    }
                }
            }
            if(transmitter.IsUltimateEvil() == true)
            {
                binaryChoiceAnswer = true;
                Debug.Log("work");
            }
        }
        
    }

    public void NegativeResponse()
    {
        text_Debounce = false;
        if (activeCoroutine != null && _text.text != npcSpeech[_aktifIndex])
        {
            StopCoroutine(activeCoroutine);
            activeCoroutine = null;
            _text.text = " ";
            _text.text = npcSpeech[_aktifIndex];
        }
        if (activeCoroutine == null && text_Debounce == false && _aktifIndex < npcSpeech.Length - 1)
        {
            _aktifIndex++;
            _text.text = " ";
            activeCoroutine = Typing();
            StartCoroutine(activeCoroutine);
            text_Debounce = true;
        }
        if (activeCoroutine == null && text_Debounce == false && _aktifIndex == npcSpeech.Length - 1)
        {
            dialoguePlace.SetActive(false);
            _continue.SetActive(false);
            _back.SetActive(false);
            _exit.SetActive(false);
            _text.text = " ";
            _aktifIndex = 0;
            _aktif = false;
            conversationDone = true;
            plr.GetComponent<PlayerLogic>().canWalk = true;
        }
        if (transmitter.IsUltimateEvil() == true)
        {
            Debug.Log("work");
            binaryChoiceAnswer = false;
        }
    }

    public void NextLine()
    {
        text_Debounce = false;
        if (activeCoroutine != null && text_Debounce == true)
        {
            StopCoroutine(activeCoroutine);
            activeCoroutine = null;
            _text.text = " ";
            _text.text = npcSpeech[_aktifIndex];
            text_Debounce = false;
        }
        if (activeCoroutine == null && text_Debounce == false && _aktifIndex < npcSpeech.Length -1)
        {
            _aktifIndex++;
            _text.text = " ";
            activeCoroutine = Typing();
            StartCoroutine(activeCoroutine);
            text_Debounce = true;
        }
        
    }

    public void LineBefore()
    {
        text_Debounce = false;
        if(activeCoroutine != null && _text.text != npcSpeech[_aktifIndex])
        {
            StopCoroutine(activeCoroutine);
            activeCoroutine = null;
            _text.text = " ";
            _text.text = npcSpeech[_aktifIndex];
        }
        if (activeCoroutine == null && _aktifIndex > 0 && text_Debounce == false)
        {
            _aktifIndex--;
            _text.text = " ";
            activeCoroutine = Typing();
            StartCoroutine(activeCoroutine);
            text_Debounce = true;
        }
    }

    public void ExitConversation()
    {
        dialoguePlace.SetActive(false);
        _continue.SetActive(false);
        _back.SetActive(false);
        _exit.SetActive(false);
        _text.text = " ";
        _aktifIndex = 0;
        _aktif = false;
        plr.GetComponent<PlayerLogic>().canWalk = true;
        conversationDone = true;
    }

    private void CanContinueorExitorGoback()
    {
        if (dialoguePlace.activeSelf == true && _aktifIndex < npcSpeech.Length - 1 && _aktifIndex == 0)
        {
            _continue.SetActive(true);
            _back.SetActive(false);
            _exit.SetActive(false);
        }
        if (dialoguePlace.activeSelf == true && _aktifIndex < npcSpeech.Length - 1 && _aktifIndex > 0)
        {
            _continue.SetActive(true);
            _back.SetActive(true);
            _exit.SetActive(false);
        }
        if (dialoguePlace.activeSelf == true && _aktifIndex == npcSpeech.Length - 1 && npcSpeech.Length - 1 > 0)
        {
            _continue.SetActive(false);
            _back.SetActive(true);
            _exit.SetActive(true);
        }
        if (dialoguePlace.activeSelf == true && _aktifIndex == npcSpeech.Length - 1 && npcSpeech.Length - 1 == 0)
        {
            _continue.SetActive(false);
            _back.SetActive(false);
            _exit.SetActive(true);
        }
    }

    private void CanBinaryChoice()
    {
        if (dialoguePlace.activeSelf == true && binaryChoiceLine != null || dialoguePlace.activeSelf == true && binaryChoiceLine.Length == 0)
        {
            if ((binaryChoiceLine.Length - 1) == 0 && transmitter != null && transmitter.CanBinaryChoice() == true)
            {
                if (binaryChoiceLine[0] == _aktifIndex)
                {
                    _yes.SetActive(true);
                    _no.SetActive(true);
                }
                else
                {
                    _yes.SetActive(false);
                    _no.SetActive(false);
                }

            }

            if (binaryChoiceLine.Length >= 0 && transmitter != null && transmitter.CanBinaryChoice() == true)
            {
                for (int i = 0; i < binaryChoiceLine.Length - 1; i++)
                {
                    if (binaryChoiceLine[i] == _aktifIndex)
                    {
                        _yes.SetActive(true);
                        _no.SetActive(true);
                    }
                    else
                    {
                        _yes.SetActive(false);
                        _no.SetActive(false);
                    }
                }
            }

            else
            {
                _yes.SetActive(false);
                _no.SetActive(false);
            }
        }
        

        if (dialoguePlace.activeSelf == false)
        {
            _yes.SetActive(false);
            _no.SetActive(false);
        }
    }

    private void TransmitterDetails()
    {
        if (dialoguePlace.activeSelf == true)
        {
            if (transmitterName != null)
            {
                _nameOnText.text = transmitterName;
            }
            if (transmitterImage != null)
            {
                _bustImage.sprite = transmitterImage;
                _bustImage.enabled = true;
            }
        }
        else
        {
            if (transmitterImage == null)
            {
                _bustImage.sprite = null;
                _bustImage.enabled = false;
            }
            if (transmitterName == null)
            {
                _nameOnText.text = "Stranger";
            }
        }
    }

    public bool BinaryChoiceAnswer()
    {
        return binaryChoiceAnswer;
    }
}
