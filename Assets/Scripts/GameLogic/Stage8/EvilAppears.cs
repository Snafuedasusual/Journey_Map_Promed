using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class EvilAppears : MonoBehaviour
{
    [SerializeField] GameObject UltimateEvil;
    [SerializeField] SpriteRenderer UltimateEvilRenderer;
    [SerializeField] float fadeTime;
    [SerializeField] private CinemachineVirtualCamera _cam;
    [SerializeField] private CameraBehaviour _cameraBehaviour;
    [SerializeField] private GameObject _passOrNot;
    [SerializeField] private GameObject victoryController;
    [SerializeField] private GameObject _currentAudio;
    [SerializeField] private GameObject _bossMusic;

    private GameObject detectedPlr;

    public Color opaque = new Color(255, 255, 255, 255);
    public Color transparent = new Color(255, 255, 255, 0);

    public Color ultimateEvilColor;

    public int initializerStages;
    private IEnumerator activeCoroutine;

    private bool isActive;

    private void Awake()
    {
        ultimateEvilColor = UltimateEvilRenderer.color;
    }

    private void Update()
    {
        Initializer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            detectedPlr = collision.gameObject;
            initializerStages++;
            _passOrNot.SetActive(true);
            victoryController.SetActive(true);
            _currentAudio.SetActive(false);
            _currentAudio.GetComponent<AudioSource>().Stop();
            _bossMusic.SetActive(true);
            _bossMusic.GetComponent<AudioSource>().Play();
            isActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        detectedPlr = collision.gameObject;
        initializerStages = 0;
        StopCoroutine(activeCoroutine);
        activeCoroutine = null;
        _passOrNot.SetActive(false);
        _currentAudio.SetActive(true);
        victoryController.SetActive(false);
        _bossMusic.SetActive(false);
        _bossMusic.GetComponent<AudioSource>().Stop();
        isActive = false;
    }

    IEnumerator ForceCamPlayer()
    {
        _cameraBehaviour.ForcePlayerCam(true);
        yield return new WaitForSecondsRealtime(2.1f);
        activeCoroutine = null;
        initializerStages++;
    }

    IEnumerator FadeIn()
    {
        _bossMusic.GetComponent<AudioSource>().Play();
        float fadeColor = UltimateEvilRenderer.GetComponent<SpriteRenderer>().color.a;
        if (ultimateEvilColor.a < 1.0f)
        {
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / fadeTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(fadeColor, 1.0f, t));
                UltimateEvilRenderer.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }
            UltimateEvilRenderer.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 255f);
        }
    }

    IEnumerator FocusOnEvil(Transform target, Transform i_target)
    {
        initializerStages++;
        _cam.Follow = target;
        _cam.LookAt = target;
        StartCoroutine(FadeIn());
        yield return new WaitForSecondsRealtime(2.5f);
        _cam.Follow = i_target;
        _cam.LookAt = i_target;
        activeCoroutine = null;
    }

    void Initializer()
    {
        if (initializerStages == 1 && activeCoroutine == null)
        {
            activeCoroutine = ForceCamPlayer();
            StartCoroutine(activeCoroutine);
        }
        if (initializerStages == 2 && activeCoroutine == null)
        {
            activeCoroutine = FocusOnEvil(UltimateEvil.transform, detectedPlr.transform);
            StartCoroutine(FocusOnEvil(UltimateEvil.transform, detectedPlr.transform));
        }
    }

    public void ReEnableAudio()
    {
        if (isActive == true)
        {
            _currentAudio.SetActive(true);
            _bossMusic.SetActive(false);
            _bossMusic.GetComponent<AudioSource>().Stop();
            isActive = false;
        }
    }

    public void endingMusic()
    {
        if (isActive == true)
        {
            _currentAudio.SetActive(true);
            _currentAudio.GetComponent<AudioSource>().Play();
            _bossMusic.SetActive(false);
            _bossMusic.GetComponent<AudioSource>().Stop();
            isActive = false;
        }
    }
}
