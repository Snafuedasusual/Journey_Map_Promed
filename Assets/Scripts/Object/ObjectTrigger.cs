using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectTrigger : MonoBehaviour
{
    [SerializeField] private Object objectSpeech;
    private bool press = false;
    private bool active = false;
    private bool i_input_debounce = false;

    private GameObject detectedPlayer;

    private PlayerInput inputPlayer;
    
    public delegate void Entered(bool stat, bool press);
    public Entered plrEntered;

    public delegate void Activation(bool act, GameObject plr, Object objectSpeech);
    public Activation activate;

    

    private void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            active = true;
            detectedPlayer = collision.gameObject;
            ObjActive();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            active = false;
            detectedPlayer = collision.gameObject;
            ObjActive();
        }
    }

    void InputInTrigger()
    {
        if (active == true && inputPlayer != null && inputPlayer.Interact() == true && i_input_debounce == false)
        {
            i_input_debounce = true;
            press = true;
            plrEntered?.Invoke(active, press);
            StartCoroutine(WaitSeconds());
        }

        IEnumerator WaitSeconds()
        {
            yield return new WaitForSecondsRealtime(0.5f);
            i_input_debounce = false;
        }
    }

    void ObjActive()
    {
        if (active == true)
        {
            activate?.Invoke(active, detectedPlayer, objectSpeech);
        }
        if (active == false)
        {
            activate?.Invoke(active, detectedPlayer, objectSpeech);
        }
    }


}
