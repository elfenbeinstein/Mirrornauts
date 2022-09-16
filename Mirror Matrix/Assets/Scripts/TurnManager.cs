using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public int turnCounter;

    [SerializeField]
    private List<ObjectBehaviour> activeSpawns;

    [SerializeField] private InterfaceManager _interfaceManager;

    public void Go()
    {
        // calculate new space ship position
        //_interfaceManager.GoCalculation();
        // turn off spaceship collider or gameObject

        // spawn + update hazards + collectibles
        foreach (ObjectBehaviour spawn in activeSpawns)
        {
            if (spawn.gameObject.activeInHierarchy)
            {
                spawn.NextTurn(turnCounter);
            }
        }

        // turn on and move spaceship

        // check for collision

        // update turn counter
    }

    public void AddSpawn(ObjectBehaviour spawn)
    {
        activeSpawns.Add(spawn);
    }

    public void RemoveSpawn(ObjectBehaviour spawn)
    {
        activeSpawns.Remove(spawn);
    }
}
