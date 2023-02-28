using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Moves The Spaceship After Calculation
/// Uses Line Renderer To Paint Line To Vector
/// </summary>

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
    [SerializeField] private GameObject shield;
    [SerializeField] private GameObject explosion;
    [SerializeField] private Animator animShield;
    [SerializeField] private InterfaceManager _interface;

    private bool scales;
    
    private Vector3 topPos;
    private Vector3 rightPos;
    private Rigidbody2D rb;

    private bool watchForEnd;
    private Vector3 endPosition;

    private Maths _maths;

    [SerializeField] private GameObject warningText;
    [SerializeField] private Animator anim;
    float[] result;

    private void Start()
    {
        scales = false;
        shield.SetActive(false);

        EventManager.Instance.AddEventListener("SHIELD", ShieldListener);

        _maths = _interface.GetComponent<Maths>();
        DrawStartLine();
    }

    public void DrawStartLine()
    {
        startV = startVObject.GetComponent<LineRenderer>();
        endV = endVObject.GetComponent<LineRenderer>();
        rb = GetComponentInChildren<Rigidbody2D>();

        startVObject.SetActive(false);
        endVObject.SetActive(true);
        endV.SetPosition(1, spaceship.transform.position);
    }

    private void OnDestroy()
    {
        EventManager.Instance.RemoveEventListener("SHIELD", ShieldListener);
    }

    private void Update()
    {
        if (watchForEnd)
        {
            if (gameObject.transform.position == endPosition)
            {
                watchForEnd = false;
                _interface.ContinueAfterMove();
            }
        }
    }

    private void ShieldListener(string eventName, object param)
    {
        if (eventName == "Start")
        {
            shield.SetActive(true);
            animShield.SetBool("Active", true);
        }
        else if (eventName == "Stop")
        {
            shield.SetActive(false);
            animShield.SetBool("Active", false);
        }
    }

    public void MoveSpaceship(float[] vector)
    {
        if (warningText != null) warningText.SetActive(false);
        spaceship.transform.position = new Vector3(vector[0], vector[1], 0);
    }

    public void UpdateLineRenderer(float[] startVector)
    {
        startVObject.SetActive(true);
        endVObject.SetActive(false);

        var position = new Vector3(startVector[0], startVector[1], 0);
        startV.SetPosition(1, position);
    }

    public void UpdateSpaceshipFF(float[] startVector, float[] vectorResult, float[] newTop, float[] newRight)
    {
        // move spaceship
        MoveSpaceship(vectorResult);

        if (scales)
        {
            // rotate based on new position
            topPos = new Vector3(newTop[0], newTop[1], 0);
            Vector3 shipPos = new Vector3(vectorResult[0], vectorResult[1], 0);
            float rotation = _maths.CalculateRotation(topPos, shipPos);
            spaceship.transform.eulerAngles = new Vector3(0, 0, rotation);

            // scale based on calculation
            rightPos = new Vector3(newRight[0], newRight[1], 0);
            float scaleY = Vector3.Distance(topPos, shipPos);
            float scaleX = _maths.CalculateScaleX(rightPos, topPos);
            spaceship.transform.localScale = new Vector3(scaleX, scaleY, 1);
        }

        // line renderer update:
        startVObject.SetActive(true);
        endVObject.SetActive(true);
        var position = new Vector3(startVector[0], startVector[1], 0);
        startV.SetPosition(1, position);
        position = new Vector3(vectorResult[0], vectorResult[1], 0);
        endV.SetPosition(1, position);
    }

    public void UpdateSpaceshipG(float[] vectorResult)
    {
        result = vectorResult;
        anim.SetTrigger("spaceshipBlink");
    }

    public void MoveAfterAnim()
    {
        // move spaceship
        rb.MovePosition(new Vector2(result[0], result[1]));
        endPosition = new Vector3(result[0], result[1], 0);
        watchForEnd = true;

        // line renderer update:
        endVObject.SetActive(true);
        var position = new Vector3(result[0], result[1], 0);
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
        // activate warning text
        if (warningText != null) warningText.SetActive(true);
    }

    public void RemoveLines()
    {
        startVObject.SetActive(false);
        endVObject.SetActive(false);
    }

    public void SetScaling(bool value)
    {
        scales = value;
    }

    public void ExplosionAnimation()
    {
        //Plays Explosion on Death
        explosion.SetActive(true);
    }
}
