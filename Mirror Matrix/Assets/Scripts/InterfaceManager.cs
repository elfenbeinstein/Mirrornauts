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
    //private float[] matrix;

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

        // ADDITION
        if (dropdown.value == 0)
        {
            // check all boxes are filled or default to zero
            if (vectorAStartx.text == "" || vectorAStarty.text == "" || vectorAddx.text == "" || vectorAddy.text == "")
            {
                Debug.LogWarning("please fill in all boxes, defaulting to zero");
                float x = 0;
                float y = 0;
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
            
            if (buttonText.text == "+")
            {
                _maths.Addition(startV, addV, true);
            }
            else
            {
                _maths.Addition(startV, addV, false);
            } 
        }
        // MULTIPLICATION
        else if (dropdown.value == 1)
        {
            // get values

        }
        else
        {
            Debug.Log("error calculation");
        }
    }
}
