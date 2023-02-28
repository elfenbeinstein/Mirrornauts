using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// handles behaviour for collectible powerups
/// </summary>

public enum PowerupType
{
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
            case PowerupType.Energy:
                EventManager.Instance.EventGo("ENERGY", "AddEnergy", amount);
                EventManager.Instance.EventGo("AUDIO", "PlayEnergy");
                break;
            case PowerupType.Shield:
                EventManager.Instance.EventGo("TURN", "Shield");
                EventManager.Instance.EventGo("AUDIO", "PlayShield");
                break;
        }
    }

    public bool IsShield()
    {
        if (type == PowerupType.Shield) return true;
        else return false;
    }
}
