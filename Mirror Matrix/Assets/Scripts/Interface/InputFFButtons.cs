using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputFFButtons : MonoBehaviour
{
    [Header("Input Elements - General:")]
    [SerializeField] private GameObject addition;
    [SerializeField] private GameObject matrix;
    [SerializeField] private GameObject scalarObject;
    [SerializeField] private GameObject vectorObject;
    [Space]
    [SerializeField] private GameObject dropdownMenu;
    private TMPro.TMP_Dropdown dropdown;
    [SerializeField] private TextMeshProUGUI additionButtonText;
    [Space]
    [Header("Matrix Multiplication:")]
    [SerializeField] private GameObject matrixFreeValues;
    [SerializeField] private GameObject matrixAngleValues;
    [SerializeField] private GameObject x1Minus;
    [SerializeField] private GameObject x2Minus;
    [SerializeField] private GameObject y1Minus;
    [SerializeField] private GameObject y2Minus;

    private InputFFValues _inputFFValues;

    private void Start()
    {
        _inputFFValues = GetComponent<InputFFValues>();

        matrixFreeValues.SetActive(false);
        matrixAngleValues.SetActive(true);

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
                _inputFFValues.SetCalcType(CalculationType.MatrixMultiplicationA);
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
            matrixAngleValues.SetActive(true);

            _inputFFValues.SetCalcType(CalculationType.MatrixMultiplicationA);
        }
        else
        {
            matrixFreeValues.SetActive(true);
            matrixAngleValues.SetActive(false);

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
}
