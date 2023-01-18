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

    [Space]
    [SerializeField] string hasCertificate;
    public bool hasCert;

    private void Start()
    {
        EventManager.Instance.AddEventListener("DATA", DataListener);
        GetDataFromPrefs();
    }

    private void OnDestroy()
    {
        EventManager.Instance.RemoveEventListener("DATA", DataListener);
    }

    void DataListener(string eventName, object param)
    {
        if (eventName == "Name")
        {
            certificateData.name = (string)param;
            SaveDataToPrefs();
        }
        else if (eventName == "DeleteAll")
            ResetCertificateAndGameData();
        else if (eventName == "ResetInGame")
            ResetData(inGameData, true);
    }

    public void SaveDataToPrefs()
    {

    }

    public void GetDataFromPrefs()
    {


    }

    [ContextMenu("Clear All Save Data")]
    public void ClearSaveData()
    {
        PlayerPrefs.DeleteAll();
    }

    public void ResetCertificateAndGameData()
    {
        ClearSaveData();

        ResetData(inGameData);
        ResetData(certificateData);
    }

    public void ResetData(CertificateData dataSet, bool save = false)
    {
        dataSet.deathAmount = 0;
        dataSet.dashsUsed = 0;
        dataSet.shieldsUsed = 0;
        dataSet.energyUsed = 0;
        dataSet.trainingTime = 0;
        dataSet.winTime = 0;
        dataSet.hasSaveData = false;

        if (save) SaveDataToPrefs();
    }
}
