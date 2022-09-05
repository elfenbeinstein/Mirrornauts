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
    [SerializeField] private GameObject spaceshipTop;

    // get arrowhead as well?

    private void Start()
    {
        startVObject.SetActive(false);
        endVObject.SetActive(false);
    }
    public void UpdateDisplayFreeFlow(float[] startVector, float[] endVector)
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

    public void UpdateDisplay(float[] startVector, float[] vectorResult, float[] newTop)
    {
        // get original rotation
        // calculate new rotation based on relation vectorResult and new top

        // optional: calculate if new distance vectorResult and new top is stretched/squished

        // move spaceship
        spaceship.transform.position = new Vector3(vectorResult[0], vectorResult[1], 0);
        
        // rotate based on new position

        // obsolete?
        startVObject.SetActive(true);
        endVObject.SetActive(true);
        var position = new Vector3(startVector[0], startVector[1], 0);
        startV.SetPosition(1, position);
        position = new Vector3(vectorResult[0], vectorResult[1], 0);
        endV.SetPosition(1, position);
    }

    public float[] ShipTopCoordinates()
    {
        float x, y;
        x = spaceshipTop.transform.position.x;
        y = spaceshipTop.transform.position.y;

        float[] positionTop = new float[] { x, y };

        return positionTop;
    }
}
