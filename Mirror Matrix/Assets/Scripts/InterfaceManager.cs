using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InterfaceManager : MonoBehaviour
{
    [SerializeField] private GameObject addition;
    [SerializeField] private GameObject multiplication;

    [Space]
    [SerializeField] private GameObject dropdownMenu;
    private TMPro.TMP_Dropdown dropdown;

    [Space]
    [SerializeField] private TextMeshProUGUI buttonText;

    void Start()
    {
        addition.SetActive(true);
        multiplication.SetActive(false);

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
}
