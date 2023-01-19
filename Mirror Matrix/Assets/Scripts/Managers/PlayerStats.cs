using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [HideInInspector] public int dashAmount;
    [HideInInspector] public bool shieldActive;
    [HideInInspector] public int energy;
    public int maxEnergy = 200;
    [HideInInspector] public int energyNeeded;
    [HideInInspector] public int currentHealth;

    private int shieldCD;
    private bool dashCountdown;
    [HideInInspector] public int dashCD;

    [Space]
    [Header("Kosten für Drehmatrix Radianten")]
    public int costDrehSechstel = 2;
    public int costDrehViertel = 3;
    public int costDrehDrittel = 4;
    public int costDrehHalbe = 6;
    public int costDrehzweiDrittel = 8;
    public int costDrehdreiViertel = 9;
    public int costDrehfünfSechstel = 10;
    public int costDrehEins = 12;

    [Space]
    [Header("Kosten für Drehmatrix Zahlenwert")]
    public int costDrehFEins = 0;
    public int costDrehFWurzelDrei = 2;
    public int costDrehFWurzelZwei = 3;
    public int costDrehFHalbe = 4;
    public int costDrehFNull = 6;
    public int costDrehFMinusHalbe = 8;
    public int costDrehFMinusWurzelZwei = 9;
    public int costDrehFMinusWurzelDrei = 10;
    public int costDrehFMinusEins = 12;


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
        if (eventName == "Addition") Addition();
        if (eventName == "Shield") Shield();
    }

    public void NextTurn()
    {
        // countdown dash --> if dashCD is zero, turn on dash again
        if (dashCountdown)
        {
            dashCD--;

            if (dashCD == 0)
            {
                dashCountdown = false;
                dashAmount = 1;

                EventManager.Instance.EventGo("DASH", "DashActive");
            }
            else
            {
                EventManager.Instance.EventGo("DASH", "Countdown");
            }
        }

        // if shieldActive --> countdown
        if (shieldActive)
        {
            shieldCD--;
            if (shieldCD == 0)
            {
                shieldActive = false;
                EventManager.Instance.EventGo("SHIELD", "Stop");
            }
        }
    }

    void Addition()
    {
        // remove addition value + start countdown at 3
        dashAmount = 0;
        dashCountdown = true;
        dashCD = 4;

        EventManager.Instance.EventGo("DATA", "Dash");

        // tell buttons:
        EventManager.Instance.EventGo("DASH", "Countdown");
    }

    void Shield()
    {
        EventManager.Instance.EventGo("DATA", "Shield");
        shieldActive = true;
        shieldCD = 4;
        EventManager.Instance.EventGo("SHIELD", "Start");
    }

    public void ResetFromManager()
    {
        shieldActive = false;
        dashAmount = 1;
        currentHealth = 1;
    }
}
