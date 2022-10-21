using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputFFButtons : MonoBehaviour
{
    [Header("Matrix Multiplication:")]
    [SerializeField] private GameObject matrixFreeValues;
    [SerializeField] private GameObject matrixAngleValues;
    [SerializeField] private GameObject x1Minus;
    [SerializeField] private GameObject x2Minus;
    [SerializeField] private GameObject y1Minus;
    [SerializeField] private GameObject y2Minus;

    private void Start()
    {
        matrixFreeValues.SetActive(false);
        matrixAngleValues.SetActive(true);
    }

    public void MatrixInputFieldChange()
    {
        if (matrixFreeValues.activeInHierarchy)
        {
            matrixFreeValues.SetActive(false);
            matrixAngleValues.SetActive(true);
        }
        else
        {
            matrixFreeValues.SetActive(true);
            matrixAngleValues.SetActive(false);
        }
    }

    public void MatrixX1()
    {
        if (x1Minus.activeInHierarchy)
            x1Minus.SetActive(false);
        else
            x1Minus.SetActive(true);

        // hand value over
    }

    public void MatrixX2()
    {
        if (x2Minus.activeInHierarchy)
            x2Minus.SetActive(false);
        else
            x2Minus.SetActive(true);

        // hand value over
    }

    public void MatrixY1()
    {
        if (y1Minus.activeInHierarchy)
            y1Minus.SetActive(false);
        else
            y1Minus.SetActive(true);

        // hand value over
    }

    public void MatrixY2()
    {
        if (y2Minus.activeInHierarchy)
            y2Minus.SetActive(false);
        else
            y2Minus.SetActive(true);

        // hand value over
    }
}
