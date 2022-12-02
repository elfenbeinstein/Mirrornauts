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

    private int additionAmount; // move to stats at some point
    private int shieldAmount;

    private bool freeMode;

    private void Start()
    {
        _inputGameValues = GetComponent<InputGameValues>();
        x1Minus.SetActive(false);
        x2Minus.SetActive(false);
        y1Minus.SetActive(false);
        y2Minus.SetActive(false);
        freeMode = false;

        multiplicationRad.SetActive(true);
        multiplicationFree.SetActive(false);
        addition.SetActive(false);

        additionAmount = 0;
    }

    private void Update()
    {
        // elfenbeinstein: DELETE for final build
        if (Input.GetKeyDown(KeyCode.A)) additionAmount += 1;
        if (Input.GetKeyDown(KeyCode.S)) shieldAmount *= 1;
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
            addition.SetActive(false);
            additionAmount += 1;
            if (!freeMode) multiplicationRad.SetActive(true);
            else multiplicationFree.SetActive(true);
        }
        else
        {
            if (additionAmount >= 1)
            {
                addition.SetActive(true);
                additionAmount -= 1;
                multiplicationRad.SetActive(false);
                multiplicationFree.SetActive(false);
            }
            else
            {
                Debug.Log("can't dash, no powerups");
                // elfenbeinstein MISSING player feedback for missing powerup
            }

        }
    }

    public void Shield()
    {
        
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
}
