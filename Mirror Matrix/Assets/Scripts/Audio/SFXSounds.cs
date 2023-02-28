using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles all SFX in the game
/// Called via Event Listener
/// eventID is AUDIO, eventName depends on the SFX to be played
/// </summary>

public class SFXSounds : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    [Space(10)]
    [SerializeField] private AudioClip click;
    [SerializeField] [Range(0, 1)] private float clickVol;
    private bool canPlayClick = true;
    [SerializeField] private float clickCoolDown = 0.1f;

    [Space]
    [SerializeField] private AudioClip drag;
    [SerializeField] [Range(0, 1)] private float dragVol = 0.4f;

    [SerializeField] private AudioClip drop;
    [SerializeField] [Range(0, 1)] private float dropVol = 0.4f;

    [SerializeField] private AudioClip go;
    [SerializeField] [Range(0, 1)] private float goVol = 0.4f;

    [SerializeField] private AudioLoop hoverLoop;

    [Space]
    [SerializeField] private AudioClip energy;
    [SerializeField] [Range(0, 1)] private float energyVol = 0.4f;

    [SerializeField] private AudioClip shield;
    [SerializeField] [Range(0, 1)] private float shieldVol = 0.4f;

    [Space]
    [SerializeField] private AudioClip error;
    [SerializeField] [Range(0, 1)] private float errorVol = 0.4f;

    [SerializeField] private AudioClip death;
    [SerializeField] [Range(0, 1)] private float deathVol = 0.4f;

    [SerializeField] private AudioClip win;
    [SerializeField] [Range(0, 1)] private float winVol = 0.4f;

    void Start()
    {
        canPlayClick = true;
        EventManager.Instance.AddEventListener("AUDIO", AudioListener);
    }

    private void OnDestroy()
    {
        EventManager.Instance.RemoveEventListener("AUDIO", AudioListener);
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(1))
            PlayClick();
        else if (Input.GetMouseButtonUp(0))
            PlayClick();
    }

    void AudioListener(string eventName, object param)
    {
        if (eventName == "PlayDrag")
            PlayDrag();
        else if (eventName == "PlayDrop")
            PlayDrop();
        else if (eventName == "PlayGo")
            PlayGo();
        else if (eventName == "PlayHover")
            PlayHover();
        else if (eventName == "StopHover")
            StopHover();
        else if (eventName == "PlayEnergy")
            PlayEnergy();
        else if (eventName == "PlayShield")
            PlayShield();
        else if (eventName == "PlayError")
            PlayError();
        else if (eventName == "PlayDeath")
            PlayDeath();
        else if (eventName == "PlayWin")
            PlayWin();
    }

    [ContextMenu("Click")]
    public void PlayClick()
    {
        if (canPlayClick)
        {
            audioSource.PlayOneShot(click, clickVol);
            StartCoroutine(ClickCooldown());
        }
    }

    IEnumerator ClickCooldown()
    {
        canPlayClick = false;
        yield return new WaitForSeconds(clickCoolDown);
        canPlayClick = true;
    }

    [ContextMenu("Drag")]
    public void PlayDrag()
    {
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(drag, dragVol);
    }

    [ContextMenu("Drop")]
    public void PlayDrop()
    {
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(drop, dropVol);
    }
    
    [ContextMenu("Go")]
    public void PlayGo()
    {
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(go, goVol);
    }
    
    [ContextMenu("Hover")]
    public void PlayHover()
    {
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
        if (hoverLoop == null) hoverLoop = GetComponentInChildren<AudioLoop>();
        hoverLoop.PlayLoop();
    }
    public void StopHover()
    {
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
        if (hoverLoop == null) hoverLoop = GetComponentInChildren<AudioLoop>();
        hoverLoop.StopLoop();
    }
    
    [ContextMenu("Energy")]
    public void PlayEnergy()
    {
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(energy, energyVol);
    }
    
    [ContextMenu("Shield")]
    public void PlayShield()
    {
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(shield, shieldVol);
    }
    
    [ContextMenu("Error")]
    public void PlayError()
    {
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(error, errorVol);
    }
    
    [ContextMenu("Death")]
    public void PlayDeath()
    {
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(death, deathVol);
    }
    
    [ContextMenu("Win")]
    public void PlayWin()
    {
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(win, winVol);
    }
}

