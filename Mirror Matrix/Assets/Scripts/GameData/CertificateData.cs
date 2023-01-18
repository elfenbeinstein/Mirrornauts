using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public bool hasSaveData;
}
