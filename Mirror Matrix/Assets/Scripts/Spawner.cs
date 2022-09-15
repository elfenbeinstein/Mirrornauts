using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Spawn
{
    public string name;
    public GameObject unit;
    public int round;
    public int countdown;
    public int liftoff;
    public Vector3 spawnPosition;
    public Vector3 rotation;
}

public class Spawner : MonoBehaviour
{
    public Spawn[] spawnUnits;

    // keep List of active spawns so that I can access them later, maybe in turn amanager?

    public void Spawn()
    {
        // check current round

        // check if a new spawn needs to be made

        // turn on new game object + give rotation, position + tell it its stuff
    }
}
