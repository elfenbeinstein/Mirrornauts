using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maths : MonoBehaviour
{
    private float[] vectorResult;

    public float[] Addition(float[]startV, float[]addV, bool addition)
    {
        if (addition)
        {
            vectorResult = new float[] { (startV[0] + addV[0]), (startV[1] + addV[1]) };
        }
        else
        {
            vectorResult = new float[] { (startV[0] - addV[0]), (startV[1] - addV[1]) };
        }

        //Debug.Log("result is " + vectorResult[0] + ", " + vectorResult[1]);
        return vectorResult;
    }

    public float[] Multiplication(float[]vectorM, float[]matrixM) 
    {
        float x, y;
        // Matrix set up: x1, x2, y1, y2
        x = (matrixM[0] * vectorM[0]) + (matrixM[1] * vectorM[1]);
        y = (matrixM[2] * vectorM[0]) + (matrixM[3] * vectorM[1]);

        vectorResult = new float[] { x, y };

        //Debug.Log("result is " + vectorResult[0] + ", " + vectorResult[1]);
        return vectorResult;
    }

    public float[] ScalarMultiplication(float[]vector, float scalar)
    {
        float x, y;

        x = scalar * vector[0];
        y = scalar * vector[1];

        vectorResult = new float[] { x, y };
        return vectorResult;
    }

    public float ConvertToRadian(float value)
    {
        float result = value * Mathf.PI / 180;

        return result;
    }

    public float ConvertFromRadian(float value)
    {
        float result = value * 180 / Mathf.PI;

        return result;
    }

    public float CalculateDistance(Vector3 topPos, Vector3 spaceshipPos)
    {
        Vector3 normPos = topPos - spaceshipPos;
        float distance;

        if (normPos.x == 0)
        {
            distance = normPos.y;
        }
        
        else if (normPos.y == 0)
        {
            distance = normPos.x;
        }
        else
        {
            distance = Mathf.Sqrt(normPos.x * normPos.x + normPos.y * normPos.y);
        }

        return distance;
    }

    public float CalculateRotation(Vector3 topPos, Vector3 spaceshipPos)
    {
        Vector3 normPos = topPos - spaceshipPos;

        float angle = normPos.y / Mathf.Sqrt((normPos.x * normPos.x) + (normPos.y * normPos.y));
        angle = Mathf.Asin(angle);

        angle = ConvertFromRadian(angle);

        angle -= 90;

        return angle;
    }
}
