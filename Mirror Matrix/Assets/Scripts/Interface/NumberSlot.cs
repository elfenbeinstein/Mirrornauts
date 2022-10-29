using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NumberSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private InputGameValues _inputGameValues;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            float value = eventData.pointerDrag.GetComponent<NumberDrag>().value;
            // check if value is possible with remaining energy level
            // if not --> error message to player - not enough energy left
            // if yes --> set value
            _inputGameValues.SetMatrix(value);
        }
    }
}
