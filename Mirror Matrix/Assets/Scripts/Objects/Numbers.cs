using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// contains the numbers displayed in the spawns (this is a workaround)

[CreateAssetMenu(fileName = "Data", menuName = "Numbers", order = 1)]
public class Numbers : ScriptableObject
{
    public Sprite[] countdownNumbers;
}
