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
    private float[] startV;
    private float[] addV;
    private float[] matrix;

    [Space]
    [Header("Addition:")]
    [SerializeField] private TextMeshProUGUI vectorAStartx;
    [SerializeField] private TextMeshProUGUI vectorAStarty;
    [SerializeField] private TextMeshProUGUI vectorAddx;
    [SerializeField] private TextMeshProUGUI vectorAddy;
    [Header("Multiplication:")]
    [SerializeField] private TextMeshProUGUI vectorMx;
    [SerializeField] private TextMeshProUGUI vectorMy;
    [SerializeField] private TextMeshProUGUI matrixX1;
    [SerializeField] private TextMeshProUGUI matrixX2;
    [SerializeField] private TextMeshProUGUI matrixY1;
    [SerializeField] private TextMeshProUGUI matrixY2;

    void Start()
    {
        addition.SetActive(true);
        multiplication.SetActive(false);

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

            // update calculation
        }
        else
        {
            buttonText.text = "+";

            // update calculation
        }
    }

    public void Calculate()
    {
        Debug.Log("start calculation");

        if (dropdown.value == 0) // Addition
        {
            // get values

            // startV --> vectorAStartx und y
            // addv --> vectorAddx
            /*
            string testx = vectorAStartx.text;
            float testingx = float.Parse(testx, CultureInfo.InvariantCulture.NumberFormat);
            Debug.Log("test x = " + testingx);*/

            /*
            if (buttonText.text == "+")
            {
                _maths.Addition(startV, addV, true);
            }
            else
            {
                _maths.Addition(startV, addV, false);
            } */
        }
        else if (dropdown.value == 1) // multiplication
        {
            // get values

        }
        else
        {
            Debug.Log("error calculation");
        }
    }
}
