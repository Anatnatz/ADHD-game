using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class PhoneDetectClick : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    public void OnBeginDrag(PointerEventData eventData)
    {
        PhoneController.noTouchTime = 0;
        Debug.Log("timeReset");
    }

    public void OnDrag(PointerEventData eventData)
    {
        PhoneController.noTouchTime = 0;
        Debug.Log("timeReset");
    }
}
