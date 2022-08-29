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

    [Space]
    [SerializeField] private GameObject dropdownMenu;
    private TMPro.TMP_Dropdown dropdown;
    [SerializeField] private TextMeshProUGUI buttonText;

    private Maths _maths;
    private Display _display;
    private float[] startV;
    private float[] addV;
    private float[] matrix;
    private float[] resultV;
    private bool additionValue;

    [Space]
    [Header("Addition:")]
    [SerializeField] private TMPro.TMP_InputField vectorAStartx;
    [SerializeField] private TMPro.TMP_InputField vectorAStarty;
    [SerializeField] private TMPro.TMP_InputField vectorAddx;
    [SerializeField] private TMPro.TMP_InputField vectorAddy;
    [Header("Multiplication:")]
    [SerializeField] private TMPro.TMP_InputField vectorMx;
    [SerializeField] private TMPro.TMP_InputField vectorMy;
    [SerializeField] private TMPro.TMP_InputField matrixX1;
    [SerializeField] private TMPro.TMP_InputField matrixX2;
    [SerializeField] private TMPro.TMP_InputField matrixY1;
    [SerializeField] private TMPro.TMP_InputField matrixY2;
    [Space]
    [Header("Result:")]
    [SerializeField] private TextMeshProUGUI resultX;
    [SerializeField] private TextMeshProUGUI resultY;

    void Start()
    {
        addition.SetActive(true);
        multiplication.SetActive(false);
        resultX.text = "";
        resultY.text = "";
        additionValue = true;

        dropdown = dropdownMenu.GetComponent<TMPro.TMP_Dropdown>();
        if (dropdown == null)
        {
            Debug.Log(gameObject + "can't find dropdown");
        }

        _maths = FindObjectOfType<Maths>();
        if (_maths == null)
        {
            Debug.LogWarning(gameObject + " can't find maths script");
        }
        _display = FindObjectOfType<Display>();
        if (_display == null)
        {
            Debug.LogWarning(gameObject + " can't find display script");
        }
    }

    public void DropDownMenu()
    {
        if (dropdown.value == 0)
        {
            //Debug.Log("should display addition");
            addition.SetActive(true);
            multiplication.SetActive(false);

            // missing: vektor übernehmen
        }
        else if (dropdown.value == 1)
        {
            //Debug.Log("should display multiplication");
            multiplication.SetActive(true);
            addition.SetActive(false);

            // missing: vektor übernehmen
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

        // ADDITION
        if (dropdown.value == 0)
        {
            // check all boxes are filled or default to zero
            if (vectorAStartx.text == "" || vectorAStarty.text == "" || vectorAddx.text == "" || vectorAddy.text == "")
            {
                Debug.LogWarning("please fill in all boxes, defaulting to zero");
                x = 0;
                y = 0;

                if (vectorAStartx.text != "")
                {
                    x = float.Parse(vectorAStartx.text);
                }
                if (vectorAStarty.text != "")
                {
                    y = float.Parse(vectorAStartx.text);
                }
                startV = new float[] { x, y };

                x = 0;
                y = 0;
                if (vectorAddx.text != "")
                {
                    x = float.Parse(vectorAddx.text);
                }
                if (vectorAddy.text != "")
                {
                    y = float.Parse(vectorAddy.text);
                }
                addV = new float[] { x, y };
            }
            else
            {
                startV = new float[] { float.Parse(vectorAStartx.text), float.Parse(vectorAStarty.text) };
                addV = new float[] { float.Parse(vectorAddx.text), float.Parse(vectorAddy.text) };
            }

            //_maths.Addition(startV, addV, additionValue); --> from when it was a public void not public float []
            resultV = _maths.Addition(startV, addV, additionValue);

            DisplayVector();
        }
        // MULTIPLICATION
        else if (dropdown.value == 1)
        {
            // Check all boxes are filled or default to zero
            if (vectorMx.text == "" || vectorMy.text == "" || matrixX1.text == "" || matrixX2.text == "" || matrixY1.text == "" || matrixY2.text == "")
            {
                Debug.LogWarning(" missing values in boxes, defaulting to zero");
                x = 0;
                y = 0;
                if (vectorMx.text != "")
                {
                    x = float.Parse(vectorMx.text);
                }
                if (vectorMy.text != "")
                {
                    y = float.Parse(vectorMy.text);
                }
                startV = new float[] { x, y };

                x = 0;
                y = 0;
                float x2 = 0;
                float y2 = 0;
                if (matrixX1.text != "")
                {
                    x = float.Parse(matrixX1.text);
                }
                if (matrixX2.text != "")
                {
                    x2 = float.Parse(matrixX2.text);
                }
                if (matrixY1.text != "")
                {
                    y = float.Parse(matrixY1.text);
                }
                if (matrixY2.text != "")
                {
                    y2 = float.Parse(matrixY2.text);
                }

                matrix = new float[] { x, x2, y, y2 };
            }
            else
            {
                startV = new float[] { float.Parse(vectorMx.text), float.Parse(vectorMy.text) };
                matrix = new float[] { float.Parse(matrixX1.text), float.Parse(matrixX2.text), float.Parse(matrixY1.text), float.Parse(matrixY2.text) };
            }

            //_maths.Multiplication(startV, matrix); --> from when it was a public void not public float []
            resultV = _maths.Multiplication(startV, matrix);

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
}
