using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergySlider : MonoBehaviour
{
    private Slider _slider;
    [SerializeField] private TMPro.TextMeshProUGUI text;
    [SerializeField] private Stats _stats;

    void Start()
    {
        _slider = GetComponent<Slider>();

        EventManager.Instance.AddEventListener("ENERGY", EnergyListener);

        GameManagement.energy = _stats.maxEnergy;
        _slider.maxValue = _stats.maxEnergy;
        _slider.value = _stats.maxEnergy;
        text.text = _stats.maxEnergy + "/" + _stats.maxEnergy.ToString();
    }

    private void OnDestroy()
    {
        EventManager.Instance.RemoveEventListener("ENERGY", EnergyListener);
    }

    void EnergyListener(string eventName, object param)
    {
        if (eventName == "AddEnergy")
        {
            UpdateSlider();
        }
        else if (eventName == "RemoveEnergy")
        {
            UpdateSlider();
        }

    }

    private void UpdateSlider()
    {
        // update slider value
        _slider.value = GameManagement.energy;
        // update description text
        text.text = GameManagement.energy.ToString() + "/" + _stats.maxEnergy.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            GameManagement.energy++;
            if (GameManagement.energy > _stats.maxEnergy) GameManagement.energy = _stats.maxEnergy;
            UpdateSlider();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            GameManagement.energy--;
            if (GameManagement.energy < 0) GameManagement.energy = 0;
            UpdateSlider();
        }
    }
}
