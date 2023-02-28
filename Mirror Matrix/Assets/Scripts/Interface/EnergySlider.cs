using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Adds and Removes Energy
/// Displays Current Amount on slider
/// plays remove & add "animations"
/// </summary>

public class EnergySlider : MonoBehaviour
{
    private Slider _slider;
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private TMPro.TextMeshProUGUI text;
    [SerializeField] private TMPro.TextMeshProUGUI energyCost;
    string origText;

    [SerializeField] private float slideDuration = 1.5f;
    [SerializeField] private Animator anim;

    bool sliding;
    int amount;
    float time = 0;

    void Start()
    {
        _slider = GetComponent<Slider>();

        EventManager.Instance.AddEventListener("ENERGY", EnergyListener);

        _playerStats.energy = _playerStats.maxEnergy;
        _slider.maxValue = _playerStats.maxEnergy;
        _slider.value = _playerStats.maxEnergy;
        text.text = _playerStats.maxEnergy + "/" + _playerStats.maxEnergy.ToString();

        origText = energyCost.text;
        energyCost.text = "";
        sliding = false;
    }

    private void OnDestroy()
    {
        EventManager.Instance.RemoveEventListener("ENERGY", EnergyListener);
    }

    void EnergyListener(string eventName, object param)
    {
        if (eventName == "AddEnergy")
        {
            // values for Sliding:
            amount = (int)param;
            if (_playerStats.energy + amount > _playerStats.maxEnergy) amount = _playerStats.maxEnergy - _playerStats.energy;
            sliding = true;
            // set animation to true
            anim.SetBool("Move", true);
            time = 0;

            _playerStats.energy += (int)param;
            if (_playerStats.energy > _playerStats.maxEnergy) _playerStats.energy = _playerStats.maxEnergy;
        }
        else if (eventName == "RemoveEnergy")
        {
            amount = (int)param * -1;
            sliding = true;
            // set animation to true
            anim.SetBool("Move", true);
            time = 0;

            _playerStats.energy -= (int)param;
            EventManager.Instance.EventGo("DATA", "Energy", param);
        }
        else if (eventName == "EnergyCost")
        {
            UpdateCost();
        }
        else if (eventName == "RemoveCost")
        {
            RemoveCost();
        }
    }

    private void UpdateSliderDirectly()
    {
        // update slider value
        _slider.value = _playerStats.energy;
        // update description text
        text.text = _playerStats.energy.ToString() + "/" + _playerStats.maxEnergy.ToString();
    }

    private void UpdateCost()
    {
        energyCost.text = origText + _playerStats.energyNeeded.ToString();
    }

    private void RemoveCost()
    {
        energyCost.text = "";
    }

    private bool ReachedTarget()
    {
        bool result = false;
        if (_slider.value == _playerStats.energy) result = true;
        else if (_slider.value < _playerStats.energy && amount < 0) result = true;
        else if (_slider.value > _playerStats.energy && amount > 0) result = true;

        return result;
    }

    private void Update()
    {
        if (sliding)
        {
            time += Time.deltaTime;
            if (amount != 0)
            {
                if (time >= slideDuration / Mathf.Abs(amount))
                {
                    if (amount > 0) _slider.value++;
                    else _slider.value--;
                    text.text = _slider.value.ToString() + "/" + _playerStats.maxEnergy.ToString();
                    time = 0;

                    // check if reached the end:
                    if (ReachedTarget())
                    {
                        _slider.value = _playerStats.energy;
                        text.text = _slider.value.ToString() + "/" + _playerStats.maxEnergy.ToString();
                        sliding = false;
                        anim.SetBool("Move", false);
                    }
                }
            }
            else
            {
                _slider.value = _playerStats.energy;
                text.text = _slider.value.ToString() + "/" + _playerStats.maxEnergy.ToString();
                sliding = false;
                anim.SetBool("Move", false);
            }
        }

        /*
# if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _playerStats.energy++;
            if (_playerStats.energy > _playerStats.maxEnergy) _playerStats.energy = _playerStats.maxEnergy;
            UpdateSliderDirectly();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _playerStats.energy--;
            if (_playerStats.energy < 0) _playerStats.energy = 0;
            UpdateSliderDirectly();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            sliding = true;
            amount = Random.Range(-25, 26);
            if (amount + _playerStats.energy > _playerStats.maxEnergy) amount = _playerStats.maxEnergy - _playerStats.energy;
            time = 0;
            _playerStats.energy += amount;
            anim.SetBool("Move", true);
        }
#endif
        */
    }
}
