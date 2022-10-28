using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputGameValues : MonoBehaviour
{
    private SpaceshipBehaviour _spaceshipBehaviour;

    private float x, y, x2, y2;
    private float[] vectorValue;

    [Header("Vector Values:")]
    [SerializeField] private TMPro.TextMeshProUGUI vectorx;
    [SerializeField] private TMPro.TextMeshProUGUI vectory;
    [Header("Matrix Radian Values:")]
    [SerializeField] private TMPro.TextMeshProUGUI matrixX1R;
    [SerializeField] private TMPro.TextMeshProUGUI matrixX2R;
    [SerializeField] private TMPro.TextMeshProUGUI matrixY1R;
    [SerializeField] private TMPro.TextMeshProUGUI matrixY2R;

    [HideInInspector] public bool x1Value; // if true +, if false -
    [HideInInspector] public bool x2Value;
    [HideInInspector] public bool y1Value;
    [HideInInspector] public bool y2Value;
    private CalculationType calcType;

    public void SetSpaceshipScript(SpaceshipBehaviour script)
    {
        _spaceshipBehaviour = script;
        vectorValue = _spaceshipBehaviour.SpaceshipCoordinates();
        vectorx.text = vectorValue[0].ToString();
        vectory.text = vectorValue[1].ToString();
    }

    public void ClearMatrix()
    {
        matrixX1R.text = "?";
        matrixX2R.text = "?";
        matrixY1R.text = "?";
        matrixY2R.text = "?";
    }

    public float[] GetMatrixValues() 
    {
        if (matrixX1R.text == "" || matrixX2R.text == "" || matrixY1R.text == "" || matrixY2R.text == "")
        {
            Debug.LogWarning("missing matrix values, defaulting to zero");
        }

        float x = 0;
        float y = 0;
        float x2 = 0;
        float y2 = 0;

        if (matrixX1R.text != "")
        {
            x = float.Parse(matrixX1R.text);
            x = Mathf.Cos(x * Mathf.PI);
            if (!x1Value) x *= -1;
        }
        else matrixX1R.text = "0";

        if (matrixX2R.text != "")
        {
            x2 = float.Parse(matrixX2R.text);
            x2 = Mathf.Sin(x2 * Mathf.PI);
            if (!x2Value) x2 *= -1;
        }
        else matrixX2R.text = "0";

        if (matrixY1R.text != "")
        {
            y = float.Parse(matrixY1R.text);
            y = Mathf.Sin(y * Mathf.PI);
            if (!y1Value) y *= -1;
        }
        else matrixY1R.text = "0";

        if (matrixY2R.text != "")
        {
            y2 = float.Parse(matrixY2R.text);
            y2 = Mathf.Cos(y2 * Mathf.PI);
            if (!y2Value) y2 *= -1;
        }
        else matrixY2R.text = "0";

        float[] matrix = new float[] { x, x2, y, y2 };

        //Debug.Log($"Matrix values: {x}, {x2}, {y}, {y2}");
        return matrix;
    }

    public CalculationType GetCalculationType()
    {
        // elfenbeinstein MISSING calculation type (if necessary)

        return CalculationType.MatrixMultiplicationG;
    }

    /* -- potentially not necessary
    public float[] GetAddVector()
    {
        x = 0;
        y = 0;

        // elfenbeinstein MISSING get addition vector

        vectorValue = new float[] { x, y };
        return vectorValue;
    }
    public bool AdditionValue()
    {
        // elfenbeinstein MISSING addition value

        return true;
    }
    public float GetScalarMultiplier()
    {
        x = 0;
        // elfenbeinstein MISSING get scalar Multiplier if necessary

        return x;
    }
    */
}
