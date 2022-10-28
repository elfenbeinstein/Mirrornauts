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
    [SerializeField] private Spawner _hazardSpawner;
    [SerializeField] private Spawner _collectiblesSpawner;

    private List<ObjectBehaviour> spawnsToAdd;
    private List<ObjectBehaviour> spawnsToDelete;

    [SerializeField]
    private Stats _stats;

    private void Start()
    {
        _interfaceManager = GetComponent<InterfaceManager>();

        if (!_interfaceManager.freeFlowMode)
        {
            turnCounter = 0; // get from somewhere maybe
            _interfaceManager.UpdateTurnCounterDisplay(turnCounter);
        }
        spawnsToAdd = new List<ObjectBehaviour>();
        spawnsToDelete = new List<ObjectBehaviour>();

    }
    public void Go()
    {
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
                activeSpawns.Remove(item);
            }
        }

        // go through all active spawns and check for Collisions
        for (int i = activeSpawns.Count - 1; i >= 0; i--)
            if (activeSpawns[i].isTouching) EventManager.Instance.EventGo("PLAYER", "HitObject", activeSpawns[i]);
        // elfenbeinstein maybe CHANGE this to just send the message once
    }

    public void AddSpawn(ObjectBehaviour spawn)
    {
        spawnsToAdd.Add(spawn);
    }

    public void RemoveSpawn(ObjectBehaviour spawn)
    {
        spawnsToDelete.Add(spawn);
    }
}
