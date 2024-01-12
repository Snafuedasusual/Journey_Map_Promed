using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectTrigger : MonoBehaviour
{
    private bool Touched = false;
    private bool serp = false;
    private bool actifv = false;
    private bool i_deb = false;

    private PlayerInput inputPlayer;
    
    public delegate void Entered(bool stat, bool press);
    public Entered plrEntered;

    public delegate void Activation(bool act);
    public Activation tivate;

    

    private void Update()
    {
        InputInTrigger();
        
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerInput>(out PlayerInput plrInput))
        {
            Touched = true;
            actifv = true;
            inputPlayer = plrInput;
            ObjActive();

        }
        else
        {
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerInput>(out PlayerInput plrInput))
        {
            serp = false;
            Touched = false;
            i_deb = false;
            actifv = false;
            ObjActive();
            inputPlayer = null;
        }

        else
        {
            
        }
    }

    void InputInTrigger()
    {
        if (Touched == true && inputPlayer != null && inputPlayer.Interact() == true && i_deb == false)
        {
            i_deb = true;
            serp = true;
            plrEntered?.Invoke(Touched, serp);
            StartCoroutine(WaitSeconds());
        }

        IEnumerator WaitSeconds()
        {
            yield return new WaitForSecondsRealtime(0.5f);
            i_deb = false;
        }
    }

    void ObjActive()
    {
        if (actifv == true)
        {
            tivate?.Invoke(actifv);
        }
        if (actifv == false)
        {
            tivate?.Invoke(actifv);
        }
    }


}
