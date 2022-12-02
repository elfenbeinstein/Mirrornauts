using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputGameButtons : MonoBehaviour
{
    private InputGameValues _inputGameValues;

    [Header("Calculation Modes:")]
    [SerializeField] private GameObject multiplicationRad;
    [SerializeField] private GameObject multiplicationFree;
    [SerializeField] private GameObject addition;

    [Header("Sin/Cos Multiplication +-:")]
    [SerializeField] private GameObject x1Minus; // to change the pos/neg value of the field
    [SerializeField] private GameObject x2Minus;
    [SerializeField] private GameObject y1Minus;
    [SerializeField] private GameObject y2Minus;

    [Header("Free Multiplication +-:")]
    [SerializeField] private GameObject x1FMinus;
    [SerializeField] private GameObject x2FMinus;
    [SerializeField] private GameObject y1FMinus;
    [SerializeField] private GameObject y2FMinus;

    [Header("Addition")]
    [SerializeField] private TMPro.TextMeshProUGUI additionButtonText;
    [SerializeField] private GameObject addXMinus;
    [SerializeField] private GameObject addYMinus;

    private bool freeMode;

    private void Start()
    {
        _inputGameValues = GetComponent<InputGameValues>();
        

        multiplicationRad.SetActive(true);
        x1Minus.SetActive(false);
        x2Minus.SetActive(false);
        y1Minus.SetActive(false);
        y2Minus.SetActive(false);

        /*
        multiplicationFree.SetActive(true);
        x1FMinus.SetActive(false);
        x2FMinus.SetActive(false);
        y1FMinus.SetActive(false);
        y2FMinus.SetActive(false);
        */
        multiplicationFree.SetActive(false);

        addition.SetActive(true);
        addXMinus.SetActive(false);
        addYMinus.SetActive(false);
        addition.SetActive(false);
        additionButtonText.text = "+";
        _inputGameValues.addValue = true;

        _inputGameValues.calcType = CalculationType.MatrixMultiplicationR;
        freeMode = false;
    }

    private void Update()
    {
        // elfenbeinstein: DELETE for final build
        if (Input.GetKeyDown(KeyCode.A)) GameManagement.dashAmount += 1;
        if (Input.GetKeyDown(KeyCode.S)) GameManagement.shieldAmount *= 1;
    }

    public void ClearAllFields()
    {
        _inputGameValues.ClearMatrix();
    }

    public void MenuButton()
    {
        GameManagement.LoadStartMenu();
    }

    public void HelpButton()
    {
        // elfenbeinstein MISSING Open Help

        // for now: it's a reset button:
    }

    public void Dash()
    {
        if(addition.activeInHierarchy)
        {
            GameManagement.dashAmount += 1;
            DashOver();
        }
        else
        {
            if (GameManagement.dashAmount >= 1)
            {
                addition.SetActive(true);
                GameManagement.dashAmount -= 1;
                multiplicationRad.SetActive(false);
                multiplicationFree.SetActive(false);
                _inputGameValues.calcType = CalculationType.Addition;
            }
            else
            {
                Debug.Log("can't dash, no powerups");
                // elfenbeinstein MISSING player feedback for missing powerup
            }

        }
    }

    public void DashOver()
    {
        addition.SetActive(false);
        if (!freeMode)
        {
            multiplicationRad.SetActive(true);
            _inputGameValues.calcType = CalculationType.MatrixMultiplicationR;
        }
        else
        {
            multiplicationFree.SetActive(true);
            _inputGameValues.calcType = CalculationType.MatrixMultiplicationF;
        }
    }

    public void Shield()
    {
        if (GameManagement.shieldActive)
        {
            ShieldOver();
        }
    }

    public void ShieldOver()
    {
        GameManagement.shieldActive = false;
    }

    public void SetUpFreeMode()
    {
        addition.SetActive(false);
        multiplicationFree.SetActive(true);
        multiplicationRad.SetActive(false);

        x1FMinus.SetActive(false);
        x2FMinus.SetActive(false);
        y1FMinus.SetActive(false);
        y2FMinus.SetActive(false);

        freeMode = true;
        _inputGameValues.calcType = CalculationType.MatrixMultiplicationF;
    }

    public void MatrixX1()
    {
        if (x1Minus.activeInHierarchy)
        {
            x1Minus.SetActive(false);
            _inputGameValues.x1Value = true;
        }
        else
        {
            x1Minus.SetActive(true);
            _inputGameValues.x1Value = false;
        }
    }

    public void MatrixX2()
    {
        if (x2Minus.activeInHierarchy)
        {
            x2Minus.SetActive(false);
            _inputGameValues.x2Value = true;
        }
        else
        {
            x2Minus.SetActive(true);
            _inputGameValues.x2Value = false;
        }
    }

    public void MatrixY1()
    {
        if (y1Minus.activeInHierarchy)
        {
            y1Minus.SetActive(false);
            _inputGameValues.y1Value = true;
        }
        else
        {
            y1Minus.SetActive(true);
            _inputGameValues.y1Value = false;
        }
    }

    public void MatrixY2()
    {
        if (y2Minus.activeInHierarchy)
        {
            y2Minus.SetActive(false);
            _inputGameValues.y2Value = true;
        }
        else
        {
            y2Minus.SetActive(true);
            _inputGameValues.y2Value = false;
        }
    }

    public void AddX()
    {
        if (addXMinus.activeInHierarchy)
        {
            addXMinus.SetActive(false);
            _inputGameValues.addXValue = true;
        }
        else
        {
            addXMinus.SetActive(true);
            _inputGameValues.addXValue = false;
        }
    }

    public void AddY()
    {
        if (addYMinus.activeInHierarchy)
        {
            addYMinus.SetActive(false);
            _inputGameValues.addYValue = true;
        }
        else
        {
            addYMinus.SetActive(true);
            _inputGameValues.addYValue = false;
        }
    }

    public void AddButton()
    {
        if (additionButtonText.text == "+")
        {
            additionButtonText.text = "-";

            _inputGameValues.addValue = false;
        }
        else
        {
            additionButtonText.text = "+";

            _inputGameValues.addValue = true;
        }
    }
}
