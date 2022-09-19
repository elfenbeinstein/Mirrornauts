using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public int turnCounter;

    [SerializeField]
    private List<ObjectBehaviour> activeSpawns;

    [SerializeField] private InterfaceManager _interfaceManager;
    [SerializeField] private Spawner _spawner;

    private List<ObjectBehaviour> spawnsToAdd;
    private List<ObjectBehaviour> spawnsToDelete;

    public void Go()
    {
        spawnsToAdd = new List<ObjectBehaviour>();
        spawnsToDelete = new List<ObjectBehaviour>();
        spawnsToAdd.Clear();
        spawnsToDelete.Clear();

        turnCounter += 1;

        // turn off spaceship collider or gameObject
        _interfaceManager.SpaceshipCollider(false);

        // move spaceship
        _interfaceManager.MoveSpaceship();

        // tell spawner to spawn new hazards
        _spawner.Spawn(turnCounter);

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

        // turn on spaceship collider
        _interfaceManager.SpaceshipCollider(true);

        // check for collision

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
