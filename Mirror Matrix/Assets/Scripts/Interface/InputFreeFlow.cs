using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Input System for Values in Free Flow Mode
/// </summary>

public class InputFreeFlow : MonoBehaviour
{
    // GameObjects that contain all input elements in free flow mode
    [SerializeField] private GameObject addition; 
    [SerializeField] private GameObject multiplication;
    [SerializeField] private GameObject scalarObject;
    [SerializeField] private GameObject vectorObject;

    // elements that adjust the input system 
    [Space]
    [SerializeField] private GameObject dropdownMenu;
    private TMPro.TMP_Dropdown dropdown;
    [SerializeField] private TextMeshProUGUI additionButtonText; // + or -

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
    [Header("Multiplication:")]
    [SerializeField] private TMPro.TMP_InputField matrixX1;
    [SerializeField] private TMPro.TMP_InputField matrixX2;
    [SerializeField] private TMPro.TMP_InputField matrixY1;
    [SerializeField] private TMPro.TMP_InputField matrixY2;
    [Header("Scalar:")]
    [SerializeField] private TMPro.TMP_InputField scalarInput;
    [Space]
    [Header("Result:")]
    [SerializeField] private TextMeshProUGUI resultX;
    [SerializeField] private TextMeshProUGUI resultY;

    private SpaceshipBehaviour _spaceshipBehaviour;
    
    private bool additionValue; // if Positive shows + in calc; if negative -

    void Start()
    {
        addition.SetActive(true);
        vectorObject.SetActive(false);
        multiplication.SetActive(false);
        scalarObject.SetActive(false);

        resultX.text = "";
        resultY.text = "";
        additionValue = true;

        dropdown = dropdownMenu.GetComponent<TMPro.TMP_Dropdown>();
        if (dropdown == null)
        {
            Debug.Log(gameObject + "can't find dropdown");
        }
    }

    public void SetSpaceshipScript(SpaceshipBehaviour script)
    {
        _spaceshipBehaviour = script;
    }

    // called from menu
    public void DropDownMenu()
    {
        if (dropdown.value == 0)
        {
            addition.SetActive(true);
            vectorObject.SetActive(false);
            multiplication.SetActive(false);
            scalarObject.SetActive(false);
        }
        else if (dropdown.value == 1)
        {
            multiplication.SetActive(true);
            vectorObject.SetActive(true);
            scalarObject.SetActive(false);
            addition.SetActive(false);
        }
        else if (dropdown.value == 2)
        {
            scalarObject.SetActive(true);
            vectorObject.SetActive(true);
            multiplication.SetActive(false);
            addition.SetActive(false);
        }
        else
        {
            Debug.Log("error in dropdown menu");
        }
    }

    // called from button
    public void AdditionButton()
    {
        if (additionButtonText.text == "+")
        {
            additionButtonText.text = "-";

            additionValue = false;
        }
        else
        {
            additionButtonText.text = "+";

            additionValue = true;
        }
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

        if (dropdown.value != 0)
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

    public float[] GetMatrixValues()
    {
        if (matrixX1.text == "" || matrixX2.text == "" || matrixY1.text == "" || matrixY2.text == "")
        {
            Debug.LogWarning("missing matrix values, defaulting to zero");
        }

        float x = 0;
        float y = 0;
        float x2 = 0;
        float y2 = 0;

        if (matrixX1.text != "")
        {
            x = float.Parse(matrixX1.text);
        }
        else
        {
            matrixX1.text = "0";
        }
        if (matrixX2.text != "")
        {
            x2 = float.Parse(matrixX2.text);
        }
        else
        {
            matrixX2.text = "0";
        }
        if (matrixY1.text != "")
        {
            y = float.Parse(matrixY1.text);
        }
        else
        {
            matrixY1.text = "0";
        }
        if (matrixY2.text != "")
        {
            y2 = float.Parse(matrixY2.text);
        }
        else
        {
            matrixY2.text = "0";
        }
        float[] matrix = new float[] { x, x2, y, y2 };

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

    public int GetCalculationType()
    {
        return dropdown.value;
    }

    public bool AdditionValue()
    {
        return additionValue;
    }
}
