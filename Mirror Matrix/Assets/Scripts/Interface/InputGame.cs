using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputGame : MonoBehaviour
{
    private SpaceshipBehaviour _spaceshipBehaviour;

    void Start()
    {
        
    }


    public void SetSpaceshipScript(SpaceshipBehaviour script)
    {
        _spaceshipBehaviour = script;
    }

    public float[] GetStartVector()
    {
        float x = 0;
        float y = 0;

        // GET VECTOR HERE

        float[] vector = new float[] { x, y };
        return vector;
    }

    //public float[] GetAddVector() { }
    //public float[] GetMatrixValues() { }
    //public float GetScalarMultiplier() { }


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
