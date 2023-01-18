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
    [SerializeField] CertificateData certificateData;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void LoadMenu()
    {
        GameManagement.LoadStartMenu();
    }

    public void SetUp()
    {
        // overwrite certificate
        certificateData.dashsUsed = inGameData.dashsUsed;
        certificateData.deathAmount = inGameData.deathAmount;
        certificateData.energyUsed = inGameData.energyUsed;
        certificateData.shieldsUsed = inGameData.shieldsUsed;
        certificateData.trainingTime = inGameData.trainingTime;
        certificateData.winTime = inGameData.winTime;
        certificateData.hasSaveData = true;

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
        enterName.SetActive(true);
    }

    public void SaveName()
    {
        EventManager.Instance.EventGo("DATA", "Name", nameInput.text);

        enterName.SetActive(false);

        // set name in field

        display.SetActive(true);
    }
}
