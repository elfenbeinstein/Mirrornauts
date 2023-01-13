using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MatrixType
{
    Dreh,
    Spiegel,
    Falsch
}

public class InputGameValues : MonoBehaviour
{
    private SpaceshipBehaviour _spaceshipBehaviour;
    private PlayerStats _playerStats;

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

    [HideInInspector] public int energyNeeded;

    float fX, fX2, fY, fY2, aX, aY; // needed since input update with new fields

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

        _playerStats = GetComponent<InterfaceManager>()._playerStats;
    }

    public void SetSpaceshipScript(SpaceshipBehaviour script)
    {
        _spaceshipBehaviour = script;
        vectorValue = _spaceshipBehaviour.SpaceshipCoordinates();
        WriteNewSpaceshipPos(vectorValue[0], vectorValue[1]);
    }

    public void SetSlot(float value, SlotType type, string text)
    {
        switch(type)
        {
            case SlotType.MatrixRadAll:
                SetMatrix(value, text);
                break;
            case SlotType.MatrixAX1:
                mFreeX1R.text = text;
                fX = value;
                break;
            case SlotType.MatrixAX2:
                mFreeX2R.text = text;
                fX2 = value;
                break;
            case SlotType.MatrixAY1:
                mFreeY1R.text = text;
                fY = value;
                break;
            case SlotType.MatrixAY2:
                mFreeY2R.text = text;
                fY2 = value;
                break;
            case SlotType.AddX:
                addX.text = text;
                aX = value;
                break;
            case SlotType.AddY:
                addY.text = text;
                aY = value;
                break;
            default:
                Debug.Log("case switch not proper");
                break;
        }

        if (AllValuesSet())
        {
            EnergyNeeded();
            EventManager.Instance.EventGo("ENERGY", "EnergyCost");
        }
    }

    public void SetMatrix(float value, string text)
    {
        matrixX1R.text = text;
        matrixX2R.text = text;
        matrixY1R.text = text;
        matrixY2R.text = text;
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
        EventManager.Instance.EventGo("ENERGY", "RemoveCost");
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
        /*
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
        */

        // values are set when number is dragged
        float[] matrix = new float[] { fX, fX2, fY, fY2 };
        return matrix;
    }

    public float[] GetAddVector()
    {
        /*
        x = 0;
        y = 0;

        x = float.Parse(addX.text);
        if (!addXValue) x *= -1;
        y = float.Parse(addY.text);
        if (!addYValue) y *= -1;
        */

        // values are set with number drop
        vectorValue = new float[] { aX, aY };
        return vectorValue;
    }

    public bool AdditionValue()
    {
        return addValue;
    }

    public void WriteNewSpaceshipPos(float x, float y)
    {
        string xValue = x.ToString("F2");
        string yValue = y.ToString("F2");

        // in jedem Textfeld für den Spaceship Vektor anzeigen:
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

    public bool AllValuesSet()
    {
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

    public bool GameReady()
    {
        if (AllValuesSet())
        {
            if (calcType == CalculationType.Addition) return true;
            else
            {
                if (GetMatrixType() != MatrixType.Falsch) return true;
                else return false;
            }
        }
        else return false;
    }

    [ContextMenu("matrix type calc")]
    public MatrixType GetMatrixType()
    {
        MatrixType type;

        float[] matrix;
        if (calcType == CalculationType.MatrixMultiplicationF) matrix = GetMatrixValuesF();
        else if (calcType == CalculationType.MatrixMultiplicationR) matrix = GetMatrixValuesR();
        else matrix = GetMatrixValuesR();

        if (matrix[0] == matrix[3] && matrix[1] == -matrix[2] && matrix[0] * matrix[3] - matrix[1] * matrix[2] >= 0.9999f && matrix[0] * matrix[3] - matrix[1] * matrix[2] <= 1.0009f)
        {
            type = MatrixType.Dreh;
        }
        else if (matrix[1] == matrix[2] && matrix[0] == -matrix[3] && matrix[0] * matrix[3] - matrix[1] * matrix[2] >= -1.0009f && matrix[0] * matrix[3] - matrix[1] * matrix[2] <= -0.9999f) type = MatrixType.Spiegel;
        else type = MatrixType.Falsch;

        //Debug.Log($"Matrix: {matrix[0]}, {matrix[1]}, {matrix[2]}, {matrix[3]}");
        //Debug.Log("type = " + type);

        return type;
    }

    public void EnergyNeeded()
    {
        int cost = 0;
        float[] startV = _spaceshipBehaviour.SpaceshipCoordinates();
        Vector3 startPos = new Vector3(startV[0], startV[1], 0);

        if (calcType == CalculationType.Addition)
        {
            // get new position of spaceship
            float[] result = Maths.Instance.Addition(startV, GetAddVector(), addValue);
            Vector3 endPos = new Vector3(result[0], result[1], 0);

            // calculate distance between new and current position
            float distance = Vector3.Distance(startPos, endPos);

            // transfer to energy cost
            cost = Mathf.CeilToInt(distance * 2);
        }
        else
        {
            MatrixType type = GetMatrixType();

            if (type == MatrixType.Dreh)
            {
                if (calcType == CalculationType.MatrixMultiplicationR)
                {
                    if (matrixX1R.text == "1/6" || matrixX1R.text == "-1/6")
                        cost = 2;
                    else if (matrixX1R.text == "1/4" || matrixX1R.text == "-1/4")
                        cost = 3;
                    else if (matrixX1R.text == "1/3" || matrixX1R.text == "-1/3")
                        cost = 4;
                    else if (matrixX1R.text == "1/2" || matrixX1R.text == "-1/2")
                        cost = 6;
                    else if (matrixX1R.text == "2/3" || matrixX1R.text == "-2/3")
                        cost = 8;
                    else if (matrixX1R.text == "3/4" || matrixX1R.text == "-3/4")
                        cost = 9;
                    else if (matrixX1R.text == "5/6" || matrixX1R.text == "-5/6")
                        cost = 10;
                    else if (matrixX1R.text == "1" || matrixX1R.text == "-1")
                        cost = 12;
                }
                else
                {
                    if (mFreeX1R.text == "1")
                        cost = 0;
                    else if (mFreeX1R.text == "(√3)/2")
                        cost = 2;
                    else if (mFreeX1R.text == "(√2)/2")
                        cost = 3;
                    else if (mFreeX1R.text == "1/2")
                        cost = 4;
                    else if (mFreeX1R.text == "0")
                        cost = 6;
                    else if (mFreeX1R.text == "-1/2")
                        cost = 8;
                    else if (mFreeX1R.text == "-(√2)/2")
                        cost = 9;
                    else if (mFreeX1R.text == "-(√3)/2")
                        cost = 10;
                    else if (mFreeX1R.text == "-1")
                        cost = 12;
                }
            }
            else if (type == MatrixType.Spiegel)
            {
                float[] matrix = GetMatrixValuesR();
                if (calcType == CalculationType.MatrixMultiplicationF) matrix = GetMatrixValuesF();

                // get new position of spaceship
                float[] result = Maths.Instance.Multiplication(startV, matrix);
                Vector3 endPos = new Vector3(result[0], result[1], 0);

                // calculate distance between new and current position
                float distance = Vector3.Distance(startPos, endPos);

                // transfer to energy cost
                cost = Mathf.CeilToInt(distance * 2);
            }
            else
            {
                cost = 1000;
            }
        }

        _playerStats.energyNeeded = cost;
    }

    public void ResetSpaceshipFromButton()
    {
        _spaceshipBehaviour.ResetRotation();
    }
}
