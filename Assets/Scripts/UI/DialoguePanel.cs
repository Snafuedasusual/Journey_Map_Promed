using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePanel : MonoBehaviour
{
    public GameObject dialoguePlace;
    public Text _text;

    [SerializeField] private GameObject _continue;
    [SerializeField] private GameObject _back;
    [SerializeField] private GameObject _exit;

    public string[] npcSpeech;
    
    public bool _aktif = false;
    public int _aktifIndex = 0;
    
    private int _statetext = 0;
    public IEnumerator activeCoroutine = null;
    
    public bool text_Debounce_initial = false;
    private bool text_Debounce = false;
    private float text_Debouncetime = 0.01f;


    private void Update()
    {
        TextToUI();
        CanContinueorExitorGoback();
    }

    private void TextToUI()
    {
        if (_aktif == true && text_Debounce_initial == false && activeCoroutine == null)
        {
            activeCoroutine = Typing();
            StartCoroutine(activeCoroutine);
            text_Debounce_initial = true;
        }
    }

    IEnumerator Typing()
    {
        for(int i = 0; i < npcSpeech[_aktifIndex].Length + 1; i++)
        {
            _text.text = npcSpeech[_aktifIndex].Substring(0,i);
            yield return new WaitForSecondsRealtime(text_Debouncetime);
        } 
    }

    public void NextLine()
    {
        text_Debounce = false;
        if (activeCoroutine != null && _text.text != npcSpeech[_aktifIndex])
        {
            StopCoroutine(activeCoroutine);
            _text.text = " ";
            _text.text = npcSpeech[_aktifIndex];
        }
        if (activeCoroutine != null && text_Debounce == false && _aktifIndex < npcSpeech.Length -1)
        {
            _aktifIndex++;
            _text.text = " ";
            activeCoroutine = Typing();
            StartCoroutine(activeCoroutine);
            text_Debounce = true;
        }
        if (activeCoroutine != null && text_Debounce == false && _aktifIndex == npcSpeech.Length - 1)
        {
            _text.text = npcSpeech[_aktifIndex];
        }
        Debug.Log(_aktifIndex);
        Debug.Log(npcSpeech.Length);
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



}
