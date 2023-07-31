using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class
DraggableItem
: MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        TMP_Text thoughtTxt = transform.GetChild(0).GetComponent<TMP_Text>();
        thoughtTxt.SetText("Drag left to ignore");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TMP_Text thoughtTxt = transform.GetChild(0).GetComponent<TMP_Text>();
        thoughtTxt.SetText(GetComponent<thought_Transform>().thoughtText);
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
