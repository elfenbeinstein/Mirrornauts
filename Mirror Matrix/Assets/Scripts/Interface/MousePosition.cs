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

    // Update is called once per frame
    void Update()
    {
        //Get mouse world position
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Check that mouse Pos is in coordinate system
        if (mousePos.x <= maxCoordinats.x && mousePos.y <= maxCoordinats.y)
        {
            if(mousePos.x >= -maxCoordinats.x && mousePos.y >= -maxCoordinats.y)
            {
                if (Input.GetKeyDown(KeyCode.LeftControl)) GameManagement._audioManager._sfxSounds.PlayHover();
                if (Input.GetKey(KeyCode.LeftControl))
                {
                    mouseCoord.GetComponentInChildren<TextMeshProUGUI>().text = mousePos.x.ToString("F1") + " / " + mousePos.y.ToString("F1");
                    mouseCoord.gameObject.SetActive(true);
                    mouseCoord.gameObject.transform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, new Vector2 (mousePos.x + textBoxOffset.x, mousePos.y + textBoxOffset.y));
                }
                else
                {
                    mouseCoord.gameObject.SetActive(false);
                    GameManagement._audioManager._sfxSounds.StopHover();
                }
            }
            else
            {
                mouseCoord.gameObject.SetActive(false);
                GameManagement._audioManager._sfxSounds.StopHover();
            }
        }
        else
        {
            mouseCoord.gameObject.SetActive(false);
            GameManagement._audioManager._sfxSounds.StopHover();
        }
    }
}
