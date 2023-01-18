using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    [SerializeField] CertificateData inGameData;
    [SerializeField] CertificateData certificateData;

    // strings für player prefs in game values:
    [Space]
    [SerializeField] string deathAmount;
    [SerializeField] string dashsUsed;
    [SerializeField] string shieldsUsed;
    [SerializeField] string energyUsed;
    [SerializeField] string trainingTime;
    [SerializeField] string winTime;
    
    // strings für player prefs certificate values
    [Space]
    [SerializeField] string deathAmountC;
    [SerializeField] string dashsUsedC;
    [SerializeField] string shieldsUsedC;
    [SerializeField] string energyUsedC;
    [SerializeField] string trainingTimeC;
    [SerializeField] string winTimeC;

    int hasCertificate;

    private void Start()
    {
        EventManager.Instance.AddEventListener("DATA", DataListener);
    }

    private void OnDestroy()
    {
        EventManager.Instance.RemoveEventListener("DATA", DataListener);
    }

    void DataListener(string eventName, object param)
    {

    }

    public void SaveDataToPrefs()
    {

    }
}
