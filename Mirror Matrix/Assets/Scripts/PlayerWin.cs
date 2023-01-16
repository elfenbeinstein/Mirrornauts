using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerWin : MonoBehaviour
{
    [SerializeField] private int winRound = 100;
    [SerializeField] private GameObject certificateScreen;
    [SerializeField] private TextMeshProUGUI codeText;
    
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
        FindObjectOfType<ErrorMsg>().OpenError(4);

        // save certificate

        // send message to errormsg script
    }

    
    public void CertificateButton()
    {
        //Activates the certificate screen after pressing the button on the win-error popup
        certificateScreen.SetActive(true);

        //Set code at certificate textbox
        //codeText.text = "Der Certificate Text"
    }
}
