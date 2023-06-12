using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomObjectTrigger : MonoBehaviour
{
    [SerializeField] Room_Object roomObject;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Tags_Enum.Mouse.ToString())
        {

            Debug.Log(roomObject.thoughtType);
            
            Thoughts_Manager.ThoughtsInstance.triggerThought(roomObject.thoughtType);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
