using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockerST5 : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject detectedPlr;
        if (collision.gameObject.CompareTag("Player"))
        {
            detectedPlr = collision.gameObject;
            for (int i = 0; i < detectedPlr.transform.childCount; i++)
            {
                if (detectedPlr.transform.GetChild(i).CompareTag("FlagLogic"))
                {
                    if(detectedPlr.transform.GetChild(i).GetChild(0).GetComponent<SpriteRenderer>().sprite != null)
                    {
                        transform.gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}
