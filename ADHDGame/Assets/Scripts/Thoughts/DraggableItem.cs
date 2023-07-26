using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class
DraggableItem
: MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector]
    public Transform parentAfterDrag;

    public Thought_trigger thoughtTrigger;

    bool isDragging = false;

    void Start()
    {
        thoughtTrigger = transform.GetComponent<Thought_trigger>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin drag");
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        isDragging = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("trigger");
        if (isDragging)
        {
            thoughtTrigger.TriggerThought(other);
        }
    }
}
