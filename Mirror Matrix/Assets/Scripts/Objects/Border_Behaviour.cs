using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Border Behaviour handles borders and playing field
/// If player touches border -->  // obsolete
/// If player leaves playing field alltogether --> damage and reset
/// </summary>

public class Border_Behaviour : MonoBehaviour
{
    [Tooltip("If isBorder is true the object is an object the player must not touch. " +
        "If it's false it is the playing field the spaceship must not leave.")]
    [SerializeField] private bool isBorder;
    public bool win;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if spaceship touched border
        if (isBorder)
        {
            EventManager.Instance.EventGo("PLAYER", "HitBorder");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // if it leaves the playing field
        if (!isBorder)
        {
            if (!win) EventManager.Instance.EventGo("PLAYER", "LeftField");
        }
    }
}
