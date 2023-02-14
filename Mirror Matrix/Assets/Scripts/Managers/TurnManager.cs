using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// central hub for managing each turn so things get done in the right order
/// 1) send message for new turn (spawner spawns, interfacemanager collects values, calculates and moves spaceship)
/// 2) holds reference to all active spawns
/// 3) checks if active spawns are touching player and then sends message if so
/// </summary>
public class TurnManager : MonoBehaviour
{
    public int turnCounter;

    [SerializeField]
    private List<ObjectBehaviour> activeSpawns;

    private InterfaceManager _interfaceManager;
    [SerializeField] private Spawner spawner;

    private List<ObjectBehaviour> spawnsToAdd;
    private List<ObjectBehaviour> spawnsToDelete;

    private PlayerStats _playerStats;

    public float randomAngle;

    private bool playerWin;

    public int startGameAt = 0;

    private void Start()
    {
        _interfaceManager = GetComponent<InterfaceManager>();
        _playerStats = _interfaceManager._playerStats;

        if (!_interfaceManager.freeFlowMode)
        {
            turnCounter = startGameAt;
            _interfaceManager.UpdateTurnCounterDisplay(turnCounter);
            _playerStats = _interfaceManager._playerStats;
        }
        spawnsToAdd = new List<ObjectBehaviour>();
        spawnsToDelete = new List<ObjectBehaviour>();

        playerWin = false;

    }
    public void Go()
    {
        if(!_interfaceManager.freeFlowMode)
        {
            // make sure that all fields where set before we start;
            if (!_interfaceManager.GameIsReady())
            {
                // if energyneeded = 1000 --> wrong type
                if (_playerStats.energyNeeded == 1000)
                    EventManager.Instance.EventGo("ERROR", "Error", 6);
                // else values not set
                else
                    EventManager.Instance.EventGo("ERROR", "Error", 7);
                EventManager.Instance.EventGo("AUDIO", "PlayError");
                //GameManagement._audioManager._sfxSounds.PlayError();
                return;
            }
            

            // make sure player has enough energy
            if (_playerStats.energyNeeded > _playerStats.energy)
            {
                EventManager.Instance.EventGo("ERROR", "Error", 5);
                EventManager.Instance.EventGo("AUDIO", "PlayError");
                //GameManagement._audioManager._sfxSounds.PlayError();
                return;
            }
        }

        spawnsToAdd.Clear();
        spawnsToDelete.Clear();
        turnCounter += 1;

        // interface manager collects values, calculates movement, moves spaceship
        _interfaceManager.NextTurn(turnCounter);

        // Send Event next Turn to all spawners etc:
        EventManager.Instance.EventGo("TURN", "NextTurn", turnCounter);
        // Listeners should be: spawner
        // spawner: adds new spawns to list spawnsToAdd

        // wait for interface manager to update spawns

        EventManager.Instance.EventGo("AUDIO", "PlayGo");
        //GameManagement._audioManager._sfxSounds.PlayGo();
    }

    public void Spawn()
    {
        if (playerWin) return;
        if (spawner != null) spawner.Spawn(turnCounter);
        else Debug.LogWarning("spawner is null");
    }

    public void UpdateSpawns()
    {
        //int x = 0;
        // update hazards + collectibles
        foreach (ObjectBehaviour spawn in activeSpawns)
        {
            if (spawn != null)
            {
                spawn.NextTurn(turnCounter);
            }
        }

        // add and remove all to list
        if (spawnsToAdd.Count != 0)
        {
            foreach (ObjectBehaviour item in spawnsToAdd)
            {
                activeSpawns.Add(item);
            }
        }
        if (spawnsToDelete.Count != 0)
        {
            foreach (ObjectBehaviour item in spawnsToDelete)
            {
                if (activeSpawns.Contains(item))
                    activeSpawns.Remove(item);
            }
        }

        bool sndHzd = false; // send hazard message;

        bool collectsShield = false;

        // go through all active spawns and check for Collisions
        for (int i = activeSpawns.Count - 1; i >= 0; i--)
        {
            if (activeSpawns[i].isTouching)
            {
                if (activeSpawns[i].isHazard) sndHzd = true;
                else EventManager.Instance.EventGo("PLAYER", "HitObject", activeSpawns[i]);

                if (!activeSpawns[i].isHazard && activeSpawns[i].gameObject.GetComponent<PowerUps>()!= null && activeSpawns[i].gameObject.GetComponent<PowerUps>().IsShield()) collectsShield = true;
            }
        }
        if (sndHzd && !collectsShield) EventManager.Instance.EventGo("PLAYER", "HitHazard", 1);

        if (spawnsToDelete.Count != 0)
        {
            foreach (ObjectBehaviour item in spawnsToDelete)
            {
                if (activeSpawns.Contains(item))
                    activeSpawns.Remove(item);
            }
        }

        // removes the énergy cost text underneath the slider
        EventManager.Instance.EventGo("ENERGY", "RemoveCost");
        _playerStats.NextTurn();
    }

    public void AddSpawn(ObjectBehaviour spawn)
    {
        spawnsToAdd.Add(spawn);
    }

    public void RemoveSpawn(ObjectBehaviour spawn)
    {
        if (!spawnsToDelete.Contains(spawn))
            spawnsToDelete.Add(spawn);
    }

    public void PlayerWin()
    {
        playerWin = true;
    }
}
