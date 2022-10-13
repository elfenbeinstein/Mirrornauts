using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
//using TMPro;

/// <summary>
/// behaviour of every spawned object
/// can be used for collectible and hazard (check bool isHazard)
/// </summary>

public class ObjectBehaviour : MonoBehaviour
{
    [Tooltip("if it is a hazard = true; if a collectible = false")]
    public bool isHazard;

    [SerializeField] private Stats _stats;

    [SerializeField] private SpriteRenderer countdownRenderer;

    [SerializeField] private GameObject _filling;
    [SerializeField] private GameObject _frame;

    private int round;
    private int countdown;
    private int liftoff;

    private Vector3 spawnPosition;
    private Vector3 spawnRotation;

    private TurnManager _turnManager;

    private bool isCounting;
    private bool isLifting;

    [HideInInspector] public bool isTouching;
    private bool touched;

    void Start()
    {
        // for testing:
        /*
        isCounting = true;
        round = 4;
        countdown = 4;
        liftoff = 4;
        spawnPosition = new Vector3(1, 1, 1);
        spawnRotation = new Vector3(0, 0, 0.5f);

        gameObject.transform.position = spawnPosition;
        gameObject.transform.eulerAngles = spawnRotation;
        countdownRenderer.sprite = _stats.countdownNumbers[countdown];
        _collider.SetActive(false);*/
    }

    
    public void SetUpNewSpawn(int _round, int _countdown, int _liftoff, Vector3 _position, Vector3 _rotation, TurnManager _script)
    {
        round = _round;
        countdown = _countdown;
        liftoff = _liftoff;
        spawnPosition = _position;
        spawnRotation = _rotation;
        _turnManager = _script;

        // set up position & rotation
        gameObject.transform.position = _position;
        gameObject.transform.eulerAngles = _rotation;

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
        //gameObject.SetActive(false); -- obsolete, for testing
        Destroy(this.gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log($"collision with {gameObject} and 2D On Trigger Enter");
        if (isLifting)
        {
            isTouching = true;
        }
        else
        {
            touched = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTouching = false;
        touched = false;
    }
}
