using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayResults : MonoBehaviour
{
    [SerializeField] private GameObject startVObject;
    [SerializeField] private LineRenderer startV;
    [SerializeField] private GameObject endVObject;
    [SerializeField] private LineRenderer endV;

    [SerializeField] private GameObject spaceship;

    // get arrowhead as well?

    private void Start()
    {
        startVObject.SetActive(false);
        endVObject.SetActive(false);
    }
    public void UpdateDisplay(float[] startVector, float[] endVector)
    {
        // using line renderer:
        startVObject.SetActive(true);
        endVObject.SetActive(true);

        var position = new Vector3(startVector[0], startVector[1], 0);
        startV.SetPosition(1, position);

        position = new Vector3(endVector[0], endVector[1], 0);
        endV.SetPosition(1, position);
    }

    public void UpdateSpaceshipStartV(float[] vector)
    {
        spaceship.transform.position = new Vector3(vector[0], vector[1], 0);
    }
}
