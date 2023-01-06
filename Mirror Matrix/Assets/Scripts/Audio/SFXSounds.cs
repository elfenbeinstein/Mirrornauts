using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(1))
            PlayClick();
        else if (Input.GetMouseButtonUp(0))
            PlayClick();
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
        audioSource.PlayOneShot(drag, dragVol);
    }

    [ContextMenu("Drop")]
    public void PlayDrop()
    {
        audioSource.PlayOneShot(drop, dropVol);
    }
    
    [ContextMenu("Go")]
    public void PlayGo()
    {
        audioSource.PlayOneShot(go, goVol);
    }
    
    [ContextMenu("Hover")]
    public void PlayHover()
    {
        hoverLoop.PlayLoop();
    }
    public void StopHover()
    {
        hoverLoop.StopLoop();
    }
    
    [ContextMenu("Energy")]
    public void PlayEnergy()
    {
        audioSource.PlayOneShot(energy, energyVol);
    }
    
    [ContextMenu("Shield")]
    public void PlayShield()
    {
        audioSource.PlayOneShot(shield, shieldVol);
    }
    
    [ContextMenu("Error")]
    public void PlayError()
    {
        audioSource.PlayOneShot(error, errorVol);
    }
    
    [ContextMenu("Death")]
    public void PlayDeath()
    {
        audioSource.PlayOneShot(death, deathVol);
    }
    
    [ContextMenu("Win")]
    public void PlayWin()
    {
        audioSource.PlayOneShot(win, winVol);
    }
}

