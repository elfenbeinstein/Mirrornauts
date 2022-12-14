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

    [SerializeField] private Stats _stats;

    [SerializeField] private SpriteRenderer countdownRenderer;

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

        // set up position & rotation
        parent.transform.position = _position;
        parent.transform.rotation = _rotation;



        // set countdown (number in corner)
        countdownRenderer.sprite = _stats.countdownNumbers[countdown];

        // set up visuals
        _frame.SetActive(true);
        _filling.SetActive(false);

        isCounting = true;
        isLifting = false;
    }

    public void NextTurn(int currentRound)
    {
        if (isCounting)
        {
            if (round - currentRound == 0)
            {
                countdownRenderer.sprite = _stats.countdownNumbers[liftoff];

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
                countdownRenderer.sprite = _stats.countdownNumbers[round - currentRound];
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
                countdownRenderer.sprite = _stats.countdownNumbers[(currentRound - round - liftoff) * -1];
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
        //Debug.Log("touched spaceship");
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
    }
}
