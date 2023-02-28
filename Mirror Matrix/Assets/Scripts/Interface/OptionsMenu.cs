using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/// <summary>
/// In-Game Menu
/// </summary>

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] GameObject warningObj;
    [SerializeField] GameObject closeWarnObj;

    [SerializeField]
    private AudioMixer _mixer;
    [SerializeField]
    private Slider sliderSFX;
    [SerializeField]
    private Slider sliderMusic;

    void Start()
    {
        sliderSFX.value = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
        sliderMusic.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
    }


    public void ToggleWarning()
    {
        if (warningObj.activeInHierarchy == true) { warningObj.SetActive(false); }
        else { warningObj.SetActive(true); }
    }

    public void ToggleQuitWarning()
    {
        if (closeWarnObj.activeInHierarchy == true) { closeWarnObj.SetActive(false); }
        else { closeWarnObj.SetActive(true); }
    }
    public void SetLevelSFX(float sliderValue) // called from menu slider
    {
        _mixer.SetFloat("VolSFX", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("SFXVolume", sliderValue);
    }

    public void SetLevelMusic(float sliderValue) // called from menu slider
    {
        _mixer.SetFloat("VolMusic", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
    }
}
