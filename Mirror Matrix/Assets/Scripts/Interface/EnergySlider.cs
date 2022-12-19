using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergySlider : MonoBehaviour
{
    private Slider _slider;
    [SerializeField] private TMPro.TextMeshProUGUI text;
    [SerializeField] private Stats _stats;

    [SerializeField] private TMPro.TextMeshProUGUI energyCost;
    string origText;

    void Start()
    {
        _slider = GetComponent<Slider>();

        EventManager.Instance.AddEventListener("ENERGY", EnergyListener);

        GameManagement._playerStats.energy = _stats.maxEnergy;
        _slider.maxValue = _stats.maxEnergy;
        _slider.value = _stats.maxEnergy;
        text.text = _stats.maxEnergy + "/" + _stats.maxEnergy.ToString();

        origText = energyCost.text;
    }

    private void OnDestroy()
    {
        EventManager.Instance.RemoveEventListener("ENERGY", EnergyListener);
    }

    void EnergyListener(string eventName, object param)
    {
        if (eventName == "AddEnergy")
        {
            GameManagement._playerStats.energy += (int)param;
            if (GameManagement._playerStats.energy > _stats.maxEnergy) GameManagement._playerStats.energy = _stats.maxEnergy;
            UpdateSlider();
        }
        else if (eventName == "RemoveEnergy")
        {
            GameManagement._playerStats.energy -= (int)param;
            UpdateSlider();
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

    private void UpdateSlider()
    {
        // update slider value
        _slider.value = GameManagement._playerStats.energy;
        // update description text
        text.text = GameManagement._playerStats.energy.ToString() + "/" + _stats.maxEnergy.ToString();
    }

    private void UpdateCost()
    {

    }

    private void RemoveCost()
    {
        energyCost.text = "";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            GameManagement._playerStats.energy++;
            if (GameManagement._playerStats.energy > _stats.maxEnergy) GameManagement._playerStats.energy = _stats.maxEnergy;
            UpdateSlider();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            GameManagement._playerStats.energy--;
            if (GameManagement._playerStats.energy < 0) GameManagement._playerStats.energy = 0;
            UpdateSlider();
        }
    }
}
