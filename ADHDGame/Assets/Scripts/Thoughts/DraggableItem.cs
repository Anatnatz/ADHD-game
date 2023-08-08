using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class
DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector]
    public Transform parentAfterDrag;

    public Thought_trigger thoughtTrigger;

    public string normalTextHover = "Drag left to ignore";
    public string taskTextHover = "Drag to the phone -> add to the todo list";
    string currentTextHover;

    static int thoughtCnt = 0;
    static bool firstNormalThought;
    static bool firstTaskThought;

    int thoughtIndex;

    bool isDragging = false;

    TextMeshProUGUI textComponent;

    Thought thought;

    thought_Transform thoughtTransform;

    Color originalColor;

    void Start()
    {
        thoughtTrigger = transform.GetComponent<Thought_trigger>();
        textComponent = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        thoughtTransform = GetComponent<thought_Transform>();
        thoughtCnt++;
        thoughtIndex = thoughtCnt;

        if (thoughtTransform.IsItATask)
        {
            currentTextHover = taskTextHover;
            firstTaskThought = true;
        }
        else if (!thoughtTransform.IsItATask)
        {
            currentTextHover = normalTextHover;
            firstNormalThought = true;
        }
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
        if (currentTextHover != null && currentTextHover != "")
        {
            textComponent.SetText(currentTextHover);
            originalColor = textComponent.color;
            textComponent.color = new Color32(42, 82, 190, 255);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (currentTextHover != null && currentTextHover != "")
        {
            textComponent.SetText(thoughtTransform.thoughtText);
            textComponent.color = originalColor;
        }
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
