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

    public void AddToPlayer(Stats _stats)
    {
        switch (type)
        {
            case PowerupType.Energy:
                //if (_stats.maxEnergy < GameManagement._playerStats.energy) GameManagement._playerStats.energy = _stats.maxEnergy;
                EventManager.Instance.EventGo("ENERGY", "AddEnergy", amount);
                GameManagement._audioManager._sfxSounds.PlayEnergy();
                break;
            case PowerupType.Shield:
                EventManager.Instance.EventGo("TURN", "Shield");
                GameManagement._audioManager._sfxSounds.PlayShield();
                break;
        }
    }
}
