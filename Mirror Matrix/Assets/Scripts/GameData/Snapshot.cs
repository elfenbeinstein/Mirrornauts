using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Snapshot : MonoBehaviour
{
    public int FileCounter = 0;

    private void Start()
    {
        FileCounter = PlayerPrefs.GetInt("FileCounter", 0);
    }

    public void TakeSnapshot()
    {
        FileCounter = PlayerPrefs.GetInt("FileCounter", 0);

        string name = GetComponent<Certificate>().certificateData._name;
        ScreenCapture.CaptureScreenshot(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/Zertifikat_" + FileCounter + "_" + name + ".png");

        FileCounter++;
        PlayerPrefs.SetInt("FileCounter", FileCounter);
    }
}
