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
    public static bool gameMode;

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameManager");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        else DontDestroyOnLoad(this.gameObject);
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
