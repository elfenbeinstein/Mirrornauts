using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// handles behaviour for collectible powerups
/// </summary>

public enum PowerupType
{
    Dash,
    Shield,
    Energy
}


public class PowerUps : MonoBehaviour
{
    [Tooltip("what kind of power up is this object?")]
    [SerializeField] PowerupType type;
    [Tooltip("How much does this add to the player (amount of powerups or energy added)")]
    [SerializeField] int amount;

    public void AddToPlayer()
    {
        switch (type)
        {
            case PowerupType.Dash:
                GameManagement.dashAmount += amount;
                break;
            case PowerupType.Energy:
                GameManagement.energy += amount;
                // elfenbeinstein MISSING: check for max amounts
                break;
            case PowerupType.Shield:
                GameManagement.shieldAmount += amount;
                break;
        }
    }
}
