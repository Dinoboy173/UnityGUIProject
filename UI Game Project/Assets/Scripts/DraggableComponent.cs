using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableComponent : MonoBehaviour, IInitializePotentialDragHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public event Action<PointerEventData> onBeginDragHandler;
    public event Action<PointerEventData> onDragHandler;
    public event Action<PointerEventData, bool> onEndDragHandler;

    public bool followCursor { get; set; } = true;
    public Vector3 startPos;
    public bool canDrag { get; set; } = true;

    public GameObject startParent = null;
    private RectTransform rectTransform;
    private Canvas canvas;

    public virtual void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!canDrag)
        {
            return;
        }

        transform.SetParent(GetComponentInParent<Canvas>().transform);

        onBeginDragHandler?.Invoke(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!canDrag)
        {
            return;
        }

        onDragHandler?.Invoke(eventData);

        if (followCursor)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!canDrag)
        {
            return;
        }

        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        DropArea dropArea = null;

        foreach (var result in results)
        {
            dropArea = result.gameObject.GetComponent<DropArea>();

            if (dropArea != null)
            {
                break;
            }
        }

        if (dropArea != null)
        {
            if (dropArea.Accetps(this))
            {
                dropArea.Drop(this);
                onEndDragHandler?.Invoke(eventData, true);
                return;
            }
        }

        transform.SetParent(startParent.transform);
        rectTransform.anchoredPosition = startPos;
        onEndDragHandler?.Invoke(eventData, false);
    }

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        startPos = rectTransform.anchoredPosition;
        startParent = transform.parent.gameObject;
    }
}