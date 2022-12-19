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
    [SerializeField] private TMPro.TextMeshProUGUI x1FMinus;
    [SerializeField] private TMPro.TextMeshProUGUI x2FMinus;
    [SerializeField] private TMPro.TextMeshProUGUI y1FMinus;
    [SerializeField] private TMPro.TextMeshProUGUI y2FMinus;

    [Header("Addition")]
    [SerializeField] private TMPro.TextMeshProUGUI additionButtonText;
    [SerializeField] private GameObject addXMinus;
    [SerializeField] private GameObject addYMinus;

    [Header("Menu and UI")]
    [SerializeField] private GameObject helpScreen;

    private bool freeMode;

    private void Start()
    {
        _inputGameValues = GetComponent<InputGameValues>();

        multiplicationRad.SetActive(true);
        x1Minus.SetActive(false);
        x2Minus.SetActive(false);
        y1Minus.SetActive(false);
        y2Minus.SetActive(false);

        multiplicationFree.SetActive(false);
        addition.SetActive(false);

        _inputGameValues.calcType = CalculationType.MatrixMultiplicationR;
        freeMode = false;
    }

    private void Update()
    {
        // elfenbeinstein: DELETE for final build
        if (Input.GetKeyDown(KeyCode.A)) ActivateDash();
        if (Input.GetKeyDown(KeyCode.S)) EventManager.Instance.EventGo("TURN", "Shield");
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
        if (helpScreen.activeInHierarchy) helpScreen.SetActive(false);
        else helpScreen.SetActive(true);
    }

    public void Dash()
    {
        if(addition.activeInHierarchy)
        {
            DashOver();
        }
        else
        {
            if (GameManagement._playerStats.dashAmount >= 1)
            {
                SetUpAddition();
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

    private void SetUpAddition()
    {
        addition.SetActive(true);

        addXMinus.SetActive(false);
        _inputGameValues.addXValue = true;

        addYMinus.SetActive(false);
        _inputGameValues.addYValue = true;

        additionButtonText.text = "+";
        _inputGameValues.addValue = true;
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

    void DeactivateDash(int amount)
    {
        // setze den Button auf inaktiv
        // slider aktivieren
        // stelle slider ein auf int
        // wenn int amount == 0 --> activate dash
    }

    void ActivateDash()
    {
        // setze Button auf inaktiv
        // deactivate den Slider
    }

    public void SetUpFreeMode()
    {
        addition.SetActive(false);
        multiplicationFree.SetActive(true);
        multiplicationRad.SetActive(false);

        x1FMinus.text = "+";
        x2FMinus.text = "+";
        y1FMinus.text = "+";
        y2FMinus.text = "+";

        freeMode = true;
        _inputGameValues.calcType = CalculationType.MatrixMultiplicationF;
    }

    public void PlusMinusButton()
    {
        EventManager.Instance.EventGo("BUTTON", "+-Button");
    }

    public void MatrixX1()
    {
        if (multiplicationRad.activeInHierarchy)
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
        else
        {
            if (x1FMinus.text == "-")
            {
                x1FMinus.text = "+";
                _inputGameValues.x1FValue = true;
            }
            else
            {
                x1FMinus.text = "-";
                _inputGameValues.x1FValue = false;
            }
        }
    }

    public void MatrixX2()
    {
        if (multiplicationRad.activeInHierarchy)
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
        else
        {
            if (x2FMinus.text == "-")
            {
                x2FMinus.text = "+";
                _inputGameValues.x2FValue = true;
            }
            else
            {
                x2FMinus.text = "-";
                _inputGameValues.x2FValue = false;
            }
        }
    }

    public void MatrixY1()
    {
        if (multiplicationRad.activeInHierarchy)
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
        else
        {
            if (y1FMinus.text == "-")
            {
                y1FMinus.text = "+";
                _inputGameValues.y1FValue = true;
            }
            else
            {
                y1FMinus.text = "-";
                _inputGameValues.y1FValue = false;
            }
        }
    }

    public void MatrixY2()
    {
        if (multiplicationRad.activeInHierarchy)
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
        else
        {
            if (y2FMinus.text == "-")
            {
                y2FMinus.text = "+";
                _inputGameValues.y2FValue = true;
            }
            else
            {
                y2FMinus.text = "-";
                _inputGameValues.y2FValue = false;
            }
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
