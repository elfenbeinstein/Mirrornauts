using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : MonoBehaviour
{
    public void PlayerWins()
    {
        EventManager.Instance.EventGo("AUDIO", "PlayWin");
        //GameManagement._audioManager._sfxSounds.PlayWin();

        //set up certificate

        // display certificate

        // save certificate
    }
}
