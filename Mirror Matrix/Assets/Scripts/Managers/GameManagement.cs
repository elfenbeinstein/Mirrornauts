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

    public static int dashAmount;
    public static int shieldAmount;
    public static bool shieldActive;
    public static int energy;
    public static int currentHealth;
    public static bool gameMode;

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameManager");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        _maths = GetComponent<Maths>();

        if (_maths == null)
        {
            Debug.Log("game manager can't find maths script");
        }

        dashAmount = 0;
        shieldActive = false;
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
