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

public class NumberSlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private InputGameValues _inputGameValues;
    [SerializeField] private GameObject highlight;
    [Tooltip("select all canvas groups that should not be interactable while dragging")]
    [SerializeField] private CanvasGroup[] valuesCanvasGroup;
    [SerializeField] private SlotType type;

    private void Start()
    {
        highlight.SetActive(false);
        EventManager.Instance.AddEventListener("DRAG", DragListener);
    }

    private void OnDestroy()
    {
        EventManager.Instance.RemoveEventListener("DRAG", DragListener);
    }

    private void DragListener(string eventName, object param)
    {
        if (eventName == "Start")
        {
            for (int i = 0; i < valuesCanvasGroup.Length; i++)
            {
                valuesCanvasGroup[i].blocksRaycasts = false;
                valuesCanvasGroup[i].interactable = false;
            }
        }
        else if (eventName == "Stop")
        {
            for (int i = 0; i < valuesCanvasGroup.Length; i++)
            {
                valuesCanvasGroup[i].blocksRaycasts = true;
                valuesCanvasGroup[i].interactable = true;
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            float value = eventData.pointerDrag.GetComponent<NumberDrag>().value;
            string text = eventData.pointerDrag.GetComponent<NumberDrag>().text.text;

            // check if value is possible with remaining energy level
            // if not --> error message to player - not enough energy left
            // if yes --> set value

            _inputGameValues.SetSlot(value, type, text);

            highlight.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            highlight.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            highlight.SetActive(false);
        }
    }
}
