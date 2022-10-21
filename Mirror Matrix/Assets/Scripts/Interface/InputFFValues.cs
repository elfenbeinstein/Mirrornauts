using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Input System for Values in Free Flow Mode
/// </summary>
public enum CalculationType
{
    Addition,
    MatrixMultiplicationF,
    MatrixMultiplicationR,
    ScalarMultiplication,
    MatrixMultiplicationG
}

public class InputFFValues : MonoBehaviour
{
    // input fields:
    [Space]
    [Header("Starting Vector")]
    [SerializeField] private TMPro.TMP_InputField vectorx;
    [SerializeField] private TMPro.TMP_InputField vectory;
    [Header("Addition:")]
    [SerializeField] private TMPro.TMP_InputField vectorAddx;
    [SerializeField] private TMPro.TMP_InputField vectorAddy;
    [SerializeField] private TMPro.TMP_InputField startVx;
    [SerializeField] private TMPro.TMP_InputField startVy;
    [Header("Matrix Free Values:")]
    [SerializeField] private TMPro.TMP_InputField matrixX1F;
    [SerializeField] private TMPro.TMP_InputField matrixX2F;
    [SerializeField] private TMPro.TMP_InputField matrixY1F;
    [SerializeField] private TMPro.TMP_InputField matrixY2F;
    [Header("Matrix Radian Values:")]
    [SerializeField] private TMPro.TMP_InputField matrixX1R;
    [SerializeField] private TMPro.TMP_InputField matrixX2R;
    [SerializeField] private TMPro.TMP_InputField matrixY1R;
    [SerializeField] private TMPro.TMP_InputField matrixY2R;
    [Header("Scalar:")]
    [SerializeField] private TMPro.TMP_InputField scalarInput;
    [Space]
    [Header("Result:")]
    [SerializeField] private TextMeshProUGUI resultX;
    [SerializeField] private TextMeshProUGUI resultY;

    private SpaceshipBehaviour _spaceshipBehaviour;
    
    // values relevant for calculation:
    [HideInInspector] public bool additionValue; // if true shows + in calc; if false -
    [HideInInspector] public bool x1Value; // if true +, if false -
    [HideInInspector] public bool x2Value;
    [HideInInspector] public bool y1Value;
    [HideInInspector] public bool y2Value;
    private CalculationType calcType;

    void Start()
    {
        resultX.text = "";
        resultY.text = "";
        additionValue = true;
    }

    public void SetSpaceshipScript(SpaceshipBehaviour script)
    {
        _spaceshipBehaviour = script;
    }

    public void SetCalcType(CalculationType type)
    {
        calcType = type;
    }

    //called from start vector fields
    public void StartVectorValueChange()
    {
        float[] vectorValue = GetStartVector();
        _spaceshipBehaviour.MoveSpaceship(vectorValue);
        /*
        vectorValue = _spaceshipBehaviour.ShipTopCoordinates();
        Debug.Log("current ship top pos = " + vectorValue[0] + ", " + vectorValue[1]);
        */
    }

    // called from button
    public void ResetRotation()
    {
        _spaceshipBehaviour.ResetRotation();
    }

    // called from button
    public void ResetStart() 
    {
        float[] value = _spaceshipBehaviour.SpaceshipCoordinates();
        
        startVx.text = value[0].ToString();
        startVy.text = value[1].ToString();

        vectorx.text = value[0].ToString();
        vectory.text = value[1].ToString();

        resultX.text = "";
        resultY.text = "";
    }

    // called from button
    public void ResetSpaceship()
    {
        ResetRotation();

        // resert position to 0,0

        startVx.text = "0";
        startVy.text = "0";

        vectorx.text = "0";
        vectory.text = "0";

        resultX.text = "";
        resultY.text = "";

        float[] vector = new float[] { 0, 0 };
        _spaceshipBehaviour.MoveSpaceship(vector);
        _spaceshipBehaviour.RemoveLines();
    }

    public void WriteResultVector(float[] result)
    {
        resultX.text = result[0].ToString();
        resultY.text = result[1].ToString();
    }

