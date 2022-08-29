using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Display : MonoBehaviour
{
    [SerializeField] private GameObject startVObject;
    [SerializeField] private LineRenderer startV;
    [SerializeField] private GameObject endVObject;
    [SerializeField] private LineRenderer endV;

    // get arrowhead as well?

    private void Start()
    {
        startVObject.SetActive(false);
        endVObject.SetActive(false);
    }
    public void UpdateDisplay(float[] startVector, float[] endVector)
    {
        //Debug.Log("updating display");
        startVObject.SetActive(true);
        endVObject.SetActive(true);

        var position = new Vector3(startVector[0], startVector[1], 0);
        startV.SetPosition(1, position);

        position = new Vector3(endVector[0], endVector[1], 0);
        endV.SetPosition(1, position);
    }
}
