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
    [SerializeField] private SpriteRenderer _bodyRenderer;

    private int round;
    private int countdown;
    private int liftoff;

    private Vector3 spawnPosition;
    private Vector3 spawnRotation;

    // --> update countdown/liftoff sprite
    // --> update visuals & collider

    // maybe update alpha based on max/min values (100 / turns it waits; min is 20, max is based on other # before it turns to 100)

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetVariables(int _round, int _countdown, int _liftoff, Vector3 _position, Vector3 _rotation)
    {
        round = _round;
        countdown = _countdown;
        liftoff = _liftoff;
        spawnPosition = _position;
        spawnRotation = _rotation;
    }
}
