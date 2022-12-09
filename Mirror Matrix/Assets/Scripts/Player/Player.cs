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
        if (eventName == "HitBorder")
            PlayerHitBorder();
        else if (eventName == "LeftField")
            PlayerOutOfBounds();
        else if (eventName == "HitObject")
            PlayerHitObject((ObjectBehaviour)param);
        else if (eventName == "HitHazard")
            PlayerHitHazard((int)param);
    }
    
    public void PlayerHitObject(ObjectBehaviour hitObject)
    {
        //Debug.Log("spaceship touched object");
        if (hitObject.gameObject.GetComponent<PowerUps>() != null) hitObject.gameObject.GetComponent<PowerUps>().AddToPlayer(_stats);
        else Debug.Log(hitObject + " is missing powerup script");
        hitObject.RemoveSelfFromList();
    }

    public void PlayerHitHazard(int damage)
    {
        DamageTaken(damage);
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
        if (!GameManagement.shieldActive)
            currentHealth -= amount;
        Debug.Log($"current hp is {currentHealth}");

        if (currentHealth <= 0)
        {
            PlayerDeath();
        }
    }

    private void PlayerDeath()
    {
        Debug.Log("player out of health");
    }
}
