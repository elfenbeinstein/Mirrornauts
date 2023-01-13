using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// does all important calculations for the Spaceship Movement
/// </summary>
public class Maths
{
    private static Maths instance;

    public static Maths Instance
    {
        get
        {
            if (instance == null) instance = new Maths();
            return instance;
        }
    }

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

        //float x, y;

        if (normPos.x == 0)
        {
            distance = normPos.y;

            if (normPos.y < 0)
            {
                distance *= -1;
            }
        }
        else if (normPos.y == 0)
        {
            distance = normPos.x;
            if (normPos.x < 0)
            {
                distance *= -1;
            }
        }
        else
        {
            distance = Mathf.Sqrt((normPos.x * normPos.x) + (normPos.y * normPos.y));
        }
        //Debug.Log($"distance is {distance}");

        return distance;
    }

    public float CalculateRotation(Vector3 topPos, Vector3 spaceshipPos)
    {
        Vector3 normPos = topPos - spaceshipPos;
        float angle;

        if (normPos.x == 0)
        {
            if (normPos.y < 0)
            {
                angle = 180;
            }
            else
            {
                angle = 0;
            }
        }
        else if (normPos.y == 0)
        {
            if (normPos.x < 0)
            {
                angle = 90;
            }
            else
            {
                angle = 270;
            }
        }
        else
        {
            /*
            angle = normPos.y / Mathf.Sqrt((normPos.x * normPos.x) + (normPos.y * normPos.y));
            angle = Mathf.Asin(angle);

            angle = ConvertFromRadian(angle);

            angle -= 90;*/

            angle = normPos.x / normPos.y;
            angle = Mathf.Abs(angle);

            angle = Mathf.Atan(angle);

            angle = ConvertFromRadian(angle);

            if (normPos.x > 0)
            {
                if (normPos.y > 0)
                {
                    angle = 360 - angle;
                }
                else
                {
                    angle = 180 + angle;
                }
            }
            else
            {
                if (normPos.y < 0)
                {
                    angle = 180 - angle;
                }
            }
        }

        //Debug.Log($"angle is {angle}");
        return angle;
    }

    public float CalculateScaleX(Vector3 rightPos, Vector3 topPos)
    {
        float s = 0;

        s = rightPos.x - topPos.x;

        if (s < 0) s *= -1;

        return s;
    }

    // FOR TESTING:
    public float SinusCalc(float value)
    {
        float result = Mathf.Sin(value * Mathf.PI);
        if (result < -1) result = 0;
        if (result > 1) result = 0;
        return result;
    }

    public float CosinusCalc(float value)
    {
        float result = Mathf.Cos(value * Mathf.PI);
        if (result < -1) result = 0;
        if (result > 1) result = 0;
        return result;
    }
}
