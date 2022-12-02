using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles Player Hit
/// Handles Player Out of bounds (when spaceship leaves the playing field
/// Handles Player Health (still needs work)
/// </summary>

public class Player : MonoBehaviour
{
    private int currentHealth;
    [SerializeField] private int maxHealth;
    [SerializeField] private float maxEnergy;
    [SerializeField] Stats _stats;
    [SerializeField] private SpaceshipBehaviour _spaceshipBehaviour;
    private bool shieldActive;

    void Start()
    {
        currentHealth = maxHealth;
        EventManager.Instance.AddEventListener("PLAYER", PlayerListener);
    }

    private void OnDestroy()
    {
        EventManager.Instance.RemoveEventListener("PLAYER", PlayerListener);
    }

    void PlayerListener(string eventName, object param)
    {
        if (eventName == "HitObject")
            PlayerHitObject((ObjectBehaviour)param);
        else if (eventName == "HitBorder")
            PlayerHitBorder();
        else if (eventName == "LeftField")
            PlayerOutOfBounds();
        else if (eventName == "ShieldOn")
            shieldActive = true;
        else if (eventName == "ShieldOff")
            shieldActive = false;
    }

    public void PlayerHitObject(ObjectBehaviour hitObject)
    {
        Debug.Log("spaceship touched object");

        if (hitObject.isHazard)
            if (!shieldActive) DamageTaken(1);
        else
        {
            // elfenbeinstein MISSING: collectible
            hitObject.RemoveSelfFromList();
        }
    }

    public void PlayerHitBorder()
    {
        Debug.Log("spaceship touched border");
        DamageTaken(1);
    }

    public void PlayerOutOfBounds()
    {
        Debug.Log("spaceship out of bounds");

        DamageTaken(1);

        _spaceshipBehaviour.ResetSpaceshipFromBorder();
    }

    private void DamageTaken(int amount)
    {
        currentHealth -= amount;
        Debug.Log($"current hp is {currentHealth}");

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
