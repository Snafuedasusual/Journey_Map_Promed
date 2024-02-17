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
                StartCoroutine(FadeIn(_audSrc, fadeTime));
                _audSrc.loop = true;
            }
            if (_audSrc.CompareTag("Stinger"))
            {
                StartCoroutine(StingerActive());
            }
            if (_audSrc.CompareTag("Music"))
            {
                StartCoroutine(FadeIn(_audSrc, fadeTime));
                _audSrc.loop = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(FadeOut(_audSrc, fadeTime));
            _inside = false;
        }
    }


    private IEnumerator FadeOut(AudioSource sfxsrc, float FadeTime)
    {
        float startVolume = sfxsrc.volume;
        while (sfxsrc.volume > 0)
        {
            sfxsrc.volume -= startVolume * Time.deltaTime / FadeTime;
            yield return null;
        }
        sfxsrc.Stop();
    }

    private IEnumerator FadeIn(AudioSource sfxsrc, float FadeTime)
    {
        sfxsrc.Play();
        sfxsrc.volume = 0f;
        while (sfxsrc.volume < 1)
        {
            sfxsrc.volume += Time.deltaTime / FadeTime;
            yield return null;
        }
    }

    private IEnumerator StingerActive()
    {
      while (_inside == true)
        {
            yield return new WaitForSecondsRealtime(Random.Range(15f, 30f));
          if (_audSrc.CompareTag("Stinger") && _audSrc.isPlaying == false)
          {
            _audSrc = _audioFolder.transform.GetChild(Random.Range(0, _audioFolder.transform.childCount)).GetComponent<AudioSource>();
            float _clipLength = _audSrc.clip.length - 10;
            StartCoroutine(FadeIn(_audSrc, fadeTime));
            StartCoroutine(StingerNearEnd(_audSrc, _clipLength));
           }
        }
    }

    IEnumerator StingerNearEnd(AudioSource _sfxSrc, float remainingSecs)
    {
        yield return new WaitForSeconds(remainingSecs);
        StartCoroutine(FadeOut(_sfxSrc, fadeTime));
    }


}
