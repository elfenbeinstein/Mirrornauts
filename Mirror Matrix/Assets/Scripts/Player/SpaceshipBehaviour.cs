using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject startVObject;
    [SerializeField] private LineRenderer startV;
    [SerializeField] private GameObject endVObject;
    [SerializeField] private LineRenderer endV;

    [SerializeField] private GameObject spaceship;
    [SerializeField] private GameObject spaceshipTop;
    //public GameObject _spaceshipCollider;
    
    private Vector3 topPos;

    private InterfaceManager _interfaceManager;

    private void Start()
    {
        startVObject.SetActive(false);
        endVObject.SetActive(false);

        _interfaceManager = FindObjectOfType<InterfaceManager>();

        /*
        float value = 1 / Mathf.Sqrt(2);
        float result = Mathf.Asin(value);
        result = GameManagement._maths.ConvertFromRadian(result);
        Debug.Log("result is " + result);*/
    }

    /*
    public void UpdateDisplayFreeFlow(float[] startVector, float[] endVector)
    {
        // using line renderer:
        startVObject.SetActive(true);
        endVObject.SetActive(true);

        var position = new Vector3(startVector[0] * scaleMultiplier, startVector[1] * scaleMultiplier, 0);
        startV.SetPosition(1, position);

        position = new Vector3(endVector[0] * scaleMultiplier, endVector[1] * scaleMultiplier, 0);
        endV.SetPosition(1, position);
    }*/

    public void MoveSpaceship(float[] vector)
    {
        spaceship.transform.position = new Vector3(vector[0], vector[1], 0);
    }

    public void UpdateSpaceshipFF(float[] startVector, float[] vectorResult, float[] newTop)
    {
        // move spaceship
        MoveSpaceship(vectorResult);

        // rotate based on new position
        topPos = new Vector3(newTop[0], newTop[1], 0);
        Vector3 shipPos = new Vector3(vectorResult[0], vectorResult[1], 0);
        float rotation = GameManagement._maths.CalculateRotation(topPos, shipPos);

        spaceship.transform.eulerAngles = new Vector3(0, 0, rotation);

        // scale based on calculation
        float scale = GameManagement._maths.CalculateDistance(topPos, shipPos);
        spaceship.transform.localScale = new Vector3(scale, scale, scale); 

        /*
        if (topPos != spaceshipTop.transform.position)
        {
            Debug.LogWarning("incorrect rotation/scaling");
            Debug.Log("new top pos should be: " + newTop[0] + ", " + newTop[1]);
            Debug.Log("new top pos is: " + spaceshipTop.transform.position.x + ", " + spaceshipTop.transform.position.y);
        }*/

        
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

    public float[] SpaceshipCoordinates()
    {
        float x, y;
        x = spaceship.transform.position.x;
        y = spaceship.transform.position.y;

        float[] position = new float[] { x, y };

        return position;
    }

    public void ResetRotation()
    {
        spaceship.transform.rotation = Quaternion.identity;
        spaceship.transform.localScale = new Vector3(1, 1, 1);
    }

    public void ResetSpaceshipFromBorder()
    {
        float[] value = new float[] { 0, 0 };
        MoveSpaceship(value);
    }
}
