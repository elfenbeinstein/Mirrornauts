using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border_Behaviour : MonoBehaviour
{
    public bool isBorder;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && isBorder)
        {
            Debug.Log("spaceship touched border");

            // MISSING: what happens if spaceship touches border
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!isBorder)
        {
            Debug.Log("spaceship out of bounds");
        }
    }
}
