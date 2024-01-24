using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    public delegate void Interactii(bool stats, int presses);
    public Interactii interacty;

    [SerializeField] private PlayerInput plrInp;


    private void Update()
    {
        Movement_and_Rotation();
    }

    void Movement_and_Rotation()
    {
        Vector3 moveDir = new Vector3(plrInp.MovementInputNormalized().x, plrInp.MovementInputNormalized().y, 0);

        transform.up += new Vector3(moveDir.x, moveDir.y, 0);

        transform.position += new Vector3(moveDir.x, moveDir.y, transform.position.z) * Time.deltaTime * 6f;
    }



    
}
