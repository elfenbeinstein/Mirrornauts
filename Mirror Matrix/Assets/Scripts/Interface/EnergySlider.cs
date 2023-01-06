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

    [SerializeField] private 

    void Start()
    {
        _slider = GetComponent<Slider>();

        EventManager.Instance.AddEventListener("ENERGY", EnergyListener);

        GameManagement._playerStats.energy = GameManagement._playerStats.maxEnergy;
        _slider.maxValue = GameManagement._playerStats.maxEnergy;
        _slider.value = GameManagement._playerStats.maxEnergy;
        text.text = GameManagement._playerStats.maxEnergy + "/" + GameManagement._playerStats.maxEnergy.ToString();

        origText = energyCost.text;
        energyCost.text = "";
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
            if (GameManagement._playerStats.energy > GameManagement._playerStats.maxEnergy) GameManagement._playerStats.energy = GameManagement._playerStats.maxEnergy;
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
        text.text = GameManagement._playerStats.energy.ToString() + "/" + GameManagement._playerStats.maxEnergy.ToString();
    }

    private void UpdateCost()
    {
        energyCost.text = origText + GameManagement._playerStats.energyNeeded.ToString();
    }

    private void RemoveCost()
    {
        energyCost.text = "";
    }

    IEnumerator SliderUpdate(int current, int newE)
    {
        yield return new WaitForSeconds(2);
    }

    private void Update()
    {
        // elfenbeinstein: REMOVE FOR BUILD
# if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            GameManagement._playerStats.energy++;
            if (GameManagement._playerStats.energy > GameManagement._playerStats.maxEnergy) GameManagement._playerStats.energy = GameManagement._playerStats.maxEnergy;
            UpdateSlider();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            GameManagement._playerStats.energy--;
            if (GameManagement._playerStats.energy < 0) GameManagement._playerStats.energy = 0;
            UpdateSlider();
        }
#endif
    }
}
