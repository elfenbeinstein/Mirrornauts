using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private GameObject startO;
    [SerializeField] private GameObject chooseO;
    [SerializeField] private GameObject creditsO;
    [SerializeField] private GameObject helpO;
    [SerializeField] private GameObject certificateO;
    [SerializeField] private GameObject certificateButton;
    private Certificate certificate;

    private bool startOpen;

    void Start()
    {
        startOpen = true;
        startO.SetActive(true);
        chooseO.SetActive(false);
        creditsO.SetActive(false);
        helpO.SetActive(false);

        certificate = certificateO.GetComponent<Certificate>();
        certificate.SetInactiveInMenu();
    }

    void Update()
    {
        if (startOpen)
        {
            if (Input.anyKeyDown)
            {
                startOpen = false;
                startO.SetActive(false);
                chooseO.SetActive(true);

                // if there is save data for a certificate --> set button to active
                certificateButton.SetActive(certificate.certificateData.hasSaveData);
            }
        }
    }

    public void StartButton()
    {
        GameManagement.StartGameMode();
    }

    public void TrainButton()
    {
        GameManagement.StartTrainingMode();
    }

    public void CreditsButton()
    {
        creditsO.SetActive(true);
        chooseO.SetActive(false);
    }
    public void HelpButton()
    {
        helpO.SetActive(true);
        chooseO.SetActive(false);
    }

    public void BackButton()
    {
        chooseO.SetActive(true);
        creditsO.SetActive(false);
        helpO.SetActive(false);
        certificate.SetInactiveInMenu();
    }

    public void CertificateButton()
    {
        chooseO.SetActive(false);
        certificateO.SetActive(true);
        certificate.SetUpInMenu();
    }

    public void DeleteData()
    {
        EventManager.Instance.EventGo("DATA", "DeleteAll");
        certificateButton.SetActive(false);
    }

    public void QuitButton()
    {
        GameManagement.QuitGame();
    }
}
