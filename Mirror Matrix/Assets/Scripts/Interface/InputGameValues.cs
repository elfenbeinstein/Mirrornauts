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
    [SerializeField] private TMPro.TextMeshProUGUI[] vectorXs;
    [SerializeField] private TMPro.TextMeshProUGUI[] vectorYs;

    [Header("Matrix Radian Values:")]
    [SerializeField] private TMPro.TextMeshProUGUI matrixX1R;
    [SerializeField] private TMPro.TextMeshProUGUI matrixX2R;
    [SerializeField] private TMPro.TextMeshProUGUI matrixY1R;
    [SerializeField] private TMPro.TextMeshProUGUI matrixY2R;

    [Header("Matrix Free Values:")]
    [SerializeField] private TMPro.TextMeshProUGUI mFreeX1R;
    [SerializeField] private TMPro.TextMeshProUGUI mFreeX2R;
    [SerializeField] private TMPro.TextMeshProUGUI mFreeY1R;
    [SerializeField] private TMPro.TextMeshProUGUI mFreeY2R;

    [Header("Addition Values:")]
    [SerializeField] private TMPro.TextMeshProUGUI addX;
    [SerializeField] private TMPro.TextMeshProUGUI addY;

    [HideInInspector] public bool x1Value; // if true +, if false -
    [HideInInspector] public bool x2Value;
    [HideInInspector] public bool y1Value;
    [HideInInspector] public bool y2Value;
    [HideInInspector] public CalculationType calcType;

    [HideInInspector] public bool x1FValue;
    [HideInInspector] public bool x2FValue;
    [HideInInspector] public bool y1FValue;
    [HideInInspector] public bool y2FValue;

    float x, y, x2, y2;
    [HideInInspector] public bool addValue;
    [HideInInspector] public bool addXValue;
    [HideInInspector] public bool addYValue;

    private void Start()
    {
        ClearMatrix();
        x1Value = true;
        x2Value = true;
        y1Value = true;
        y2Value = true;
        x1FValue = true;
        x2FValue = true;
        y1FValue = true;
        y2FValue = true;
        addValue = true;
        addXValue = true;
        addYValue = true;
    }

    public void SetSpaceshipScript(SpaceshipBehaviour script)
    {
        _spaceshipBehaviour = script;
        vectorValue = _spaceshipBehaviour.SpaceshipCoordinates();
        WriteNewSpaceshipPos(vectorValue[0], vectorValue[1]);
    }

    public void SetSlot(float value, SlotType type)
    {
        switch(type)
        {
            case SlotType.MatrixRadAll:
                SetMatrix(value);
                break;
            case SlotType.MatrixAX1:
                mFreeX1R.text = value.ToString();
                break;
            case SlotType.MatrixAX2:
                mFreeX2R.text = value.ToString();
                break;
            case SlotType.MatrixAY1:
                mFreeY1R.text = value.ToString();
                break;
            case SlotType.MatrixAY2:
                mFreeY2R.text = value.ToString();
                break;
            case SlotType.AddX:
                addX.text = value.ToString();
                break;
            case SlotType.AddY:
                addY.text = value.ToString();
                break;
            default:
                Debug.Log("case switch not proper");
                break;
        }
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
        if (calcType == CalculationType.MatrixMultiplicationR)
        {
            matrixX1R.text = "?";
            matrixX2R.text = "?";
            matrixY1R.text = "?";
            matrixY2R.text = "?";
            numberSlot = 0;
        }
        else if (calcType == CalculationType.MatrixMultiplicationF)
        {
            mFreeX1R.text = "?";
            mFreeX2R.text = "?";
            mFreeY1R.text = "?";
            mFreeY2R.text = "?";
            x = 0;
            x2 = 0;
            y = 0;
            y2 = 0;
        }
        else if (calcType == CalculationType.Addition)
        {
            addX.text = "?";
            addY.text = "?";
        }

        // elfenbeinstein MISSING: get energy back
    }

    public float[] GetMatrixValuesR() 
    {
        x = 0;
        y = 0;
        x2 = 0;
        y2 = 0;

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

    public float[] GetMatrixValuesF()
    {
        x = 0;
        y = 0;
        x2 = 0;
        y2 = 0;

        x = float.Parse(mFreeX1R.text);
        if (!x1FValue) x *= -1;
        x2 = float.Parse(mFreeX2R.text);
        if (!x2FValue) x2 *= -1;
        y = float.Parse(mFreeY1R.text);
        if (!y1FValue) y *= -1;
        y2 = float.Parse(mFreeY2R.text);
        if (!y2FValue) y2 *= -1;

        float[] matrix = new float[] { x, x2, y, y2 };
        return matrix;
    }

    public float[] GetAddVector()
    {
        x = 0;
        y = 0;

        x = float.Parse(addX.text);
        if (!addXValue) x *= -1;
        y = float.Parse(addY.text);
        if (!addYValue) y *= -1;

        vectorValue = new float[] { x, y };
        return vectorValue;
    }

    public bool AdditionValue()
    {
        return addValue;
    }

    public void WriteNewSpaceshipPos(float x, float y)
    {
        string xValue = x.ToString();
        string yValue = y.ToString();

        if (xValue.Contains(","))
        {
            double value = System.Math.Round(x, 2);
            xValue = value.ToString();
        }
        if (yValue.Contains(","))
        {
            double value = System.Math.Round(y, 2);
            yValue = value.ToString();
        }
        //vectorx.text = xValue;
        //vectory.text = yValue;

        for (int i = 0; i < vectorXs.Length; i++)
        {
            if (vectorXs[i] != null) vectorXs[i].text = xValue;
        }
        for (int i = 0; i < vectorYs.Length; i++)
        {
            if (vectorYs[i] != null) vectorYs[i].text = yValue;
        }
    }

    public CalculationType GetCalculationType()
    {
        return calcType;
    }

    public bool IsGameReady()
    {
        calcType = GetCalculationType();

        if (calcType == CalculationType.MatrixMultiplicationR)
        {
            if (matrixX1R.text == "?") return false;
            else if (matrixX2R.text == "?") return false;
            else if (matrixY1R.text == "?") return false;
            else if (matrixY2R.text == "?") return false;
            else return true;
        }
        else if (calcType == CalculationType.Addition)
        {
            if (addX.text == "?") return false;
            else if (addY.text == "?") return false;
            else return true;
        }
        else if (calcType == CalculationType.MatrixMultiplicationF)
        {
            if (mFreeX1R.text == "?") return false;
            else if (mFreeX2R.text == "?") return false;
            else if (mFreeY1R.text == "?") return false;
            else if (mFreeY2R.text == "?") return false;
            else return true;
        }
        else return false;
    }

    [ContextMenu("test rounding")]
    public void TestRounding()
    {
        float x = 3.2385f;
        float y = 2f;
        WriteNewSpaceshipPos(x, y);
    }

    public void ResetSpaceshipFromButton()
    {
        _spaceshipBehaviour.ResetRotation();
    }
}
