using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("Begin Drag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("On Drag");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("End Drag");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("Pointer Down");
    }
}
