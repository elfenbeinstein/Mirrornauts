using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maths : MonoBehaviour
{
    private float[] vectorResult;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Addition(float[]startV, float[]addV, bool addition)
    {
        if (addition)
        {
            vectorResult = new float[] { (startV[0] + addV[0]), (startV[1] + addV[1]) };
        }
        else
        {
            vectorResult = new float[] { (startV[0] - addV[0]), (startV[1] - addV[1]) };
        }

        Debug.Log("result is " + vectorResult[0] + ", " + vectorResult[1]);
    }

    public void Multiplication(float[]vectorM, float[]matrixM) // needs alotta work
    {
        float x = 0;
        float y = 0;

        vectorResult = new float[] { x, y };
        // multiply

    }
}
