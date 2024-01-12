using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerLogic;

public class PlayerInput : MonoBehaviour
{

    private void Update()
    {
        MovementInputNormalized();
        Interact();
    }


    public Vector2 MovementInputNormalized()
    {
        
        Vector2 plrDirection = new Vector2(0, 0);

        if (Input.GetKey(KeyCode.W))
        {
           plrDirection.y += 1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            plrDirection.y -= 1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            plrDirection.x -= 1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            plrDirection.x += 1;
        }

        plrDirection = plrDirection.normalized;
        return plrDirection;

    }

    public bool Interact()
    {
        bool interacted = false;
        if(Input.GetKeyDown(KeyCode.E))
        {
            interacted = true;
            return interacted;
        }

        return interacted;

    }
}
