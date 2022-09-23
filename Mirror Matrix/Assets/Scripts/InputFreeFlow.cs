using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputFreeFlow : MonoBehaviour
{
    // GameObjects that contain all input elements in free flow mode
    [SerializeField] private GameObject addition; 
    [SerializeField] private GameObject multiplication;
    [SerializeField] private GameObject scalarObject;
    [SerializeField] private GameObject vectorObject;

    [Space]
    [SerializeField] private GameObject dropdownMenu;
    private TMPro.TMP_Dropdown dropdown;
    [SerializeField] private TextMeshProUGUI buttonText;

    private Maths _maths;
    private DisplayResults _display;
    private float[] startV;
    private float[] addV;
    private float[] matrix;
    private float[] resultV;
    private float scalar;

    private bool additionValue;

    [SerializeField]
    private float[] spaceshipTop;
    [SerializeField]
    private float[] oldTop;
    [SerializeField]
    private float[] spaceshipTopResult;

    [Space]
    [Header("Starting Vector")]
    [SerializeField] private TMPro.TMP_InputField vectorx;
    [SerializeField] private TMPro.TMP_InputField vectory;

    [Header("Addition:")]
    [SerializeField] private TMPro.TMP_InputField vectorAddx;
    [SerializeField] private TMPro.TMP_InputField vectorAddy;
    [SerializeField] private TMPro.TMP_InputField startVx;
    [SerializeField] private TMPro.TMP_InputField startVy;
    [Header("Multiplication:")]
    [SerializeField] private TMPro.TMP_InputField matrixX1;
    [SerializeField] private TMPro.TMP_InputField matrixX2;
    [SerializeField] private TMPro.TMP_InputField matrixY1;
    [SerializeField] private TMPro.TMP_InputField matrixY2;
    [Header("Scalar:")]
    [SerializeField] private TMPro.TMP_InputField scalarInput;
    [Space]
    [Header("Result:")]
    [SerializeField] private TextMeshProUGUI resultX;
    [SerializeField] private TextMeshProUGUI resultY;
    
    void Start()
    {
        addition.SetActive(true);
        vectorObject.SetActive(false);
        multiplication.SetActive(false);
        scalarObject.SetActive(false);

        resultX.text = "";
        resultY.text = "";
        additionValue = true;

        _maths = FindObjectOfType<Maths>();
        if (_maths == null)
        {
            Debug.LogWarning(gameObject + " can't find maths script");
        }
        _display = FindObjectOfType<DisplayResults>();
        if (_display == null)
        {
            Debug.LogWarning(gameObject + " can't find display script");
        }
        oldTop = _display.ShipTopCoordinates();

        dropdown = dropdownMenu.GetComponent<TMPro.TMP_Dropdown>();
        if (dropdown == null)
        {
            Debug.Log(gameObject + "can't find dropdown");
        }
    }

    public void DropDownMenu()
    {
        if (dropdown.value == 0)
        {
            addition.SetActive(true);
            vectorObject.SetActive(false);
            multiplication.SetActive(false);
            scalarObject.SetActive(false);
        }
        else if (dropdown.value == 1)
        {
            multiplication.SetActive(true);
            vectorObject.SetActive(true);
            scalarObject.SetActive(false);
            addition.SetActive(false);
        }
        else if (dropdown.value == 2)
        {
            scalarObject.SetActive(true);
            vectorObject.SetActive(true);
            multiplication.SetActive(false);
            addition.SetActive(false);
        }
        else
        {
            Debug.Log("error in dropdown menu");
        }
    }

    public void GetStartVector()
    {

    }

    public void GetAddVector()
    {

    }

    public void GetMatrixValues()
    {

    }
}
