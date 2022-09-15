using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Stats", order = 1)]
public class Stats : ScriptableObject
{
    public Sprite[] countdownNumbers;

    public int highscore;
    public int deaths;
    public int lastPlayedLevel;
    public int levelsCompleted;

    public int currentTurnCount;
    
}
