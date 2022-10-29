using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputGameValues : MonoBehaviour
{
    private SpaceshipBehaviour _spaceshipBehaviour;

    private float[] vectorValue;
    private float numberSlot;

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

    private void Start()
    {
        ClearMatrix();
        x1Value = true;
        x2Value = true;
        y1Value = true;
        y2Value = true;
    }

    public void SetSpaceshipScript(SpaceshipBehaviour script)
    {
        _spaceshipBehaviour = script;
        vectorValue = _spaceshipBehaviour.SpaceshipCoordinates();
        vectorx.text = vectorValue[0].ToString();
        vectory.text = vectorValue[1].ToString();
    }

    public void SetMatrix(float value)
    {
        matrixX1R.text = value.ToString();
        matrixX2R.text = value.ToString();
        matrixY1R.text = value.ToString();
        matrixY2R.text = value.ToString();
        numberSlot = value;
    }

    public void ClearMatrix()
    {
        matrixX1R.text = "?";
        matrixX2R.text = "?";
        matrixY1R.text = "?";
        matrixY2R.text = "?";
        numberSlot = 0;
    }

    public float[] GetMatrixValues() 
    {
        float x = 0;
        float y = 0;
        float x2 = 0;
        float y2 = 0;

        x = Mathf.Cos(numberSlot * Mathf.PI);
        if (!x1Value) x *= -1;

        x2 = Mathf.Sin(numberSlot * Mathf.PI);
        if (!x2Value) x2 *= -1;

        y = Mathf.Sin(numberSlot * Mathf.PI);
        if (!y1Value) y *= -1;

        y2 = Mathf.Cos(numberSlot * Mathf.PI);
        if (!y2Value) y2 *= -1;

        float[] matrix = new float[] { x, x2, y, y2 };

        //Debug.Log($"Matrix values: {x}, {x2}, {y}, {y2}");
        return matrix;
    }

    public void WriteNewSpaceshipPos(float x, float y)
    {

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
