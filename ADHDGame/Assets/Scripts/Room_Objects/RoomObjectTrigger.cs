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
        if(roomObject.relatedThoughts != null)
        { roomObject.objectTrigger(); }
    }
    internal void OnMouseDown()
    { 
        
        for (int i = 0; i < roomObject.relatedTasks.Count; i++)
        {
            if (roomObject.relatedTasks[i].taskType == Task_Enum.Turn_on_Coffemaker)
            {
                Debug.Log("triger coffee");
                if (roomObject.relatedTasks[i].waiting == true)
                {
                    Thoughts_Manager.ThoughtsInstance.triggerThought(Thought_Enum.Coffe_Not_Read);
                }
            }

        }
        
    }
}
