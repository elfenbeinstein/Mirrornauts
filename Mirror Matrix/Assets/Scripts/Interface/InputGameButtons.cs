using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputGameButtons : MonoBehaviour
{
    private InputGameValues _inputGameValues;

    [SerializeField] private GameObject x1Minus; // to change the pos/neg value of the field
    [SerializeField] private GameObject x2Minus;
    [SerializeField] private GameObject y1Minus;
    [SerializeField] private GameObject y2Minus;

    private void Start()
    {
        _inputGameValues = GetComponent<InputGameValues>();
        x1Minus.SetActive(false);
        x2Minus.SetActive(false);
        y1Minus.SetActive(false);
        y2Minus.SetActive(false);
    }

    public void ClearAllFields()
    {
        _inputGameValues.ClearMatrix();
    }

    public void MenuButton()
    {
        // elfenbeinstein MISSING Open Menu
    }

    public void HelpButton()
    {
        // elfenbeinstein MISSING Open Help
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
