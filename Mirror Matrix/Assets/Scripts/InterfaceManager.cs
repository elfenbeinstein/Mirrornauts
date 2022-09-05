using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Globalization;

public class InterfaceManager : MonoBehaviour
{
    [SerializeField] private GameObject addition;
    [SerializeField] private GameObject multiplication;
    [SerializeField] private GameObject scalarObject;
    [SerializeField] private GameObject vectorObject;

    [Space]
    [SerializeField] private GameObject dropdownMenu;
    private TMPro.TMP_Dropdown dropdown;
    [SerializeField] private TextMeshProUGUI buttonText;

    private Maths _maths;
    private DisplayResults _display;
    private float[] startV;
    private float[] addV;
    private float[] matrix;
    private float[] resultV;
    private float scalar;

    private bool additionValue;

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

    void Start()
    {
        addition.SetActive(true);
        vectorObject.SetActive(false);
        multiplication.SetActive(false);
        scalarObject.SetActive(false);
        resultX.text = "";
        resultY.text = "";
        additionValue = true;

        _maths = FindObjectOfType<Maths>();
        if (_maths == null)
        {
            Debug.LogWarning(gameObject + " can't find maths script");
        }
        _display = FindObjectOfType<DisplayResults>();
        if (_display == null)
        {
            Debug.LogWarning(gameObject + " can't find display script");
        }

        dropdown = dropdownMenu.GetComponent<TMPro.TMP_Dropdown>();
        if (dropdown == null)
        {
            Debug.Log(gameObject + "can't find dropdown");
        }
    }

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

    public void AdditionButton()
    {
        if (buttonText.text == "+")
        {
            buttonText.text = "-";

            additionValue = false;
        }
        else
        {
            buttonText.text = "+";

            additionValue = true;
        }
    }

    public void Calculate()
    {
        //Debug.Log("start calculation");

        float x = 0;
        float y = 0;

        // START VECTOR
        if (dropdown.value != 0)
        {
            x = 0;
            y = 0;
            if (vectorx.text == "" || vectory.text == "")
            {
                Debug.LogWarning("starting vector values missing, defaulting to zero");
            }

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
            startV = new float[] { x, y };
        }

        // ADDITION
        if (dropdown.value == 0)
        {
            x = 0;
            y = 0;
            // check all boxes are filled or default to zero
            if (startVx.text == "" || startVy.text == "" || vectorAddx.text == "" || vectorAddy.text == "")
            {
                Debug.LogWarning("missing values, defaulting to zero");
                
            }

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
            startV = new float[] { x, y };

            x = 0;
            y = 0;
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
            addV = new float[] { x, y };

            resultV = _maths.Addition(startV, addV, additionValue);

            DisplayVector();
        }
        // MULTIPLICATION
        else if (dropdown.value == 1)
        {
            if (matrixX1.text == "" || matrixX2.text == "" || matrixY1.text == "" || matrixY2.text == "")
            {
                Debug.LogWarning("missing matrix values, defaulting to zero");
            }

            x = 0;
            y = 0;
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
            matrix = new float[] { x, x2, y, y2 };

            resultV = _maths.Multiplication(startV, matrix);
            DisplayVector();
        }
        // SCALAR MULTIPLICATION
        else if (dropdown.value == 2)
        {
            x = 0;
            y = 0;

            if (scalarInput.text == "")
            {
                Debug.LogWarning(" missing values in boxes, defaulting to zero");
            }

            if (scalarInput.text != "")
            {
                scalar = float.Parse(scalarInput.text);
            }
            else
            {
                scalarInput.text = "0";
            }

            resultV = _maths.ScalarMultiplication(startV, scalar);
            DisplayVector();
        }
        else
        {
            Debug.Log("error calculation dropwdown menu option out of bounds");
        }
    }

    private void DisplayVector()
    {
        //Debug.Log("displaying vector " + resultV[0] + " / " + resultV[1]);

        resultX.text = resultV[0].ToString();
        resultY.text = resultV[1].ToString();

        _display.UpdateDisplay(startV, resultV);
    }

    public void StartVectorValueChange()
    {
        float x = 0;
        float y = 0;

        if (vectorx.text != "")
        {
            x = float.Parse(vectorx.text);
        }
        if (vectory.text != "")
        {
            y = float.Parse(vectory.text);
        }

        startV = new float[] { x, y };

        _display.UpdateSpaceshipStartV(startV);
    }
}
