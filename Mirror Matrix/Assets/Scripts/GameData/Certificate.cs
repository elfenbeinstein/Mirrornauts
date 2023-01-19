using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Certificate : MonoBehaviour
{
    [SerializeField] private List<CanvasGroup> canvasGroups;
    [SerializeField] private GameObject bg;
    [SerializeField] private GameObject display;
    [SerializeField] private GameObject enterName;
    [SerializeField] private TMPro.TMP_InputField nameInput;

    [Space]
    [SerializeField] CertificateData inGameData;
    public CertificateData certificateData;

    [Space]
    [SerializeField] private TMPro.TextMeshProUGUI codeField;
    [SerializeField] private TMPro.TextMeshProUGUI nameField;
    [SerializeField] private TMPro.TextMeshProUGUI deathEnergyField;
    [SerializeField] private TMPro.TextMeshProUGUI shieldDashField;
    [SerializeField] private TMPro.TextMeshProUGUI gameTimeField;
    [SerializeField] private TMPro.TextMeshProUGUI trainingTimeField;

    //public float testTime;
    public string defaultText;

    void Start()
    {
        bg.SetActive(false);
        enterName.SetActive(false);
        display.SetActive(false);
        defaultText = nameInput.text;
    }

    public void LoadMenu()
    {
        EventManager.Instance.EventGo("DATA", "Save");
        GameManagement.LoadStartMenu();
    }

    public void SetUpValues()
    {
        // overwrite certificate
        certificateData.dashsUsed = inGameData.dashsUsed;
        certificateData.deathAmount = inGameData.deathAmount;
        certificateData.energyUsed = inGameData.energyUsed;
        certificateData.shieldsUsed = inGameData.shieldsUsed;
        certificateData.trainingTime = inGameData.trainingTime;
        certificateData.winTime = inGameData.winTime;
        certificateData.hasSaveData = true;

        // generate random code
        int r1 = Random.Range(0, 10);
        int random2 = Random.Range(0, 3);
        int random3 = Random.Range(0, 2);
        int r2, r3;
        if (random2 == 0) r2 = 4;
        else if (random2 == 1) r2 = 6;
        else r2 = 8;
        if (random3 == 0) r3 = 3;
        else r3 = 7;

        string energy = certificateData.energyUsed.ToString("000");
        if (certificateData.energyUsed > 999) energy = "999";
        string death = certificateData.deathAmount.ToString("00");
        if (certificateData.deathAmount > 99) death = "99";
        string dash = certificateData.dashsUsed.ToString("00");
        if (certificateData.dashsUsed > 99) dash = "99";
        string shield = certificateData.shieldsUsed.ToString("00");
        if (certificateData.shieldsUsed > 99) shield = "99";

        // code setup: r1 + energy + death + r2 + dash + shield + r3
        certificateData.code = r1.ToString() + energy + death + r2.ToString() + dash + shield + r3.ToString();

        // reset ingame certificate and save data
        EventManager.Instance.EventGo("DATA", "ResetInGame");
    }

    public void ShowCertificate()
    {
        // disable all other inputs
        if (canvasGroups.Count != 0)
            foreach (CanvasGroup group in canvasGroups)
                group.interactable = false;

        // turn on certificate display
        bg.SetActive(true);
        enterName.SetActive(true);
    }

    public void SaveName()
    {
        if (nameInput.text == "" || nameInput.text == defaultText) return;

        EventManager.Instance.EventGo("DATA", "Name", nameInput.text);

        display.SetActive(true);

        // set all text fields:
        nameField.text = nameInput.text;
        deathEnergyField.text = $"brauchte {certificateData.deathAmount} Versuche, {certificateData.energyUsed} Energie,";
        shieldDashField.text = $"und benutzte {certificateData.dashsUsed} Mal Sprint und {certificateData.shieldsUsed} Mal Schild.";
        codeField.text = certificateData.code;
        System.TimeSpan time = System.TimeSpan.FromSeconds(certificateData.winTime);
        gameTimeField.text = $"Spielzeit: " + time.ToString(@"hh\:mm\:ss");
        time = System.TimeSpan.FromSeconds(certificateData.trainingTime);
        trainingTimeField.text = $"Trainingszeit: " + time.ToString(@"hh\:mm\:ss");

        enterName.SetActive(false);
    }

    public void DisplayValues()
    {
        display.SetActive(true);

        // set all text fields:
        nameField.text = certificateData._name;
        deathEnergyField.text = $"brauchte {certificateData.deathAmount} Versuche, {certificateData.energyUsed} Energie,";
        shieldDashField.text = $"und benutzte {certificateData.dashsUsed} Mal Sprint und {certificateData.shieldsUsed} Mal Schild.";
        codeField.text = certificateData.code;
        System.TimeSpan time = System.TimeSpan.FromSeconds(certificateData.winTime);
        gameTimeField.text = $"Spielzeit: " + time.ToString(@"hh\:mm\:ss");
        time = System.TimeSpan.FromSeconds(certificateData.trainingTime);
        trainingTimeField.text = $"Trainingszeit: " + time.ToString(@"hh\:mm\:ss");

        enterName.SetActive(false);
    }

    public void SetUpInMenu()
    {
        bg.SetActive(true);
        display.SetActive(true);

        // set all text fields:
        nameField.text = certificateData._name;
        deathEnergyField.text = $"brauchte {certificateData.deathAmount} Versuche, {certificateData.energyUsed} Energie,";
        shieldDashField.text = $"und benutzte {certificateData.dashsUsed} Mal Sprint und {certificateData.shieldsUsed} Mal Schild.";
        codeField.text = certificateData.code;
        System.TimeSpan time = System.TimeSpan.FromSeconds(certificateData.winTime);
        gameTimeField.text = $"Spielzeit: " + time.ToString(@"hh\:mm\:ss");
        time = System.TimeSpan.FromSeconds(certificateData.trainingTime);
        trainingTimeField.text = $"Trainingszeit: " + time.ToString(@"hh\:mm\:ss");

        enterName.SetActive(false);
    }

    public void SetInactiveInMenu()
    {
        display.SetActive(false);
        bg.SetActive(false);
        enterName.SetActive(false);
    }

    /*
    [ContextMenu("test zeit")]
    public void TestHours()
    {
        System.TimeSpan time = System.TimeSpan.FromSeconds(testTime);
        string str = time.ToString(@"hh\:mm\:ss");
        gameTimeField.text = "Spielzeit: " + str;
    }
    */
}
