using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// handles the loop sound(s) in the game
/// currently only the sfx when the mouse coordinates are presented
/// called via sfx script
/// </summary>

public class AudioLoop : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float volume = 0.4f;

    [SerializeField] private float durationFadeIn = 2f;
    [SerializeField] private float durationFadeOut = 2f;

    public void PlayLoop()
    {
        StopAllCoroutines();

        if (durationFadeIn == 0) StartLoop();
        else
        {
            if (!audioSource.isPlaying) audioSource.Play();
            StartCoroutine(StartFade(durationFadeIn, volume));
        }
    }

    public void StartLoop()
    {
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
        if (!audioSource.isPlaying) audioSource.Play();
        audioSource.volume = volume;
    }

    public void StopLoop()
    {
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
        if (audioSource.isPlaying)
        {
            StopAllCoroutines();
            StartCoroutine(StartFade(durationFadeOut, 0));
        }
    }

    IEnumerator StartFade(float duration, float targetV)
    {
        float time = 0;
        float startV = audioSource.volume;

        while (time < duration)
        {
            time += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startV, targetV, time / duration);
            yield return null;
        }

        if (targetV == 0)
        {
            audioSource.Stop();
            audioSource.volume = 0;
        }

        yield break;
    }

    // for mixing:
    [ContextMenu("play")]
    public void PlayFromInspector()
    {
        PlayLoop();
    }

    // for mixing:
    [ContextMenu("stop")]
    public void StopFromInspector()
    {
        StopLoop();
    }
}
