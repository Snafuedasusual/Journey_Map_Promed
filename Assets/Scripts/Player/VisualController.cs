using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualController : MonoBehaviour
{
    [SerializeField] SpriteRenderer sprRend;
    [SerializeField] Animator animator;
    [SerializeField] PlayerLogic plrLog;
    [SerializeField] PlayerInput plrInp;


    private void Update()
    {
        FaceDirection();
        Walk();
    }

    private void FaceDirection()
    {
        if (plrLog.FaceDir() == true)
        {
            sprRend.flipX = false;
        }
        if (plrLog.FaceDir() ==  false)
        {
            sprRend.flipX = true;
        }
    }

    private void Walk()
    {
        if (plrLog.IsWalking() == true)
        {
            animator.SetBool("walking", true);
        }
        if (plrLog.IsWalking() == false)
        {
            animator.SetBool("walking", false);
        }
    }
}
