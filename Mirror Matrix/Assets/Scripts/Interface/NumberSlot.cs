using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum SlotType
{
    MatrixRadAll,
    MatrixAX1,
    MatrixAX2,
    MatrixAY1,
    MatrixAY2,
    AddX,
    AddY
}

/// <summary>
/// Holds Calculation Slots + Sits On Calculation Elements
/// Once Number is Dragged and Dropped Onto It --> Takes Value Into Calculation
/// </summary>

public class NumberSlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private InputGameValues _inputGameValues;
    [SerializeField] private GameObject highlight;
    [Tooltip("select all canvas groups that should not be interactable while dragging")]
    [SerializeField] private CanvasGroup[] valuesCanvasGroup;
    [SerializeField] private SlotType type;
    private CanvasGroup canvas;

    private void Start()
    {
        highlight.SetActive(false);
        EventManager.Instance.AddEventListener("DRAG", DragListener);
        canvas = GetComponent<CanvasGroup>();
    }

    private void OnDestroy()
    {
        EventManager.Instance.RemoveEventListener("DRAG", DragListener);
    }

    private void DragListener(string eventName, object param)
    {
        if (canvas != null && !canvas.interactable) return;

        if (eventName == "Start")
        {
            for (int i = 0; i < valuesCanvasGroup.Length; i++)
            {
                if (valuesCanvasGroup[i] != null) valuesCanvasGroup[i].blocksRaycasts = false;
                if (valuesCanvasGroup[i] != null) valuesCanvasGroup[i].interactable = false;
            }
        }
        else if (eventName == "Stop")
        {
            for (int i = 0; i < valuesCanvasGroup.Length; i++)
            {
                if (valuesCanvasGroup[i] != null) valuesCanvasGroup[i].blocksRaycasts = true;
                if (valuesCanvasGroup[i] != null) valuesCanvasGroup[i].interactable = true;
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (canvas != null && !canvas.interactable) return;

        if (eventData.pointerDrag != null)
        {
            float value = eventData.pointerDrag.GetComponent<NumberDrag>().value;
            string text = eventData.pointerDrag.GetComponent<NumberDrag>().text.text;

            _inputGameValues.SetSlot(value, type, text);

            highlight.SetActive(false);

            EventManager.Instance.EventGo("AUDIO", "PlayDrop");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (canvas != null && !canvas.interactable) return;

        if (eventData.pointerDrag != null)
        {
            highlight.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (canvas != null && !canvas.interactable) return;

        if (eventData.pointerDrag != null)
        {
            highlight.SetActive(false);
        }
    }
}
