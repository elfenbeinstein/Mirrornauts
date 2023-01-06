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

    [SerializeField] private float slideDuration = 1.5f;
    [SerializeField] private Animator anim;

    bool sliding;
    int amount;
    float time = 0;

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
            if (GameManagement._playerStats.energy + amount > GameManagement._playerStats.maxEnergy) amount = GameManagement._playerStats.maxEnergy - GameManagement._playerStats.energy;
            sliding = true;
            // set animation to true
            anim.SetBool("Move", true);
            time = 0;

            GameManagement._playerStats.energy += (int)param;
            if (GameManagement._playerStats.energy > GameManagement._playerStats.maxEnergy) GameManagement._playerStats.energy = GameManagement._playerStats.maxEnergy;
        }
        else if (eventName == "RemoveEnergy")
        {
            amount = (int)param * -1;
            sliding = true;
            // set animation to true
            anim.SetBool("Move", true);
            time = 0;

            GameManagement._playerStats.energy -= (int)param;
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

    private bool ReachedTarget()
    {
        bool result = false;
        if (_slider.value == GameManagement._playerStats.energy) result = true;
        else if (_slider.value < GameManagement._playerStats.energy && amount < 0) result = true;
        else if (_slider.value > GameManagement._playerStats.energy && amount > 0) result = true;

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
                    text.text = _slider.value.ToString() + "/" + GameManagement._playerStats.maxEnergy.ToString();
                    time = 0;

                    // check if reached the end:
                    if (ReachedTarget())
                    {
                        _slider.value = GameManagement._playerStats.energy;
                        text.text = _slider.value.ToString() + "/" + GameManagement._playerStats.maxEnergy.ToString();
                        sliding = false;
                        anim.SetBool("Move", false);
                    }
                }
            }
            else
            {
                _slider.value = GameManagement._playerStats.energy;
                text.text = _slider.value.ToString() + "/" + GameManagement._playerStats.maxEnergy.ToString();
                sliding = false;
                anim.SetBool("Move", false);
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            sliding = true;
            amount = Random.Range(-25, 26);
            if (amount + GameManagement._playerStats.energy > GameManagement._playerStats.maxEnergy) amount = GameManagement._playerStats.maxEnergy - GameManagement._playerStats.energy;
            time = 0;
            GameManagement._playerStats.energy += amount;
            anim.SetBool("Move", true);
        }




        // elfenbeinstein: REMOVE FOR BUILD
# if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            GameManagement._playerStats.energy++;
            if (GameManagement._playerStats.energy > GameManagement._playerStats.maxEnergy) GameManagement._playerStats.energy = GameManagement._playerStats.maxEnergy;
            UpdateSliderDirectly();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            GameManagement._playerStats.energy--;
            if (GameManagement._playerStats.energy < 0) GameManagement._playerStats.energy = 0;
            UpdateSliderDirectly();
        }
#endif
    }
}
