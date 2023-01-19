using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [SerializeField] string codeC;
    [SerializeField] string nameC;

    [Space]
    [SerializeField] string hasCertificate;
    public bool hasCert;

    bool countGameTime;
    bool countTrainTime;

    private void Start()
    {
        EventManager.Instance.AddEventListener("DATA", DataListener);
        GetDataFromPrefs();

        if (SceneManager.GetActiveScene().buildIndex == 1) countTrainTime = true;
        else if (SceneManager.GetActiveScene().buildIndex == 2) countGameTime = true;
    }

    private void Update()
    {
        if (countGameTime) inGameData.winTime += Time.deltaTime;
        else if (countTrainTime) inGameData.trainingTime += Time.deltaTime;
    }

    private void OnDestroy()
    {
        EventManager.Instance.RemoveEventListener("DATA", DataListener);
    }

    void DataListener(string eventName, object param)
    {
        if (eventName == "Name")
        {
            certificateData._name = (string)param;
            SaveDataToPrefs();
        }
        else if (eventName == "DeleteAll")
            ResetCertificateAndGameData();
        else if (eventName == "ResetInGame")
            ResetData(inGameData, true);
        else if (eventName == "CountGame")
            countGameTime = ((bool)param);
        else if (eventName == "CountTrain")
            countTrainTime = ((bool)param);
        else if (eventName == "Shield") inGameData.shieldsUsed++;
        else if (eventName == "Dash") inGameData.dashsUsed++;
        else if (eventName == "Energy") inGameData.energyUsed += ((int)param);
        else if (eventName == "Death") inGameData.deathAmount++;
        else if (eventName == "Save") SaveDataToPrefs();
    }

    public void SaveDataToPrefs()
    {
        // set for in game data:
        PlayerPrefs.SetInt(deathAmount, inGameData.deathAmount);
        PlayerPrefs.SetInt(dashsUsed, inGameData.dashsUsed);
        PlayerPrefs.SetInt(energyUsed, inGameData.energyUsed);
        PlayerPrefs.SetFloat(trainingTime, inGameData.trainingTime);
        PlayerPrefs.SetFloat(winTime, inGameData.winTime);

        // set for certificate data:
        PlayerPrefs.SetInt(deathAmountC, certificateData.deathAmount);
        PlayerPrefs.SetInt(dashsUsedC, certificateData.dashsUsed);
        PlayerPrefs.SetInt(energyUsedC, certificateData.energyUsed);
        PlayerPrefs.SetFloat(trainingTimeC, certificateData.trainingTime);
        PlayerPrefs.SetFloat(winTimeC, certificateData.winTime);
        PlayerPrefs.SetString(codeC, certificateData.code);
        PlayerPrefs.SetString(nameC, certificateData._name);
        if (certificateData.hasSaveData == true) PlayerPrefs.SetInt(hasCertificate, 1);
        else PlayerPrefs.SetInt(hasCertificate, 0);
    }

    public void GetDataFromPrefs()
    {
        // get values for in game data
        if (PlayerPrefs.HasKey(deathAmount)) inGameData.deathAmount = PlayerPrefs.GetInt(deathAmount);
        if (PlayerPrefs.HasKey(dashsUsed)) inGameData.dashsUsed = PlayerPrefs.GetInt(dashsUsed);
        if (PlayerPrefs.HasKey(energyUsed)) inGameData.energyUsed = PlayerPrefs.GetInt(energyUsed);
        if (PlayerPrefs.HasKey(trainingTime)) inGameData.trainingTime = PlayerPrefs.GetFloat(trainingTime);
        if (PlayerPrefs.HasKey(winTime)) inGameData.winTime = PlayerPrefs.GetFloat(winTime);

        // get values for certificate data
        if (PlayerPrefs.HasKey(deathAmountC)) certificateData.deathAmount = PlayerPrefs.GetInt(deathAmountC);
        if (PlayerPrefs.HasKey(dashsUsedC)) certificateData.dashsUsed = PlayerPrefs.GetInt(dashsUsedC);
        if (PlayerPrefs.HasKey(energyUsedC)) certificateData.energyUsed = PlayerPrefs.GetInt(energyUsedC);
        if (PlayerPrefs.HasKey(trainingTimeC)) certificateData.trainingTime = PlayerPrefs.GetFloat(trainingTimeC);
        if (PlayerPrefs.HasKey(winTimeC)) certificateData.winTime = PlayerPrefs.GetFloat(winTimeC);
        if (PlayerPrefs.HasKey(codeC)) certificateData.code = PlayerPrefs.GetString(codeC);
        if (PlayerPrefs.HasKey(nameC)) certificateData._name = PlayerPrefs.GetString(nameC);
        if (PlayerPrefs.HasKey(hasCertificate) && PlayerPrefs.GetInt(hasCertificate) == 1) certificateData.hasSaveData = true;
        else certificateData.hasSaveData = false;

    }

    [ContextMenu("Clear All Save Data")]
    public void ClearAllSaveData()
    {
        PlayerPrefs.DeleteAll();
        ResetData(inGameData);
        ResetData(certificateData);
    }

    public void ClearCertData()
    {
        PlayerPrefs.DeleteKey(deathAmount);
        PlayerPrefs.DeleteKey(deathAmountC);

        PlayerPrefs.DeleteKey(dashsUsed);
        PlayerPrefs.DeleteKey(dashsUsedC);

        PlayerPrefs.DeleteKey(shieldsUsed);
        PlayerPrefs.DeleteKey(shieldsUsedC);

        PlayerPrefs.DeleteKey(energyUsed);
        PlayerPrefs.DeleteKey(energyUsedC);

        PlayerPrefs.DeleteKey(winTime);
        PlayerPrefs.DeleteKey(winTimeC);

        PlayerPrefs.DeleteKey(trainingTime);
        PlayerPrefs.DeleteKey(trainingTimeC);

        PlayerPrefs.DeleteKey(codeC);
        PlayerPrefs.DeleteKey(hasCertificate);
        PlayerPrefs.DeleteKey(nameC);
    }

    public void ResetCertificateAndGameData()
    {
        ClearCertData();

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
        dataSet.code = "";
        dataSet._name = "";
        dataSet.hasSaveData = false;

        if (save) SaveDataToPrefs();
    }
}
