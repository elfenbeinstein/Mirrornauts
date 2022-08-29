using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maths : MonoBehaviour
{
    private float[] vectorResult;

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

    public void Multiplication(float[]vectorM, float[]matrixM) 
    {
        float x, y;
        // Matrix set up: x1, x2, y1, y2
        x = (matrixM[0] * vectorM[0]) + (matrixM[1] * vectorM[1]);
        y = (matrixM[2] * vectorM[0]) + (matrixM[3] * vectorM[1]);

        vectorResult = new float[] { x, y };

        Debug.Log("result is " + vectorResult[0] + ", " + vectorResult[1]);
    }
}
