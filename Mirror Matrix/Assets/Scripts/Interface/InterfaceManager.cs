using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
using TMPro;
//using System.Globalization;

/// <summary>
/// central hub for managing all inputs and calculating the next rounds
/// collects the correct values via the respective input scripts
/// then sends the values to maths script to get correct results
/// and tells the spaceship script what the result is
/// </summary>

public class InterfaceManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scalesText;
    [SerializeField] private TextMeshProUGUI turnCounterText;
    [SerializeField] private GameObject turnCounterObject;
    [SerializeField] private int switchToFreeMode = 1;

    // Scripts
    [Space]
    private InputFFValues _inputFF;
    private InputGameValues _inputG;
    [SerializeField] private SpaceshipBehaviour _spaceshipBehaviour;
    private InputGameButtons _buttons;

    private float[] startV;
    private float[] addV;
    private float[] matrix;
    private float[] resultV;
    private float scalar;

    private float[] spaceshipTop;
    private float[] spaceshipTopResult;

    private float[] spaceshipRight;
    private float[] spaceshipRightResult;

    public bool freeFlowMode;

    private bool additionValue;
    private bool calculationSuccessful;
    private CalculationType calcType;
    
    void Start()
    {
        if (_spaceshipBehaviour == null)
        {
            _spaceshipBehaviour = FindObjectOfType<SpaceshipBehaviour>();
        }

        //_turnManager = GetComponent<TurnManager>();
        if (freeFlowMode)
        {
            _inputFF = GetComponent<InputFFValues>();
            _inputFF.SetSpaceshipScript(_spaceshipBehaviour);

            turnCounterObject.SetActive(false);
        }
        else
        {
            _inputG = GetComponent<InputGameValues>();
            _inputG.SetSpaceshipScript(_spaceshipBehaviour);
            _buttons = GetComponent<InputGameButtons>();
            //update turncounter?
        }

        //scalesText.text = "yes";
        _spaceshipBehaviour.SetScaling(false);

        /* -- turn back on once game mode possible
        if (freeFlowMode)
        {
            turnCounterObject.SetActive(false);
            additionValue = true;
        }
        else
        {
            turnCounterObject.SetActive(true);
        }*/

        EventManager.Instance.AddEventListener("TURN", NextTurn);
    }

    private void OnDestroy()
    {
        EventManager.Instance.RemoveEventListener("TURN", NextTurn);
    }

    private void NextTurn(string eventName, object param)
    {
        if (eventName == "NextTurn")
        {
            if (!freeFlowMode)
                UpdateTurnCounterDisplay((int)param);
            CollectValues();
            Calculate();
            Move();
            if ((int)param == switchToFreeMode) _buttons.SetUpFreeMode();
        }
    }

    public void UpdateTurnCounterDisplay(int value)
    {
        if (!turnCounterObject.activeInHierarchy)
        {
            turnCounterObject.SetActive(true);
        }
        turnCounterText.text = value.ToString();
    }

    public void CollectValues()
    {
        if (freeFlowMode)
        {
            // get start vector
            startV = _inputFF.GetStartVector();

            // move spaceship to start vector + get correct spaceship top + right position
            _spaceshipBehaviour.MoveSpaceship(startV);
            spaceshipTop = _spaceshipBehaviour.ShipTopCoordinates();
            spaceshipRight = _spaceshipBehaviour.ShipRightCoordinates();

            // get type of calculation and get corresponding values
            calcType = _inputFF.GetCalculationType();

            if (calcType == CalculationType.Addition)
            {
                additionValue = _inputFF.AdditionValue();
                addV = _inputFF.GetAddVector();
            }
            else if (calcType == CalculationType.MatrixMultiplicationF)
                matrix = _inputFF.GetMatrixValuesF();
            else if (calcType == CalculationType.MatrixMultiplicationR)
                matrix = _inputFF.GetMatrixValuesA();
            else if (calcType == CalculationType.ScalarMultiplication)
                scalar = _inputFF.GetScalarMultiplier();
        }
        else
        {
            // get current spaceship position (startV) and spaceship top + right position
            startV = _spaceshipBehaviour.SpaceshipCoordinates();
            spaceshipTop = _spaceshipBehaviour.ShipTopCoordinates();
            spaceshipRight = _spaceshipBehaviour.ShipRightCoordinates();

            // get type of calculation Type + corresponding values
            calcType = _inputG.GetCalculationType();
            
            if (calcType == CalculationType.MatrixMultiplicationR)
                matrix = _inputG.GetMatrixValuesR();
            else if (calcType == CalculationType.Addition)
            {
                additionValue = _inputG.AdditionValue();
                addV = _inputG.GetAddVector();
                _buttons.DashOver();
            }
            else if (calcType == CalculationType.MatrixMultiplicationF)
                matrix = _inputG.GetMatrixValuesF();
        }
    }

    public void Calculate()
    {
        calculationSuccessful = false;

        /*
        Debug.Log($"spaceship pos = {startV[0]}, {startV[1]}");
        Debug.Log($"spaceship top pos = {spaceshipTop[0]}, {spaceshipTop[1]}");
        Debug.Log($"spaceship right pos = {spaceshipRight[0]}, {spaceshipRight[1]}");
        */

        if (calcType == CalculationType.Addition)
        {
            resultV = GameManagement._maths.Addition(startV, addV, additionValue);
            spaceshipTopResult = GameManagement._maths.Addition(spaceshipTop, addV, additionValue);
            spaceshipRightResult = GameManagement._maths.Addition(spaceshipRight, addV, additionValue);
            calculationSuccessful = true;
        }
        else if (calcType == CalculationType.MatrixMultiplicationF || calcType == CalculationType.MatrixMultiplicationR)
        {
            resultV = GameManagement._maths.Multiplication(startV, matrix);
            spaceshipTopResult = GameManagement._maths.Multiplication(spaceshipTop, matrix);
            spaceshipRightResult = GameManagement._maths.Multiplication(spaceshipRight, matrix);
            calculationSuccessful = true;
        }
        else if (calcType == CalculationType.ScalarMultiplication)
        {
            resultV = GameManagement._maths.ScalarMultiplication(startV, scalar);
            spaceshipTopResult = GameManagement._maths.ScalarMultiplication(spaceshipTop, scalar);
            spaceshipRightResult = GameManagement._maths.ScalarMultiplication(spaceshipRight, scalar);
            calculationSuccessful = true;
        }
        else
        {
            Debug.Log("error calculation");
            calculationSuccessful = false;
        }
    }

    public void Move()
    {
        // move spaceship
        if (calculationSuccessful)
        {
            if (freeFlowMode)
            {
                _inputFF.WriteResultVector(resultV);
                _spaceshipBehaviour.UpdateSpaceshipFF(startV, resultV, spaceshipTopResult, spaceshipRightResult);
            }
            else
            {
                _inputG.WriteNewSpaceshipPos(resultV[0], resultV[1]);
                _spaceshipBehaviour.UpdateSpaceshipG(resultV, spaceshipTopResult, spaceshipRightResult);
                _inputG.ClearMatrix();
            }
        }
        else
        {
            Debug.LogWarning("something went wrong with the calculation");
        }
    }

    public bool GameIsReady()
    {
        if (freeFlowMode) return true;
        else return _inputG.IsGameReady();
    }

    public void MenuButton()
    {
        GameManagement.LoadStartMenu();
    }

    public void ScalesButton()
    {
        if (scalesText.text == "yes")
        {
            scalesText.text = "no";
            _spaceshipBehaviour.SetScaling(false);
        }
        else
        {
            scalesText.text = "yes";
            _spaceshipBehaviour.SetScaling(true);
        }
    }
}
