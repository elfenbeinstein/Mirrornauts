using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// scriptable object that holds all data relevant for the certificate
/// data is collected during gameplay in a scriptable object InGameData
/// once player wins --> written over into a scriptable object CertificateData
/// </summary>

[CreateAssetMenu(fileName = "new Certificate Data", menuName = "Data", order = 0)]
public class CertificateData : ScriptableObject
{
    public int deathAmount;
    public int dashsUsed;
    public int shieldsUsed;
    public int energyUsed;

    public float trainingTime;
    public float winTime;
    public string code;
    public string _name;

    public bool hasSaveData;
}
