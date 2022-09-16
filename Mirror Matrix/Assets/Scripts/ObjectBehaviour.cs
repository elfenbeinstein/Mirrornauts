using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectBehaviour : MonoBehaviour
{
    [SerializeField] private Stats _stats;

    [SerializeField] private GameObject _collider;
    [SerializeField] private SpriteRenderer countdownRenderer;

    private int round;
    private int countdown;
    private int liftoff;

    private Vector3 spawnPosition;
    private Vector3 spawnRotation;

    private TurnManager _turnManager;

    private bool isCounting;
    private bool isLifting;

    void Start()
    {
        // for testing:
        isCounting = true;
        round = 4;
        countdown = 4;
        liftoff = 4;
        spawnPosition = new Vector3(1, 1, 1);
        spawnRotation = new Vector3(0, 0, 0.5f);

        gameObject.transform.position = spawnPosition;
        gameObject.transform.eulerAngles = spawnRotation;
        countdownRenderer.sprite = _stats.countdownNumbers[countdown];
        _collider.SetActive(false);
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
        _collider.SetActive(false);

        isCounting = true;
        isLifting = false;
    }

    public void NextTurn(int currentRound)
    {
        if (isCounting)
        {
            if (round - currentRound == 0)
            {
                _collider.SetActive(true);
                countdownRenderer.sprite = _stats.countdownNumbers[liftoff];

                // MISSING: update look of the spawn

                isCounting = false;
                isLifting = true;
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
                DeleteSelf();
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

    public void DeleteSelf()
    {
        // tell turn manager to delete itself from list
        // work around for testing
        _turnManager = FindObjectOfType<TurnManager>();
        //_turnManager.RemoveSpawn(this.gameObject.GetComponent<ObjectBehaviour>());

        gameObject.SetActive(false); // ideally deletes itself
    }
}
