using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOver : MonoBehaviour
{
    [SerializeField]
    Room_Object roomObject;
    [SerializeField]
    Thoughts_Manager Thoughts_Manager;


    private void OnTriggerEnter2D(Collider2D other)

    {
        Debug.Log(roomObject.thoughtType);
        Thoughts_Manager.triggerThought(roomObject.thoughtType);
    }

}
