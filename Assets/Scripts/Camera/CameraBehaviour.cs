using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] PlayerLogic playerLogic;
    [SerializeField] private CinemachineVirtualCamera worldcam;
    [SerializeField] private CinemachineVirtualCamera playercam;
    [SerializeField] private PlayerInput plrInput;


    private void OnEnable()
    {
        plrInput.boolThrower = CameraAdjust;
    }

    private void OnDisable ()
    {
        plrInput.boolThrower -= CameraAdjust;
    }

    private void Update()
    {
        CheckIfInCutscene();
        CheckIfSpeaking();
    }

    void CameraAdjust(bool focusPlr, bool canWalk)
    {
        if (focusPlr == true && canWalk == true)
        {

            playercam.Priority = 1;
            worldcam.Priority = 0;
           
        }

        if (focusPlr == false && canWalk == true)
        {
            playercam.Priority = 0;
            worldcam.Priority = 1;

        }
    }

    void CheckIfInCutscene()
    {
        if(playerLogic.canWalk == false && worldcam.Priority > playercam.Priority)
        {
            playercam.Priority = 1;
            worldcam.Priority = 0;
        }
    }

    void CheckIfSpeaking()
    {
        if (playerLogic.CheckIfConversation() == true && worldcam.Priority > playercam.Priority)
        {
            playercam.Priority = 1;
            worldcam.Priority = 0;
        }
    }

    public void ForcePlayerCam(bool turnOn)
    {
        if (turnOn == true && worldcam.Priority > playercam.Priority)
        {
            playercam.Priority = 1;
            worldcam.Priority = 0;
            worldcam.enabled = false;
        }
    }
}

