using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputGame : MonoBehaviour
{
    private SpaceshipBehaviour _spaceshipBehaviour;

    private int x, y, x2, y2;
    private float[] vectorValue;

    void Start()
    {
        
    }


    public void SetSpaceshipScript(SpaceshipBehaviour script)
    {
        _spaceshipBehaviour = script;
    }

    public float[] GetAddVector() 
    {
        x = 0;
        y = 0;

        // elfenbeinstein MISSING get addition vector

        vectorValue = new float[] { x, y };
        return vectorValue;
    }

    public float[] GetMatrixValues() 
    {
        x = 0;
        x2 = 0;
        y = 0;
        y2 = 0;

        // elfenbeinstein MISSING matrix values

        vectorValue = new float[] { x, x2, y, y2 };
        return vectorValue;
    }

    public float GetScalarMultiplier() 
    {
        x = 0;
        // elfenbeinstein MISSING get scalar Multiplier

        return x;
    }


    public int GetCalculationType()
    {
        // elfenbeinstein MISSING calculation type 

        return 0;
    }

    public bool AdditionValue()
    {
        // elfenbeinstein MISSING addition value

        return true;
    }

}
