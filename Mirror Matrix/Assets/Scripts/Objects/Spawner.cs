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
    public Vector3 position;
    public Vector3 rotation;
}

/// <summary>
/// sits on an empty game object called spawn manager
/// can spawn different kinds of objects 
/// (e.g. can have spawn managers for collectibles and hazards in the same script
/// </summary>

public class Spawner : MonoBehaviour
{
    public Spawn[] spawnUnits;

    //private int arrayCounter;
    [SerializeField] private TurnManager _turnManager;

    private void Start()
    {
        //arrayCounter = 0;
        
        if (_turnManager == null)
        {
            _turnManager = FindObjectOfType<TurnManager>();
            Debug.LogWarning($"{gameObject} is missing reference to the TurnManager script");
        }

        EventManager.Instance.AddEventListener("TURN", TurnListener);
    }

    private void OnDestroy()
    {
        EventManager.Instance.RemoveEventListener("TURN", TurnListener);
    }

    private void TurnListener(string eventName, object param)
    {
        if (eventName == "NextTurn")
            Spawn((int)param);
    }

    public void Spawn(int currentRound)
    {
        // bool spawned = false;
        for (int i = 0; i < spawnUnits.Length; i++)
        {
            // check if a new spawn needs to be made
            if (spawnUnits[i].round - spawnUnits[i].countdown == currentRound)
            {
                // turn on new game object + make it set up (send to set up in object behaviour script)
                if (spawnUnits[i].unit != null)
                {
                    GameObject clone = Instantiate(spawnUnits[i].unit);
                    clone.GetComponent<ObjectBehaviour>().SetUpNewSpawn(spawnUnits[i].round, spawnUnits[i].countdown, spawnUnits[i].liftoff, spawnUnits[i].position, spawnUnits[i].rotation, _turnManager);
                    _turnManager.AddSpawn(clone.GetComponent<ObjectBehaviour>());
                    //spawned = true;
                }
                else
                {
                    Debug.LogWarning("unit could not be spawned, missing game object to clone");
                }
            }

            /*
            // if spawn instantiated --> arrayCounter = i
            if (spawned)
            {
                arrayCounter = i;
            }
            */
        }

    }
}
