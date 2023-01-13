using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// data connected to the player for save file
// also contains the numbers displayed in the spawns (this is a workaround) elfenbeinstein needs CHANGE

[CreateAssetMenu(fileName = "Data", menuName = "Numbers", order = 1)]
public class Numbers : ScriptableObject
{
    public Sprite[] countdownNumbers;

    /*
    public int highscore;
    public int deaths;
    public int lastPlayedRound;
    public int roundsCompleted;

    public int currentTurnCount;
    */
    
}
