using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public int turnCounter;

    [SerializeField]
    private List<ObjectBehaviour> activeSpawns;

    private InterfaceManager _interfaceManager;
    [SerializeField] private Spawner _hazardSpawner;
    [SerializeField] private Spawner _collectiblesSpawner;
    [SerializeField] private Player _player;

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
        _interfaceManager.UpdateTurnCounterDisplay(turnCounter); // delete once game mode possible 
        if (!_interfaceManager.freeFlowMode)
        {
            _interfaceManager.UpdateTurnCounterDisplay(turnCounter);
        }

        // get values + move spaceship
        _interfaceManager.CollectValues();
        _interfaceManager.CalculateAndMove();

        // tell spawner to spawn new hazards
        _hazardSpawner.Spawn(turnCounter);
        _collectiblesSpawner.Spawn(turnCounter);

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
            if (activeSpawns[i].isTouching) _player.PlayerHitObject(activeSpawns[i]);
        /* -- old way
        foreach (ObjectBehaviour spawn in activeSpawns)
        {
            if (spawn.isTouching == true)
                _player.PlayerHitObject(spawn);
        }*/

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
