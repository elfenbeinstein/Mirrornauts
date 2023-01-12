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

    private void Start()
    {
        _interfaceManager = GetComponent<InterfaceManager>();

        if (!_interfaceManager.freeFlowMode)
        {
            turnCounter = 0;
            _interfaceManager.UpdateTurnCounterDisplay(turnCounter);
        }
        spawnsToAdd = new List<ObjectBehaviour>();
        spawnsToDelete = new List<ObjectBehaviour>();

    }
    public void Go()
    {
        if(!_interfaceManager.freeFlowMode)
        {
            // make sure that all fields where set before we start;
            if (!_interfaceManager.GameIsReady())
            {
                // if energyneeded = 1000 --> wrong type
                if (GameManagement._playerStats.energyNeeded == 1000)
                    EventManager.Instance.EventGo("ERROR", "Error", 6);
                // else values not set
                else
                    EventManager.Instance.EventGo("ERROR", "Error", 7);
                GameManagement._audioManager._sfxSounds.PlayError();
                return;
            }
            

            // make sure player has enough energy
            if (GameManagement._playerStats.energyNeeded > GameManagement._playerStats.energy)
            {
                EventManager.Instance.EventGo("ERROR", "Error", 5);
                GameManagement._audioManager._sfxSounds.PlayError();
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

        GameManagement._audioManager._sfxSounds.PlayGo();
    }

    public void Spawn()
    {
        if (spawner != null) spawner.Spawn(turnCounter);
        else Debug.LogWarning("spawner is null");
    }

    public void UpdateSpawns()
    {
        //int x = 0;
        // update hazards + collectibles
        foreach (ObjectBehaviour spawn in activeSpawns)
        {
            if (spawn.gameObject != null)
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

        // go through all active spawns and check for Collisions
        for (int i = activeSpawns.Count - 1; i >= 0; i--)
        {
            if (activeSpawns[i].isTouching)
            {
                if (activeSpawns[i].isHazard) sndHzd = true;
                else EventManager.Instance.EventGo("PLAYER", "HitObject", activeSpawns[i]);
            }
        }
        if (sndHzd) EventManager.Instance.EventGo("PLAYER", "HitHazard", 1);

        EventManager.Instance.EventGo("ENERGY", "RemoveCost");
        GameManagement._playerStats.NextTurn();
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
}
