using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Holds Reference To Which Round is the Win Round
/// Then Sends out Messages that Game is won (to set up certificate etc)
/// Then turns off all visuals and sets new bg visuals
/// </summary>

public class PlayerWin : MonoBehaviour
{
    [SerializeField] private int winRound = 100;
    [SerializeField] private GameObject certificateScreen;
    [SerializeField] private TextMeshProUGUI codeText;
    [SerializeField] private ErrorMsg _errorMsg;
    [SerializeField] private TurnManager _turnManager;
    [SerializeField] private Certificate certificate;

    [SerializeField] private int story2;
    [SerializeField] private int story3;

    [SerializeField] GameObject[] disableVisuals;
    [SerializeField] private GameObject winBG;
    [SerializeField] private Border_Behaviour border;
    
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
            else if ((int)param == story2)
                _errorMsg.OpenError(1);
            else if ((int)param == story3)
                _errorMsg.OpenError(2);
        }
    }

    public void PlayerWins()
    {
        // stop counting time:
        EventManager.Instance.EventGo("DATA", "CountGame", false);
        EventManager.Instance.EventGo("DATA", "CountTrain", false);

        EventManager.Instance.EventGo("AUDIO", "PlayWin");
        Debug.Log("PlayerWins");

        // stop turnmanager
        _turnManager.PlayerWin();

        // send message to errormsg script
        EventManager.Instance.EventGo("ERROR", "Win", 4);

        certificate.SetUpValues();
        border.win = true;

        //Turns off al BG visuals
        foreach (GameObject obj in disableVisuals) { obj.SetActive(false); }

        winBG.SetActive(true);
    }
}
