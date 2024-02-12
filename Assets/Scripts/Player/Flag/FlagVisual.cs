using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagVisual : MonoBehaviour
{
    [SerializeField] private FlagLogic _flagLogic;
    [SerializeField] SpriteRenderer _sprRenderer;

    private void Update()
    {
        FlagDirection();
    }

    private void FlagDirection()
    {
        if (_flagLogic.FlagFaceDirection() == true)
        {
            _sprRenderer.flipX = false;
        }
        if (_flagLogic.FlagFaceDirection() == false)
        {
            _sprRenderer.flipX = true;
        }
    }
}
