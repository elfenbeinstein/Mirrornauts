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

    private bool startOpen;

    void Start()
    {
        startOpen = true;
        startO.SetActive(true);
        chooseO.SetActive(false);
        creditsO.SetActive(false);
        helpO.SetActive(false);
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
        certificateO.SetActive(false);
    }

    public void CertificateButton()
    {

    }

    public void QuitButton()
    {
        GameManagement.QuitGame();
    }

    public void GetCertificate()
    {

    }
}