    public float[] GetStartVector()
    {
        float x = 0;
        float y = 0;

        if (calcType != CalculationType.Addition)
        {
            if (vectorx.text != "")
            {
                x = float.Parse(vectorx.text);
            }
            else
            {
                vectorx.text = "0";
            }
            if (vectory.text != "")
            {
                y = float.Parse(vectory.text);
            }
            else
            {
                vectory.text = "0";
            }
        }
        else
        {
            if (startVx.text != "")
            {
                x = float.Parse(startVx.text);
            }
            else
            {
                startVx.text = "0";
            }
            if (startVy.text != "")
            {
                y = float.Parse(startVy.text);
            }
            else
            {
                startVy.text = "0";
            }
        }
        float[] startVectorValue = new float[] { x, y };
        return startVectorValue;
    }

    public float[] GetAddVector()
    {
        float x = 0;
        float y = 0;

        if (vectorAddx.text != "")
        {
            x = float.Parse(vectorAddx.text);
        }
        else
        {
            vectorAddx.text = "0";
        }
        if (vectorAddy.text != "")
        {
            y = float.Parse(vectorAddy.text);
        }
        else
        {
            vectorAddy.text = "0";
        }

        float[] addVectorValue = new float[] { x, y };
        return addVectorValue;
    }

    public float[] GetMatrixValuesF()
    {
        if (matrixX1F.text == "" || matrixX2F.text == "" || matrixY1F.text == "" || matrixY2F.text == "")
        {
            Debug.LogWarning("missing matrix values, defaulting to zero");
        }

        float x = 0;
        float y = 0;
        float x2 = 0;
        float y2 = 0;

        if (matrixX1F.text != "")
        {
            x = float.Parse(matrixX1F.text);
        }
        else matrixX1F.text = "0";

        if (matrixX2F.text != "")
        {
            x2 = float.Parse(matrixX2F.text);
        }
        else matrixX2F.text = "0";

        if (matrixY1F.text != "")
        {
            y = float.Parse(matrixY1F.text);
        }
        else matrixY1F.text = "0";

        if (matrixY2F.text != "")
        {
            y2 = float.Parse(matrixY2F.text);
        }
        else matrixY2F.text = "0";

        float[] matrix = new float[] { x, x2, y, y2 };

        return matrix;
    } 

    public float[] GetMatrixValuesA()
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
            x = float.Parse(matrixX1R.text) * Mathf.PI;
            x = Mathf.Cos(x);
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

    public float GetScalarMultiplier()
    {
        float x = 0;

        if (scalarInput.text == "")
        {
            Debug.LogWarning(" missing scalar value in boxes, defaulting to zero");
            scalarInput.text = "0";
        }
        else
        {
            x = float.Parse(scalarInput.text);
        }

        return x;
    }

    public CalculationType GetCalculationType()
    {
        return calcType;
    }

    public bool AdditionValue()
    {
        return additionValue;
    }




    [ContextMenu("Sin cos tryout")]
    public void TrySinCos()
    {
        float a = 0;
        float cos = GameManagement._maths.CosinusCalc(a);
        float sin = GameManagement._maths.SinusCalc(a);
        Debug.Log($"Sin({a}*PI) = {sin}; Cos({a}*PI) = {cos}");

        a = 0.5f;
        cos = GameManagement._maths.CosinusCalc(a);
        sin = GameManagement._maths.SinusCalc(a);
        Debug.Log($"Sin({a}*PI) = {sin}; Cos({a}*PI) = {cos}");

        a = 1;
        cos = GameManagement._maths.CosinusCalc(a);
        sin = GameManagement._maths.SinusCalc(a);
        Debug.Log($"Sin({a}*PI) = {sin}; Cos({a}*PI) = {cos}");

        a = 1.5f;
        cos = GameManagement._maths.CosinusCalc(a);
        sin = GameManagement._maths.SinusCalc(a);
        Debug.Log($"Sin({a}) = {sin}; Cos({a}) = {cos}");

        a = 2;
        cos = GameManagement._maths.CosinusCalc(a);
        sin = GameManagement._maths.SinusCalc(a);
        Debug.Log($"Sin({a}*PI) = {sin}; Cos({a}*PI) = {cos}");
    }
}
