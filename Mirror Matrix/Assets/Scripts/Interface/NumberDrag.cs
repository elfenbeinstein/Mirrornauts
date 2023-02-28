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

/// <summary>
/// Sits On Number Fields that can be Dragged Into the Calculation
/// Holds Reference for their Value
/// Handles Dragging Motion and Snapping Back Into Place
/// </summary>

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

    [SerializeField] private Canvas canvas;

    Vector3 offset;

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
        if (!canvasGroup.interactable) return;

        // make sure field is at the very front 
        canvasGroup.alpha = 0.8f;
        canvasGroup.blocksRaycasts = false;
        EventManager.Instance.EventGo("DRAG", "Start");
        gameObject.transform.SetAsLastSibling();

        EventManager.Instance.EventGo("AUDIO", "PlayDrag");

        offset = Input.mousePosition - transform.position;
    }

    // gets called on every frame while object is being dragged
    public void OnDrag(PointerEventData eventData)
    {
        if (!canvasGroup.interactable) return;

        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform,
            Input.mousePosition - offset,
            canvas.worldCamera,
            out position);

        transform.position = canvas.transform.TransformPoint(position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!canvasGroup.interactable) return;
        rectTransform.anchoredPosition = originalPosition;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
        EventManager.Instance.EventGo("DRAG", "Stop");
        EventManager.Instance.EventGo("AUDIO", "PlayDrag");
    }
}
