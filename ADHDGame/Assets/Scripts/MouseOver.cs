using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOver : MonoBehaviour
{
    [SerializeField]
    Objects_Data Objects_Data;
    [SerializeField]
    Thoughts_Manager Thoughts_Manager;


    private void OnTriggerEnter2D(Collider2D other)

    {
        Debug.Log(Objects_Data.thoughtType);
        Debug.Log (other.gameObject);
        Thoughts_Manager.triggerThought(Objects_Data.thoughtType);
    }

}
