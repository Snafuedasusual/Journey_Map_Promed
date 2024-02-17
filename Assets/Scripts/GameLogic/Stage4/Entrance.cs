using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _cam;
    [SerializeField] private CameraBehaviour _cameraBehaviour;
    public GameObject _dosenST4;
    [SerializeField] private SpriteRenderer _dosenST4renderer;

    public GameObject _startPos;
    public GameObject _midPos;
    public GameObject _endPos;

    private DialoguePanel dialoguePanel;

    private GameObject detectedPlr;

    [SerializeField] private float duration;

    private float elapsedTime;
    
    private bool isActive = false;
    public int _stage = 0;


    private void Update()
    {
        ST4Entrance();
        LookAtPlayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && _stage == 0)
        {
            Debug.Log("work");
            detectedPlr = collision.gameObject;
            dialoguePanel = detectedPlr.GetComponent<DialoguePanel>();
            StartCoroutine(Initializer(collision.transform));
        }
    }
    IEnumerator Initializer(Transform player)
    {
        _cameraBehaviour.ForcePlayerCam(true);
        yield return new WaitForSecondsRealtime(2);
        _stage++;
        isActive = true;
        StartCoroutine(ChangePrespective(_dosenST4.transform, player));

    }

    void ST4Entrance()
    {
        if (_stage == 1 && isActive == true)
        {
            _dosenST4.SetActive(true);
            elapsedTime += Time.deltaTime;
            float percentageComplete = elapsedTime / duration;

            _dosenST4.transform.position = Vector3.Lerp(_startPos.transform.position, _midPos.transform.position, percentageComplete);
            if(transform.position == _midPos.transform.position)
            {
                _cameraBehaviour.ForcePlayerCam(false);
            }
            if (_dosenST4.GetComponent<Object>().HasInteracted().activeSelf == true && isActive == true)
            {
                _stage++;
                elapsedTime = 0;
                percentageComplete = 0;
                if (_stage == 2 && isActive == true)
                {
                    Debug.Log(_stage);
                    elapsedTime += Time.deltaTime;
                    percentageComplete = elapsedTime / duration;
                    _dosenST4.transform.position = Vector3.Lerp(_midPos.transform.position, _endPos.transform.position, percentageComplete);

                    if (_dosenST4.transform.position == _endPos.transform.position)
                    {
                        _stage++;
                        isActive = false;
                    }
                }
            }
        }
    }

    IEnumerator ChangePrespective(Transform target, Transform i_target)
    {
        _cam.Follow = target;
        _cam.LookAt = target;
        yield return new WaitForSecondsRealtime(4f);
        _cam.Follow = i_target;
        _cam.LookAt = i_target;
    }

    int DialoguePanelChecker()
    {
        return dialoguePanel._aktifIndex;
    }


    void LookAtPlayer()
    {
        if (detectedPlr != null)
        {
            Vector3 dosenToPlayer = (detectedPlr.transform.position - _dosenST4.transform.position).normalized;
            if (dosenToPlayer.x < 0)
            {
                _dosenST4renderer.flipX = true;
            }
            else
            {
                _dosenST4renderer.flipX = false;
            }
        }
    }
}
