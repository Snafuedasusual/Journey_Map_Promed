using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagLogic : MonoBehaviour
{
    [SerializeField] PlayerLogic _plrLogic;
    [SerializeField] FlagVisual _flagVis;
    private bool _flagFaceDir;
    private bool _plrFaceDir;

    private void Update()
    {
        FlagDirection();

    }

    void FlagDirection()
    {
        _plrFaceDir = _plrLogic.FaceDir();

        if (_plrFaceDir == true)
        {
            _flagFaceDir = true;
        }
        if (_plrFaceDir == false)
        {
            _flagFaceDir = false;
        }

    }

    public bool FlagFaceDirection()
    {
        return _flagFaceDir;
    }
}
