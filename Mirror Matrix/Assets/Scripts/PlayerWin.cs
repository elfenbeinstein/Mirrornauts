using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWin : MonoBehaviour
{
    [SerializeField] private int winRound = 100;
    
    private void Start()
    {
        EventManager.Instance.AddEventListener("TURN", TurnListener);
    }

    private void OnDestroy()
    {
        EventManager.Instance.RemoveEventListener("TURN", TurnListener);
    }

    private void TurnListener(string eventName, object param)
    {
        if (eventName == "NextTurn")
        {
            if ((int)param == winRound)
            {
                PlayerWins();
            }
        }
    }

    public void PlayerWins()
    {
        EventManager.Instance.EventGo("AUDIO", "PlayWin");
        //GameManagement._audioManager._sfxSounds.PlayWin();
        Debug.Log("PlayerWins");

        //set up certificate

        // display certificate

        // save certificate

        // send message to errormsg script
    }
}
