using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class
DraggableItem: MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector]
    public Transform parentAfterDrag;

    public Thought_trigger thoughtTrigger;

    public string firstTextHover = "Drag left to ignore";
    public string secondTextHover = "Drag to the phone -> add to the todo list";

    static int thoughtCnt = 0;

    int thoughtIndex;

    bool isDragging = false;

    TMP_Text textComponent;

    Thought thought;

    void Start()
    {
        thoughtTrigger = transform.GetComponent<Thought_trigger>();
        textComponent = transform.GetChild(0).GetComponent<TMP_Text>();
        thoughtCnt++;
        thoughtIndex = thoughtCnt;
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
        if (thoughtIndex == 1)
        {
            textComponent.SetText(firstTextHover);

        }
        else if (thoughtIndex == 2)
        {
            textComponent.SetText(secondTextHover);

        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        textComponent.SetText(GetComponent<thought_Transform>().thoughtText);
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
