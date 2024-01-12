using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class Object : MonoBehaviour
{
    [SerializeField] private ObjectTrigger objTrig;
    [SerializeField] private SpriteRenderer objColor;
    [SerializeField] private GameObject obj;
    [SerializeField] private int numOfSlides;
    [SerializeField] private int defNum = 0;

    private Vector3 defSize = new Vector3 (1.05f, 1.05f, 1.05f);
    private Vector3 sizeAdd = new Vector3(0.1f, 0.1f, 0.1f);

    private bool onLine = false;

    bool _debounce = false;

    private void OnEnable()
    {
        objTrig.plrEntered = evenTesting;
        objTrig.tivate = ObjActivate;
    }

    private void OnDisable ()
    {
        objTrig.plrEntered -= evenTesting;
        objTrig.tivate -= ObjActivate;
    }

    void evenTesting(bool stat, bool press)
    {
        defNum++;
        if (defNum <= numOfSlides)
        {
            obj.transform.localScale += sizeAdd;
        }

        if (defNum > numOfSlides)
        {
            defNum = 0;
            obj.transform.localScale = defSize;
        }
    }

    void ObjActivate(bool isOn)
    {
        onLine = isOn;

        if (onLine == true)
        {
            objColor.color = Color.white;
            Debug.Log(onLine);
        }

        if (onLine == false)
        {
            objColor.color = Color.gray;
            Debug.Log(onLine);
        }

    }






}
