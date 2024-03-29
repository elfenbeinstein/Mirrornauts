using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Game Manager Script -- Don't destroy on load
/// Loads Scenes holds public available bool which gameMode it is
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
        else
        {
            Application.targetFrameRate = 60;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public static void StartGameMode()
    {
        SceneManager.LoadScene(2);
        EventManager.Instance.EventGo("DATA", "CountGame", true);
        gameMode = true;
    }

    public static void StartTrainingMode()
    {
        SceneManager.LoadScene(1);
        EventManager.Instance.EventGo("DATA", "CountTrain", true);
        gameMode = false;
    }

    public static void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
        EventManager.Instance.EventGo("DATA", "CountGame", false);
        EventManager.Instance.EventGo("DATA", "CountTrain", false);
    }

    public static void QuitGame()
    {
        Debug.Log("quit game");
        Application.Quit();
    }
}
