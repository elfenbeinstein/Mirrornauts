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
    [SerializeField] private float maxEnergy;
    [SerializeField] private SpaceshipBehaviour _spaceshipBehaviour;
    private PlayerStats _playerStats;


    void Start()
    {
        EventManager.Instance.AddEventListener("PLAYER", PlayerListener);
        _playerStats = GetComponent<PlayerStats>();
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
        Debug.Log("spaceship touched object");
        if (hitObject.gameObject.GetComponent<PowerUps>() != null) hitObject.gameObject.GetComponent<PowerUps>().AddToPlayer();
        else Debug.Log(hitObject + " is missing powerup script");
        hitObject.RemoveSelfFromList();
    }

    public void PlayerHitHazard(int damage)
    {
        DamageTaken(damage);
    }

    public void PlayerHitBorder()
    {
        /*
        Debug.Log("spaceship touched border");
        DamageTaken(1);
        */
    }

    public void PlayerOutOfBounds()
    {
        Debug.Log("spaceship out of bounds");

        if (GameManagement.gameMode)
        {
            PlayerDeath();
        }
        else
        {
            _spaceshipBehaviour.ResetSpaceshipFromBorder();
        }
    }

    private void DamageTaken(int amount)
    {
        if (!_playerStats.shieldActive)
            _playerStats.currentHealth -= amount;
        Debug.Log($"current hp is {_playerStats.currentHealth}");

        if (_playerStats.currentHealth <= 0)
        {
            PlayerDeath();
        }
    }

    private void PlayerDeath()
    {
        //Debug.Log("player out of health");

        // count death
        EventManager.Instance.EventGo("DATA", "Death");
        // play audio
        EventManager.Instance.EventGo("AUDIO", "PlayDeath");
        // get error message
        EventManager.Instance.EventGo("ERROR", "Death", 3);
        //play explosion animation
        _spaceshipBehaviour.ExplosionAnimation();
    }
}
