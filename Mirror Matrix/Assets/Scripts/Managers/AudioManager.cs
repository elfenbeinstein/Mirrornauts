using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// sits on game manager
/// originally intended for more but currently just starts the main theme at the beginning of the game
/// </summary>
public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioLoop musicLoop;
    public SFXSounds _sfxSounds;

    void Start()
    {
        musicLoop.PlayLoop();
    }
}
