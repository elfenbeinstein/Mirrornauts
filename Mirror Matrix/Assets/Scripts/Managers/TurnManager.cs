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
        // make sure that all fields where set before we start
        if (!_interfaceManager.GameIsReady()) return;

        spawnsToAdd.Clear();
        spawnsToDelete.Clear();
        turnCounter += 1;

        // Send Event next Turn:
        EventManager.Instance.EventGo("TURN", "NextTurn", turnCounter);
        // Listeners should be: spawner, interface manager 
        // spawner: adds new spawns to list spawnsToAdd
        // interface manager collects values, calculates movement, moves spaceship

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
                if(activeSpawns.Contains(item))
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
        if(sndHzd) EventManager.Instance.EventGo("PLAYER", "HitHazard", 1);
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
