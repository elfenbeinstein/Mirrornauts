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
    public GameObject _spaceshipCollider;
    private Vector3 topPos;

    [SerializeField] private float scaleMultiplier = 1f;

    private Maths _maths;

    private void Start()
    {
        startVObject.SetActive(false);
        endVObject.SetActive(false);
        if (scaleMultiplier == 0)
        {
            scaleMultiplier = 1;
        }

        _maths = FindObjectOfType<Maths>();
        if (_maths == null)
        {
            Debug.LogWarning(gameObject + " can't find maths script");
        }

        /*
        float value = 1 / Mathf.Sqrt(2);
        float result = Mathf.Asin(value);
        result = _maths.ConvertFromRadian(result);
        Debug.Log("result is " + result);*/
    }
    public void UpdateDisplayFreeFlow(float[] startVector, float[] endVector)
    {
        // using line renderer:
        startVObject.SetActive(true);
        endVObject.SetActive(true);

        var position = new Vector3(startVector[0] * scaleMultiplier, startVector[1] * scaleMultiplier, 0);
        startV.SetPosition(1, position);

        position = new Vector3(endVector[0] * scaleMultiplier, endVector[1] * scaleMultiplier, 0);
        endV.SetPosition(1, position);
    }

    public void UpdateSpaceshipStartV(float[] vector)
    {
        spaceship.transform.position = new Vector3(vector[0] * scaleMultiplier, vector[1] * scaleMultiplier, 0);
    }

    public void UpdateDisplay(float[] startVector, float[] vectorResult, float[] newTop)
    {
        // move spaceship
        spaceship.transform.position = new Vector3(vectorResult[0] * scaleMultiplier, vectorResult[1] * scaleMultiplier, 0);

        // rotate based on new position
        topPos = new Vector3(newTop[0], newTop[1], 0);
        float rotation = _maths.CalculateRotation(topPos, spaceship.transform.position);

        spaceship.transform.eulerAngles = new Vector3(0, 0, rotation);

        // scale based on calculation
        float scale = _maths.CalculateDistance(topPos, spaceship.transform.position);
        spaceship.transform.localScale = new Vector3(scale, scale, scale);


        if (topPos != spaceshipTop.transform.position)
        {
            Debug.LogWarning("incorrect rotation/scaling");
            Debug.Log("new top pos should be: " + newTop[0] * scaleMultiplier + ", " + newTop[1] * scaleMultiplier);
            Debug.Log("new top pos is: " + spaceshipTop.transform.position.x + ", " + spaceshipTop.transform.position.y);
        }


        // obsolete?
        startVObject.SetActive(true);
        endVObject.SetActive(true);
        var position = new Vector3(startVector[0] * scaleMultiplier, startVector[1] * scaleMultiplier, 0);
        startV.SetPosition(1, position);
        position = new Vector3(vectorResult[0] * scaleMultiplier, vectorResult[1] * scaleMultiplier, 0);
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

    public void ResetRotation()
    {
        spaceship.transform.rotation = Quaternion.identity;
        spaceship.transform.localScale = new Vector3(1, 1, 1);
    }
}
