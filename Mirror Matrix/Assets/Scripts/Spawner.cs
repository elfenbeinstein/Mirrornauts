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

    public void Spawn()
    {
        // check current round

        // check if a new spawn needs to be made

        // turn on new game object + make it set up (send to set up in object behaviour script)
    }
}
