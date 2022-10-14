using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    private int level;

    public static bool gameManagerLoaded;

    public static Maths _maths;
    
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
    }

}
