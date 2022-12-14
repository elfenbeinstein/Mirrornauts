using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Spawn
{
    public string name;
    [Tooltip("if is prefab --> keine rotation und position eingeben, sondern ist ein objekt in der szene")]
    public bool isPrefab;
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
/// (e.g. can have spawn manager objects for collectibles and hazards using the same script
/// </summary>

public class Spawner : MonoBehaviour
{
    public Spawn[] spawnUnits;

    [SerializeField] private TurnManager _turnManager;

    private void Start()
    {
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
        if (_turnManager == null)
        {
            _turnManager = FindObjectOfType<TurnManager>();
            Debug.LogWarning($"{gameObject} is missing reference to the TurnManager script");
        }

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
                    if (spawnUnits[i].isPrefab)
                    {
                        // if it is a prefab --> set up with position and rotation according to what is in the spawnUnits:
                        clone.GetComponent<ObjectBehaviour>().SetUpNewSpawn(spawnUnits[i].round, spawnUnits[i].countdown, spawnUnits[i].liftoff, spawnUnits[i].position, spawnUnits[i].rotation, _turnManager);
                    }
                    else
                    {
                        // if it is not a prefab --> set up with values that are already in game Object and set active
                        clone.SetActive(true);
                        Vector3 rot = new Vector3(clone.transform.rotation.x, clone.transform.rotation.y, clone.transform.rotation.z);
                        clone.GetComponent<ObjectBehaviour>().SetUpNewSpawn(spawnUnits[i].round, spawnUnits[i].countdown, spawnUnits[i].liftoff, clone.transform.position, rot, _turnManager);
                    }
                    _turnManager.AddSpawn(clone.GetComponent<ObjectBehaviour>());
                }
                else
                {
                    Debug.LogWarning("unit could not be spawned, missing game object to clone");
                }
            }
        }

    }
}
