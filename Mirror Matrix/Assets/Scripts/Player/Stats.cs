using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// all data connected to the player
// also contains the numbers displayed in the spawns (this is a workaround) elfenbeinstein needs CHANGE

[CreateAssetMenu(fileName = "Data", menuName = "Stats", order = 1)]
public class Stats : ScriptableObject
{
    public Sprite[] countdownNumbers;

    public int currentHealth;

    public int highscore;
    public int deaths;
    public int lastPlayedLevel;
    public int levelsCompleted;

    public int currentTurnCount;
    
}
