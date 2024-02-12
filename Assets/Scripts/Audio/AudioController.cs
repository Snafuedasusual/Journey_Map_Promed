using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [Header("----Audio Controller Name----")]
    [SerializeField] private string ctrlName;

    [Header("----Audio Folder----")]
    [SerializeField] private GameObject _audioFolder;

    [Header("----Audio Source----")]
    [SerializeField] private AudioSource _audSrc;

    [Header("----Audio Fade Time----")]
    [SerializeField] private float fadeTime;

    [Header("Other Fields")]
    [SerializeField] private bool _inside;

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _inside = true;
           _audSrc = _audioFolder.transform.GetChild(Random.Range(0,_audioFolder.transform.childCount)).GetComponent<AudioSource>();
            if (_audSrc.CompareTag("Ambiance"))
            {
                StartCoroutine(FadeIn(fadeTime));
                _audSrc.loop = true;
            }
            if (_audSrc.CompareTag("Stinger"))
            {
                StartCoroutine(StingerActive());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(FadeOut(fadeTime));
            _inside = false;
        }
    }


    private IEnumerator FadeOut(float FadeTime)
    {
        float startVolume = _audSrc.volume;
        while (_audSrc.volume > 0)
        {
            _audSrc.volume -= startVolume * Time.deltaTime / FadeTime;
            yield return null;
        }
        _audSrc.Stop();
        _audSrc = null;
    }

    private IEnumerator FadeIn(float FadeTime)
    {
        _audSrc.Play();
        _audSrc.volume = 0f;
        while (_audSrc.volume < 1)
        {
            _audSrc.volume += Time.deltaTime / FadeTime;
            yield return null;
        }
    }

    private IEnumerator StingerActive()
    {
      while (_inside == true)
        {
          yield return new WaitForSecondsRealtime(Random.Range(5f, 10f));
          if (_audSrc.CompareTag("Stinger") && _audSrc.isPlaying == false)
          {
            _audSrc = _audioFolder.transform.GetChild(Random.Range(0, _audioFolder.transform.childCount)).GetComponent<AudioSource>();
            StartCoroutine(FadeIn(fadeTime));
            _audSrc.loop = false;
          }
        }
    }
}
