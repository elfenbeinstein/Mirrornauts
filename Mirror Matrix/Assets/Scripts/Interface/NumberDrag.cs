using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum ValueTypes
{
    Default,
    Wurzel2,
    Wurzel3
}

public class NumberDrag : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public float value;
    [Tooltip("set default if not special case")]
    public ValueTypes type;
    public TMPro.TextMeshProUGUI text;
    bool hasMinus;
    string origText;

    private RectTransform rectTransform;
    private Vector3 originalPosition;
    private CanvasGroup canvasGroup;

    private int n;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition;
        canvasGroup = GetComponent<CanvasGroup>();

        switch (type)
        {
            case ValueTypes.Default:
                break;
            case ValueTypes.Wurzel2:
                value = Mathf.Sqrt(2) / 2;
                break;
            case ValueTypes.Wurzel3:
                value = Mathf.Sqrt(3) / 2;
                break;
        }

        EventManager.Instance.AddEventListener("BUTTON", ButtonListener);
        hasMinus = false;
        origText = text.text;
    }

    private void OnDestroy()
    {
        EventManager.Instance.RemoveEventListener("BUTTON", ButtonListener);
    }

    private void ButtonListener(string eventName, object param)
    {
        if (eventName == "+-Button")
        {
            if (value == 0) return;
            if (hasMinus)
            {
                text.text = origText;
                hasMinus = false;
            }
            else
            {
                text.text = "-" + origText;
                hasMinus = true;
            }
            value *= -1;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        /*
        switch (type)
        {
            case ValueTypes.Default:
                break;
            case ValueTypes.Wurzel2:
                value = Mathf.Sqrt(2) / 2;
                if (hasMinus) value *= -1;
                break;
            case ValueTypes.Wurzel3:
                value = Mathf.Sqrt(3) / 2;
                if (hasMinus) value *= -1;
                break;
        }
        */

        // make sure field is at the very front 
        canvasGroup.alpha = 0.8f;
        canvasGroup.blocksRaycasts = false;
        EventManager.Instance.EventGo("DRAG", "Start");
        gameObject.transform.SetAsLastSibling();

        GameManagement._audioManager._sfxSounds.PlayDrag();
    }

    // gets called on every frame while object is being dragged
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition = originalPosition;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
        EventManager.Instance.EventGo("DRAG", "Stop");
        GameManagement._audioManager._sfxSounds.PlayDrag();
    }
}
