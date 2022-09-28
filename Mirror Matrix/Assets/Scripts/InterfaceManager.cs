using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
using TMPro;
//using System.Globalization;

public class InterfaceManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI turnCounterText;
    [SerializeField] private GameObject turnCounterObject;

    // Scripts
    [Space]
    [SerializeField] private InputFreeFlow _inputFreeFlow;
    [SerializeField] private Stats _stats;
    [SerializeField] private TurnManager _turnManager;
    private Maths _maths;
    private SpaceshipBehaviour _spaceshipBehaviour;

    private float[] startV;
    private float[] addV;
    private float[] matrix;
    private float[] resultV;
    private float scalar;

    private float[] spaceshipTop;
    private float[] spaceshipTopResult;

    public bool freeFlowMode;
    private bool additionValue;
    private bool calculationSuccessful;
    private int calculationType;
    


    void Start()
    {
        _maths = FindObjectOfType<Maths>();
        if (_maths == null)
        {
            Debug.LogWarning(gameObject + " can't find maths script");
        }
        _spaceshipBehaviour = FindObjectOfType<SpaceshipBehaviour>();
        if (_spaceshipBehaviour == null)
        {
            Debug.LogWarning(gameObject + " can't find display script");
        }

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
    }

    public void UpdateTurnCounterDisplay(int value)
    {
        if (!turnCounterObject.activeInHierarchy)
        {
            turnCounterObject.SetActive(true);
        }
        turnCounterText.text = value.ToString();
    }

    /*
    public void SpaceshipCollider(bool value)
    {
        _spaceshipBehaviour._spaceshipCollider.SetActive(value);
    }*/

    public void CollectValues()
    {
        if (freeFlowMode)
        {
            // get start vector
            startV = _inputFreeFlow.GetStartVector();

            // move spaceship to start vector
            _spaceshipBehaviour.MoveSpaceship(startV);

            // get spaceship top from current position
            spaceshipTop = _spaceshipBehaviour.ShipTopCoordinates();
            Debug.Log($"top position before calc is {spaceshipTop[0]}, {spaceshipTop[1]}");

            // get type of calculation and get corresponding values
            calculationType = _inputFreeFlow.GetCalculationType();

            if (calculationType == 0) // Addition
            {
                additionValue = _inputFreeFlow.AdditionValue();
                addV = _inputFreeFlow.GetAddVector();

            }
            else if (calculationType == 1) // Multiplication
            {
                matrix = _inputFreeFlow.GetMatrixValues();
            }
            else if (calculationType == 2) // Scalar Multiplication
            {
                scalar = _inputFreeFlow.GetScalarMultiplier();
            }
        }
        else
        {
            // get current spaceship position (startV) and spaceship top position
            startV = _spaceshipBehaviour.SpaceshipCoordinates();
            spaceshipTop = _spaceshipBehaviour.ShipTopCoordinates();

            // get type of calculation + values (from different input script)
        }
    }

    public void Calculate()
    {
        calculationSuccessful = false;

        if (calculationType == 0) // Addition
        {
            resultV = _maths.Addition(startV, addV, additionValue);
            spaceshipTopResult = _maths.Addition(spaceshipTop, addV, additionValue);
            calculationSuccessful = true;
        }
        else if (calculationType == 1) // Multiplication
        {
            resultV = _maths.Multiplication(startV, matrix);
            spaceshipTopResult = _maths.Multiplication(spaceshipTop, matrix);
            calculationSuccessful = true;
        }
        else if (calculationType == 2) // Scalar Multiplication
        {
            resultV = _maths.ScalarMultiplication(startV, scalar);
            spaceshipTopResult = _maths.ScalarMultiplication(spaceshipTop, scalar);
            calculationSuccessful = true;
        }
        else
        {
            Debug.Log("error calculation dropwdown menu option out of bounds");
            calculationSuccessful = false;
        }
    }

    public void CalculateAndMove()
    {
        Calculate();

        // move spaceship
        if (calculationSuccessful)
        {
            _inputFreeFlow.WriteResultVector(resultV);
            _spaceshipBehaviour.UpdateSpaceshipFF(startV, resultV, spaceshipTopResult);

        }
        else
        {
            Debug.LogWarning("something went wrong with the calculation");
        }

        /* -- update once game mode is possible
        if (freeFlowMode)
        {
            // move spaceship
            if (calculationSuccessful)
            {
                _inputFreeFlow.WriteResultVector(resultV);
                _spaceshipBehaviour.UpdateDisplay(startV, resultV, spaceshipTopResult);

            }
            else
            {
                Debug.LogWarning("something went wrong with the calculation");
            }
        }
        else
        {
            // POSSIBLY ADD DIFFERENT MOVEMENT HERE
            if (calculationSuccessful)
            {
                _spaceshipBehaviour.UpdateDisplay(startV, resultV, spaceshipTopResult);

            }
            else
            {
                Debug.LogWarning("something went wrong with the calculation");
            }
        }
        */
    }
}
