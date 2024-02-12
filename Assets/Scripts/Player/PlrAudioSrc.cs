using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlrAudioSrc : MonoBehaviour
{
    [Header("------Audio Source------")]
    [SerializeField] AudioSource _audSrc;

    [Header("---Audio Clips---")]
    [SerializeField] AudioClip _footstep1;
    [SerializeField] AudioClip _footstep2;
    [SerializeField] AudioClip _footstep3;

    [Header("Script References")]
    [SerializeField] PlayerLogic _plrLogic;
    [SerializeField] PlayerInput _plrInput;
    [SerializeField] VisualController _visualController;
    public void footSteps()
    {
        AudioClip[] _footSteps = {_footstep1, _footstep2, _footstep3};
        _audSrc.clip = _footSteps[Random.Range(0, _footSteps.Length)];
        _audSrc.loop = false;
        _audSrc.Play();
    }

}
