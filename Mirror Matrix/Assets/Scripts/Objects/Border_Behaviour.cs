using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Border Behaviour handles borders and playing field
/// If player touches border --> damage and reset
/// If player leaves playing field alltogether --> damage and reset
/// </summary>

public class Border_Behaviour : MonoBehaviour
{
    [Tooltip("If isBorder is true the object is an object the player must not touch. " +
        "If it's false it is the playing field the spaceship must not leave.")]
    [SerializeField] private bool isBorder;

    private bool cooldown;

    private void Start()
    {
        cooldown = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isBorder)
        {
            //Debug.Log("spaceship touched border");

            // MISSING: what happens if spaceship touches border

            if (!cooldown)
            {
                EventManager.Instance.EventGo("PLAYER", "HitBorder");
                StartCoroutine(CollisionCooldown());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // if it leaves the playing field
        if (!isBorder)
        {
            //Debug.Log("spaceship out of bounds");
            if (!cooldown)
            {
                EventManager.Instance.EventGo("PLAYER", "LeaveField");
                StartCoroutine(CollisionCooldown());
            }
        }
    }

    // used for testing
    IEnumerator CollisionCooldown()
    {
        cooldown = true;
        yield return new WaitForSeconds(0.2f);
        cooldown = false;
    }
}
