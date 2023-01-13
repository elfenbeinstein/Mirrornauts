using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// behaviour of every spawned object (sits on the hazard prefab)
/// can be used for collectible and hazard (check bool isHazard)
/// </summary>

public class ObjectBehaviour : MonoBehaviour
{
    [Tooltip("if it is a hazard = true; if a collectible = false + add powerup script to object")]
    public bool isHazard;

    [SerializeField] private Numbers _numbers;

    [SerializeField] private SpriteRenderer countdownRenderer;
    [Tooltip("offset from corner of the number field")]
    [SerializeField] private float offset;
    [SerializeField] private Transform parentNumber;

    [SerializeField] private GameObject _filling;
    [SerializeField] private GameObject _frame;
    [SerializeField] private GameObject parent;

    private int round;
    private int countdown;
    private int liftoff;

    private TurnManager _turnManager;

    private bool isCounting;
    private bool isLifting;

    [HideInInspector] public bool isTouching;
    private bool touched;
    
    public void SetUpNewSpawn(int _round, int _countdown, int _liftoff, Vector3 _position, Quaternion _rotation, TurnManager _script)
    {
        round = _round;
        countdown = _countdown;
        liftoff = _liftoff;
        _turnManager = _script;
        _turnManager.AddSpawn(this);

        // set up position & rotation
        parent.transform.position = _position;
        parent.transform.rotation = _rotation;

        if (countdown == 0)
        {
            _frame.SetActive(true);
            _filling.SetActive(true);

            countdownRenderer.sprite = _numbers.countdownNumbers[liftoff];

            isCounting = false;
            isLifting = true;
        }
        else
        {
            // set countdown (number in corner)
            countdownRenderer.sprite = _numbers.countdownNumbers[countdown];

            // set up visuals
            _frame.SetActive(true);
            _filling.SetActive(false);

            isCounting = true;
            isLifting = false;
        }

        if (_turnManager.randomAngle != 0)
        {
            SetUpNumberDisplay();
        }
    }

    private void SetUpNumberDisplay()
    {
        float xPosition;
        float yPosition;
        #region Get Top Right Corner of Spawn
        // if the object is flipped to the side the x and y values of scale are flipped
        /*
        if (Mathf.Abs(_turnManager.randomAngle) == 90 || Mathf.Abs(_turnManager.randomAngle) == 270)
        {
            xPosition = transform.localPosition.y + transform.localScale.y / 2 - offset;
            yPosition = transform.localPosition.x + transform.localScale.x / 2 - offset;
        }
        else
        {
            xPosition = transform.localPosition.x + transform.localScale.x / 2 - offset;
            yPosition = transform.localPosition.y + transform.localScale.y / 2 - offset;
        }
        */

        if (Mathf.Abs(_turnManager.randomAngle) == 90)
        {
            xPosition = transform.localPosition.x + transform.localScale.x / 2 - offset;
            yPosition = transform.localPosition.y + transform.localScale.y / 2 - offset;
        }
        else if (Mathf.Abs(_turnManager.randomAngle) == 180)
        {
            xPosition = transform.localPosition.x + transform.localScale.x / 2 - offset;
            yPosition = transform.localPosition.y + transform.localScale.y / 2 - offset;
        }
        else if (Mathf.Abs(_turnManager.randomAngle) == 270)
        {
            xPosition = (transform.localPosition.x + transform.localScale.x / 2 - offset) * -1;
            yPosition = transform.localPosition.y + transform.localScale.y / 2 - offset;
        }
        else
        {
            xPosition = transform.localPosition.x + transform.localScale.x / 2 - offset;
            yPosition = transform.localPosition.y + transform.localScale.y / 2 - offset;
        }

        #endregion

        // set number to top right corner
        //countdownRenderer.gameObject.transform.localPosition = new Vector3(xPosition, yPosition, 0);
        parentNumber.localPosition = new Vector3(xPosition, yPosition, 0);

        // rotate number by turn manager random angle (is already set to - value in randomisation)
        countdownRenderer.gameObject.transform.Rotate(0, 0, _turnManager.randomAngle);
    }

    public void NextTurn(int currentRound)
    {
        if (isCounting)
        {
            if (round - currentRound == 0)
            {
                countdownRenderer.sprite = _numbers.countdownNumbers[liftoff];

                _filling.SetActive(true);

                isCounting = false;
                isLifting = true;

                if (touched)
                {
                    isTouching = true;
                }
            }
            else
            {
                // update countdown
                countdownRenderer.sprite = _numbers.countdownNumbers[round - currentRound];
            }
        }
        else if (isLifting)
        {
            if (currentRound - round - liftoff >= 0)
            {
                RemoveSelfFromList();
            }
            else
            {
                countdownRenderer.sprite = _numbers.countdownNumbers[(currentRound - round - liftoff) * -1];
            }
        }
        else
        {
            Debug.Log("error in if statement of object behaviour script");
        }
    }

    public void RemoveSelfFromList()
    {
        if (_turnManager == null)
        {
            _turnManager = FindObjectOfType<TurnManager>();
        }

        _turnManager.RemoveSpawn(this.gameObject.GetComponent<ObjectBehaviour>());
        Destroy(parent);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (isLifting)
        {
            isTouching = true;
        }
        else
        {
            touched = true;
            // if the object is not yet active - saving if it touched.
            // if in the same round the active turns active --> switched to isTouching is true
            // which will be used to send message to spaceship
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTouching = false;
        touched = false;
        //Debug.Log("ship left " + gameObject);
    }
}
