using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Game Manager Script -- Singleton
/// 
/// holds reference to maths script for every other script
/// </summary>
public class GameManagement : MonoBehaviour
{
    private int level;

    public static bool gameManagerLoaded;

    public static Maths _maths;
    public static PlayerStats _playerStats;

    public static bool gameMode;
    public static AudioManager _audioManager;

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameManager");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        _maths = GetComponent<Maths>();
        if (_maths == null) Debug.Log("game manager can't find maths script");

        _playerStats = GetComponentInChildren<PlayerStats>();
        if (_playerStats == null) Debug.Log("game manager can't find player stats script");

        _audioManager = GetComponentInChildren<AudioManager>();
        if (_audioManager == null) Debug.Log("game manager can't find audio manager script");
    }

    public static void StartGameMode()
    {
        SceneManager.LoadScene(2);
    }

    public static void StartTrainingMode()
    {
        SceneManager.LoadScene(1);
    }

    public static void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public static void QuitGame()
    {
        Debug.Log("quit game");
        Application.Quit();
    }
}
