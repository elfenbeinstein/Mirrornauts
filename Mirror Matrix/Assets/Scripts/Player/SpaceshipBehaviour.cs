using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipBehaviour : MonoBehaviour
{
    [Tooltip("Line Renderer Object for start vector / see environment -> line renderer")]
    [SerializeField] private GameObject startVObject;
    private LineRenderer startV;
    [Tooltip("Line Renderer Object for end vector / see environment -> line renderer")]
    [SerializeField] private GameObject endVObject;
    private LineRenderer endV;

    [SerializeField] private GameObject spaceship;
    [SerializeField] private GameObject spaceshipTop;
    [SerializeField] private GameObject spaceshipRight;

    [SerializeField] private bool scales;
    
    private Vector3 topPos;
    private Vector3 rightPos;

    private void Start()
    {
        startV = startVObject.GetComponent<LineRenderer>();
        endV = endVObject.GetComponent<LineRenderer>();

        startVObject.SetActive(false);
        endVObject.SetActive(false);
    }

    public void MoveSpaceship(float[] vector)
    {
        spaceship.transform.position = new Vector3(vector[0], vector[1], 0);
    }

    public void UpdateSpaceshipFF(float[] startVector, float[] vectorResult, float[] newTop, float[] newRight)
    {
        // move spaceship
        MoveSpaceship(vectorResult);

        // rotate based on new position
        topPos = new Vector3(newTop[0], newTop[1], 0);
        Vector3 shipPos = new Vector3(vectorResult[0], vectorResult[1], 0);
        float rotation = GameManagement._maths.CalculateRotation(topPos, shipPos);
        spaceship.transform.eulerAngles = new Vector3(0, 0, rotation);

        if (scales)
        {
            // scale based on calculation
            rightPos = new Vector3(newRight[0], newRight[1], 0);
            float scaleY = Vector3.Distance(topPos, shipPos);
            float scaleX = GameManagement._maths.CalculateScaleX(rightPos, topPos);
            spaceship.transform.localScale = new Vector3(scaleX, scaleY, 1);

            /*
            Debug.Log($"new spaceshipPosition = {spaceship.transform.position.x}, {spaceship.transform.position.y}");
            Debug.Log($"new topPos should be = {topPos.x}, {topPos.y} with scaleY = {scaleY}");
            Debug.Log($"Right Pos should be: {rightPos.x}, {rightPos.y} with scaleX = {scaleX}");
            */
        }

        // line renderer update:
        startVObject.SetActive(true);
        endVObject.SetActive(true);
        var position = new Vector3(startVector[0], startVector[1], 0);
        startV.SetPosition(1, position);
        position = new Vector3(vectorResult[0], vectorResult[1], 0);
        endV.SetPosition(1, position);
    }

    public void UpdateSpaceshipG(float[] vectorResult, float[] newTop, float[] newRight)
    {
        // move spaceship
        MoveSpaceship(vectorResult);

        // rotate based on new position
        topPos = new Vector3(newTop[0], newTop[1], 0);
        Vector3 shipPos = new Vector3(vectorResult[0], vectorResult[1], 0);
        float rotation = GameManagement._maths.CalculateRotation(topPos, shipPos);
        spaceship.transform.eulerAngles = new Vector3(0, 0, rotation);

        if (scales)
        {
            // scale based on calculation
            rightPos = new Vector3(newRight[0], newRight[1], 0);
            float scaleY = Vector3.Distance(topPos, shipPos);
            float scaleX = GameManagement._maths.CalculateScaleX(rightPos, topPos);
            spaceship.transform.localScale = new Vector3(scaleX, scaleY, 1);

            /*
            Debug.Log($"new spaceshipPosition = {spaceship.transform.position.x}, {spaceship.transform.position.y}");
            Debug.Log($"new topPos should be = {topPos.x}, {topPos.y} with scaleY = {scaleY}");
            Debug.Log($"Right Pos should be: {rightPos.x}, {rightPos.y} with scaleX = {scaleX}");
            */
        }

        // line renderer update:
        endVObject.SetActive(true);
        var position = new Vector3(vectorResult[0], vectorResult[1], 0);
        endV.SetPosition(1, position);
    }

    [ContextMenu("test position values")]
    public void TestPositions()
    {
        float[] topPos = ShipTopCoordinates();
        float[] rightPos = ShipRightCoordinates();

        Debug.Log($"top pos = {topPos[0]}, {topPos[1]}");
        Debug.Log($"right pos = {rightPos[0]}, {rightPos[1]}");
    }

    public float[] ShipTopCoordinates()
    {
        float x, y;
        x = spaceshipTop.transform.position.x;
        y = spaceshipTop.transform.position.y;

        float[] positionTop = new float[] { x, y };

        return positionTop;
    }

    public float[] ShipRightCoordinates()
    {
        float x, y;
        x = spaceshipRight.transform.position.x;
        y = spaceshipRight.transform.position.y;

        float[] position = new float[] { x, y };
        return position;
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

    public void RemoveLines()
    {
        startVObject.SetActive(false);
        endVObject.SetActive(false);
    }

    /* -- obsolete, first version without spaceship object
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
    }
