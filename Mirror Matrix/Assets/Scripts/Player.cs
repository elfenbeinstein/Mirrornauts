using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int currentHealth;
    [SerializeField] private int maxHealth;
    [SerializeField] Stats _stats;
    [SerializeField] private SpaceshipBehaviour _spaceshipBehaviour;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void PlayerHit()
    {
        //Debug.Log("spaceship touched border");
        currentHealth -= 1;
        Debug.Log($"current hp is {currentHealth}");

        if (currentHealth == 0)
        {
            PlayerDeath();
        }
    }

    public void PlayerOutOfBounds()
    {
        //Debug.Log("spaceship out of bounds");

        currentHealth -= 1;
        Debug.Log($"current hp is {currentHealth}");

        _spaceshipBehaviour.ResetSpaceshipFromBorder();

        if (currentHealth == 0)
        {
            PlayerDeath();
        }
    }

    private void PlayerDeath()
    {
        Debug.Log("player out of health");
    }
}
