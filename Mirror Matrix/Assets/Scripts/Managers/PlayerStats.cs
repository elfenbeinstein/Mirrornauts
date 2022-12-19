using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int dashAmount;
    public bool shieldActive;
    public int energy;
    public int currentHealth;

    private bool shieldCountdown;
    private int shieldCD;
    private bool dashCountdown;
    private int dashCD;

    void Start()
    {
        dashAmount = 1;
        shieldActive = false;
        currentHealth = 1;

        EventManager.Instance.AddEventListener("TURN", TurnListener);
    }

    private void OnDestroy()
    {
        EventManager.Instance.RemoveEventListener("TURN", TurnListener);
    }

    void TurnListener(string eventName, object param)
    {
        if (eventName == "NextTurn") NextTurn();
        if (eventName == "Addition") Addition();
        if (eventName == "Shield") Shield();
    }

    void NextTurn()
    {
        // if dashAmount == 0 --> countdown

        // if shieldActive --> countdown
    }

    void Addition()
    {
        // remove addition value
        // start countdown
    }

    void Shield()
    {

    }
}
