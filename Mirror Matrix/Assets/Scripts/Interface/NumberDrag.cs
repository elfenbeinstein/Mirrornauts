using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NumberDrag : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public float value;

    private RectTransform rectTransform;
    private Vector3 originalPosition;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition;
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // make sure field is at the very front 
        canvasGroup.alpha = 0.8f;
        canvasGroup.blocksRaycasts = false;
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
        // if hovering over the matrix --> set values
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    
   
}
