using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border_Behaviour : MonoBehaviour
{
    private Player _player;

    [Tooltip("If isBorder is true the object is an object the player must not touch. " +
        "If it's false it is the playing field the spaceship must not leave.")]
    public bool isBorder;

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
            if (_player == null)
            {
                _player = FindObjectOfType<Player>();
                if (_player == null)
                {
                    Debug.LogWarning(gameObject + " can't find player script");
                    return;
                }
            }
            if (!cooldown)
            {
                _player.PlayerHit();
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

            if (_player == null)
            {
                _player = FindObjectOfType<Player>();
                if (_player == null)
                {
                    Debug.LogWarning(gameObject + " can't find player script");
                    return;
                }
            }
            if (!cooldown)
            {
                _player.PlayerOutOfBounds();
                StartCoroutine(CollisionCooldown());
            }
        }
    }
    IEnumerator CollisionCooldown()
    {
        cooldown = true;
        yield return new WaitForSeconds(0.2f);
        cooldown = false;
    }
}
