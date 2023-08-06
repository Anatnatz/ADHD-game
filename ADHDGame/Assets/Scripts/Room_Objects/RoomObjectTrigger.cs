using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomObjectTrigger : MonoBehaviour
{
    [SerializeField]
    RoomObject roomObject;

    // private void OnTriggerEnter2D(Collider2D collision)
    //{
    //  if (collision.tag == Tags_Enum.Mouse.ToString() && roomObject.relatedThoughts != null)
    //{
    //  roomObject.objectTrigger();
    //}
    //}

    private void OnMouseEnter()
    {
        if (roomObject.relatedThoughts != null)
        { roomObject.objectTrigger(); }
    }
}
