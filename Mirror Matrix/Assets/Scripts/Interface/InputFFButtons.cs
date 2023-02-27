using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// handles button / menu behaviour for the game in free flow mode
/// exceptions: reset spaceship and everything that's connected to values in input fields --> InputFFValues
/// 
/// the dropdown menu decides which Input Elements are active
/// the addition button changes whether it's an addition or subtraction
/// 
/// allows to switch between inputting the matrix values freely as numbers
/// or as radians e.g. (cos(x*PI))
/// 
/// </summary>

public class InputFFButtons : MonoBehaviour
{
    [Header("Input Elements - General:")]
    [SerializeField] private GameObject addition;
    [SerializeField] private GameObject matrix;
    [SerializeField] private GameObject scalarObject;
    [SerializeField] private GameObject vectorObject; // for matrix and scalar (addition has its own start vector)
    [Space]
    [SerializeField] private GameObject dropdownMenu;
    private TMPro.TMP_Dropdown dropdown;
    [SerializeField] private TextMeshProUGUI additionButtonText;
    [Space]
    [Header("Matrix Multiplication:")]
    [SerializeField] private GameObject matrixFreeValues; // Input Fields for all Matrix Values
    [SerializeField] private GameObject matrixRadianValues; // sin or cos of (InputField * PI)
    [SerializeField] private GameObject x1Minus; // to change the pos/neg value of the field
    [SerializeField] private GameObject x2Minus;
    [SerializeField] private GameObject y1Minus;
    [SerializeField] private GameObject y2Minus;

    private InputFFValues _inputFFValues;
    [SerializeField] private List<GameObject> resetObjects;
    [SerializeField] private List<bool> resetBools;
    [SerializeField] private List<TextMeshProUGUI> resetTexts;
    [SerializeField] private List<string> texts;
 
    private void Start()
    {
        _inputFFValues = GetComponent<InputFFValues>();

        matrixFreeValues.SetActive(false);
        matrixRadianValues.SetActive(true);

        addition.SetActive(true);
        vectorObject.SetActive(false);
        matrix.SetActive(false);
        scalarObject.SetActive(false);

        x1Minus.SetActive(false);
        _inputFFValues.x1Value = true;
        x2Minus.SetActive(false);
        _inputFFValues.x2Value = true;
        y1Minus.SetActive(false);
        _inputFFValues.y1Value = true;
        y2Minus.SetActive(false);
        _inputFFValues.y2Value = true;

        dropdown = dropdownMenu.GetComponent<TMPro.TMP_Dropdown>();
        if (dropdown == null) Debug.Log(gameObject + "can't find dropdown");

        resetBools = new List<bool>();
        texts = new List<string>();

        for (int i = 0; i < resetObjects.Count; i++)
        {
            resetBools.Add(resetObjects[i].activeInHierarchy);
        }
        for (int i = 0; i < resetTexts.Count; i++)
        {
            texts.Add(resetTexts[i].text);
        }
    }

    // called from menu
    public void DropDownMenu()
    {
        if (dropdown.value == 0) // Addition
        {
            addition.SetActive(true);
            vectorObject.SetActive(false);
            matrix.SetActive(false);
            scalarObject.SetActive(false);

            _inputFFValues.SetCalcType(CalculationType.Addition);
        }
        else if (dropdown.value == 1) // matrixmultiplication
        {
            matrix.SetActive(true);
            vectorObject.SetActive(true);
            scalarObject.SetActive(false);
            addition.SetActive(false);

            if (matrixFreeValues.activeInHierarchy)
                _inputFFValues.SetCalcType(CalculationType.MatrixMultiplicationF);
            else
                _inputFFValues.SetCalcType(CalculationType.MatrixMultiplicationR);
        }
        else if (dropdown.value == 2) // scalar multiplication
        {
            scalarObject.SetActive(true);
            vectorObject.SetActive(true);
            matrix.SetActive(false);
            addition.SetActive(false);

            _inputFFValues.SetCalcType(CalculationType.ScalarMultiplication);
        }
        else
        {
            Debug.Log("error in dropdown menu");
        }

        _inputFFValues.ResetResult();
    }

    // called from button
    public void AdditionButton()
    {
        if (additionButtonText.text == "+")
        {
            additionButtonText.text = "-";

            _inputFFValues.additionValue = false;
        }
        else
        {
            additionButtonText.text = "+";

            _inputFFValues.additionValue = true;
        }
    }

    public void MatrixInputFieldChange()
    {
        if (matrixFreeValues.activeInHierarchy)
        {
            matrixFreeValues.SetActive(false);
            matrixRadianValues.SetActive(true);

            _inputFFValues.SetCalcType(CalculationType.MatrixMultiplicationR);
        }
        else
        {
            matrixFreeValues.SetActive(true);
            matrixRadianValues.SetActive(false);

            _inputFFValues.SetCalcType(CalculationType.MatrixMultiplicationF);
        }
    }

    public void MatrixX1()
    {
        if (x1Minus.activeInHierarchy)
        {
            x1Minus.SetActive(false);
            _inputFFValues.x1Value = true;
        }
        else
        {
            x1Minus.SetActive(true);
            _inputFFValues.x1Value = false;
        }
    }

    public void MatrixX2()
    {
        if (x2Minus.activeInHierarchy)
        {
            x2Minus.SetActive(false);
            _inputFFValues.x2Value = true;
        }
        else
        {
            x2Minus.SetActive(true);
            _inputFFValues.x2Value = false;
        }
    }

    public void MatrixY1()
    {
        if (y1Minus.activeInHierarchy)
        {
            y1Minus.SetActive(false);
            _inputFFValues.y1Value = true;
        }
        else
        {
            y1Minus.SetActive(true);
            _inputFFValues.y1Value = false;
        }
    }

    public void MatrixY2()
    {
        if (y2Minus.activeInHierarchy)
        {
            y2Minus.SetActive(false);
            _inputFFValues.y2Value = true;
        }
        else
        {
            y2Minus.SetActive(true);
            _inputFFValues.y2Value = false;
        }
    }

    public void ResetAllValues()
    {
        for (int i = 0; i < resetObjects.Count; i++)
        {
            resetObjects[i].SetActive(resetBools[i]);
        }
        for (int i = 0; i < resetTexts.Count; i++)
        {
            resetTexts[i].text = texts[i];
        }
    }
}
