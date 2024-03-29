using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Randomises hazards and spaceship position at the start of the game
/// hazards --> random rotation by 90 degree steps, all hazards are rotated by the same angle
/// spaceship --> random quadrant of start position + variance from that position
/// </summary>

public class Randomisation : MonoBehaviour
{
    private TurnManager _turnManager;
    private List<float> angles;

    [SerializeField] private bool randomiseSpawns;
    [SerializeField] private Transform spawnParent;

    [SerializeField] private bool randomiseSpaceship;
    [SerializeField] private List<Vector3> spaceshipStartPositions;
    [Tooltip("amount of variance from start positions: so if x in a start position is 3 and dispersion is 1, spaceship is spawned between 2 and 4")]
    [SerializeField] private float randomDispersion = 0.4f;
    [SerializeField] private SpaceshipBehaviour _spaceshipBehaviour;

    [SerializeField] private InputGameValues gameValues;

    //[SerializeField] private float testAngle;


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

            float[] vector = new float[] { spaceshipStartPositions[random].x + Random.Range(-randomDispersion, randomDispersion), spaceshipStartPositions[random].y + Random.Range(-randomDispersion, randomDispersion) };
            _spaceshipBehaviour.MoveSpaceship(vector);
            _spaceshipBehaviour.DrawStartLine();
            gameValues.WriteNewSpaceshipPos(vector[0], vector[1]);
        }
    }
}
