using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioLoop musicLoop;
    public SFXSounds _sfxSounds;

    void Start()
    {
        musicLoop.PlayLoop();
    }

}
