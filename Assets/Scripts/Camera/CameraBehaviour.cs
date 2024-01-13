using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera worldcam;
    [SerializeField] private CinemachineVirtualCamera playercam;
    [SerializeField] private PlayerInput plrInput;


    private void OnEnable()
    {
        plrInput.boolThrower = CameraAdjust;
    }

    void CameraAdjust(bool focusPlr)
    {
        if (focusPlr == true)
        {

            playercam.Priority = 1;
            worldcam.Priority = 0;
           
        }

        if (focusPlr == false)
        {
            playercam.Priority = 0;
            worldcam.Priority = 1;

        }
    }
}

