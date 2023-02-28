using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// All Button Behaviour During Game Mode
/// --> Dash, +- Buttons, Menu, Help, Clear All etc
/// </summary>

public class InputGameButtons : MonoBehaviour
{
    private InputGameValues _inputGameValues;
    private PlayerStats _playerStats;

    [Header("Calculation Modes:")]
    [SerializeField] private GameObject multiplicationRad;
    [SerializeField] private GameObject multiplicationFree;
    [SerializeField] private GameObject addition;

    [Header("Sin/Cos Multiplication +-:")]
    [SerializeField] private GameObject x1Minus; // to change the pos/neg value of the field
    [SerializeField] private GameObject x2Minus;
    [SerializeField] private GameObject y1Minus;
    [SerializeField] private GameObject y2Minus;

    [Header("Addition")]
    [SerializeField] private TMPro.TextMeshProUGUI additionButtonText;

    [Header("Menu and UI")]
    [SerializeField] private GameObject helpScreen;
    [SerializeField] private GameObject menuScreen;
    [SerializeField] private Button dashButton;
    [Tooltip("all objects that should only be active when cooldown is happening put as children of the slider obj")]
    [SerializeField] private Slider dashCooldown;
    [SerializeField] private Animator anim;

    private bool freeMode;

    private void Start()
    {
        _inputGameValues = GetComponent<InputGameValues>();
        _playerStats = GetComponent<InterfaceManager>()._playerStats;

        multiplicationRad.SetActive(true);
        x1Minus.SetActive(false);
        x2Minus.SetActive(false);
        y1Minus.SetActive(false);
        y2Minus.SetActive(false);

        multiplicationFree.SetActive(false);
        addition.SetActive(false);

        dashCooldown.gameObject.SetActive(false);

        _inputGameValues.calcType = CalculationType.MatrixMultiplicationR;
        freeMode = false;

        EventManager.Instance.AddEventListener("DASH", DashListener);
    }

    private void Update()
    {
        /*
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.A))
        {
            ActivateDash(); 
            _playerStats.dashAmount = 1;
        }

        if (Input.GetKeyDown(KeyCode.S)) EventManager.Instance.EventGo("TURN", "Shield");
#endif
        */
    }

    private void OnDestroy()
    {
        EventManager.Instance.RemoveEventListener("DASH", DashListener);
    }

    void DashListener(string eventName, object param)
    {
        if (eventName == "Countdown")
            DeactivateDash();
        if (eventName == "DashActive")
            ActivateDash();
    }

    public void ClearAllFields()
    {
        _inputGameValues.ClearMatrix();
    }

    public void MenuButton()
    {
        if (menuScreen.activeInHierarchy) menuScreen.SetActive(false);
        else menuScreen.SetActive(true);
    }

    public void HelpButton()
    {
        if (helpScreen.activeInHierarchy) helpScreen.SetActive(false);
        else helpScreen.SetActive(true);
    }

    public void Dash()
    {
        // if addition is active --> deactivate the addition calc and go back to normal matrix
        if(addition.activeInHierarchy)
        {
            DashOver();
        }
        else
        {
            if (_playerStats.dashAmount >= 1)
            {
                SetUpAddition();
                multiplicationRad.SetActive(false);
                multiplicationFree.SetActive(false);
                _inputGameValues.calcType = CalculationType.Addition;
            }
            else
            {
                anim.SetTrigger("Shake");
                EventManager.Instance.EventGo("AUDIO", "PlayError");
            }
        }
    }

    private void SetUpAddition()
    {
        addition.SetActive(true);

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

    void DeactivateDash()
    {
        // slider aktivieren
        dashCooldown.gameObject.SetActive(true);
        // stelle slider ein auf int
        dashCooldown.value = _playerStats.dashCD;
    }

    void ActivateDash()
    {
        // deactivate den Slider
        dashCooldown.gameObject.SetActive(false);
    }

    public void SetUpFreeMode(float[] values)
    {
        addition.SetActive(false);
        multiplicationFree.SetActive(true);
        multiplicationRad.SetActive(false);

        freeMode = true;
        _inputGameValues.calcType = CalculationType.MatrixMultiplicationF;

        _inputGameValues.WriteNewSpaceshipPos(values[0], values[1]);
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

        if (_inputGameValues.AllValuesSet())
        {
            _inputGameValues.EnergyNeeded();
            EventManager.Instance.EventGo("ENERGY", "EnergyCost");
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

        if (_inputGameValues.AllValuesSet())
        {
            _inputGameValues.EnergyNeeded();
            EventManager.Instance.EventGo("ENERGY", "EnergyCost");
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

        if (_inputGameValues.AllValuesSet())
        {
            _inputGameValues.EnergyNeeded();
            EventManager.Instance.EventGo("ENERGY", "EnergyCost");
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

        if (_inputGameValues.AllValuesSet())
        {
            _inputGameValues.EnergyNeeded();
            EventManager.Instance.EventGo("ENERGY", "EnergyCost");
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

        if (_inputGameValues.AllValuesSet())
        {
            _inputGameValues.EnergyNeeded();
            EventManager.Instance.EventGo("ENERGY", "EnergyCost");
        }
    }
}
