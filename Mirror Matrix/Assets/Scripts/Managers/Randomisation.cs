using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomisation : MonoBehaviour
{
    private TurnManager _turnManager;
    private List<float> angles;

    [SerializeField] private bool randomiseSpawns;
    [SerializeField] private Transform spawnParent;

    [SerializeField] private bool randomiseSpaceship;
    [SerializeField] private List<Vector3> spaceshipStartPositions;
    [SerializeField] private SpaceshipBehaviour _spaceshipBehaviour;


    void Start()
    {
        _turnManager = GetComponent<TurnManager>();

        angles = new List<float>();
        angles.Add(0f);
        angles.Add(90f);
        angles.Add(180f);
        angles.Add(270f);

        Randomise();
    }

    private void Randomise()
    {
        if (randomiseSpawns)
        {
            int random = Random.Range(0, 4);

            spawnParent.Rotate(new Vector3(0, 0, angles[random]));

            _turnManager.randomAngle = -1 * angles[random];
        }
        
        if (randomiseSpaceship && spaceshipStartPositions.Count != 0)
        {
            int random = Random.Range(0, spaceshipStartPositions.Count);

            float[] vector = new float[] { spaceshipStartPositions[random].x, spaceshipStartPositions[random].y };
            _spaceshipBehaviour.MoveSpaceship(vector);
        }
    }
}
