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
        ScreenCapture.CaptureScreenshot(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/Zertifikat" + FileCounter + ".png");

        FileCounter++;
        PlayerPrefs.SetInt("FileCounter", FileCounter);
    }
}
