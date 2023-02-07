using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MousePosition : MonoBehaviour
{
    /// <summary>
    /// Gets the mouse position and translates it into coordinates when holding Ctrl
    /// It gets displayed next to mouse
    /// </summary>
    /// 
    [SerializeField] Vector2 maxCoordinats;
    [SerializeField] Vector2 textBoxOffset;
    [SerializeField] GameObject mouseCoord;
    Vector3 mousePos;
    TMPro.TextMeshProUGUI textField;

    private bool active;
    private bool playerWIN;

    private void Start()
    {
        playerWIN = false;
        textField = mouseCoord.GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update()
    {
        if (playerWIN) return;

        //Get mouse world position
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // if left control is first pressed, check if over the coordinate system --> set active
        if (Input.GetKeyDown(KeyCode.LeftControl) && MouseOverCoordinateSystem())
            SetActive(true);

        if (active)
        {
            // if still over coordinate system, display coordinate stuff
            if (MouseOverCoordinateSystem())
            {
                // display coordinate stuff
                textField.text = mousePos.x.ToString("F1") + " / " + mousePos.y.ToString("F1");
                mouseCoord.gameObject.transform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, new Vector2(mousePos.x + textBoxOffset.x, mousePos.y + textBoxOffset.y));
            }
            else // stop being active
                SetActive(false);
        }

        if (Input.GetKey(KeyCode.LeftControl) && MouseOverCoordinateSystem() && !active)
            SetActive(true);

        // if still active (so over coordinate system) but stop pressing key --> set inactive and stop playing sound
        if (Input.GetKeyUp(KeyCode.LeftControl) && active)
            SetActive(false);
    }

    private void SetActive(bool value)
    {
        active = value;
        mouseCoord.gameObject.SetActive(value);
        if (value) EventManager.Instance.EventGo("AUDIO", "PlayHover"); //GameManagement._audioManager._sfxSounds.PlayHover();
        else EventManager.Instance.EventGo("AUDIO", "StopHover"); //GameManagement._audioManager._sfxSounds.StopHover();
    }

    private bool MouseOverCoordinateSystem()
    {
        if (mousePos.x <= maxCoordinats.x && mousePos.y <= maxCoordinats.y && mousePos.x >= -maxCoordinats.x && mousePos.y >= -maxCoordinats.y) return true;
        else return false;
    }

    public void PlayerWin()
    {
        if (active) SetActive(false);
        playerWIN = true;
    }
}
