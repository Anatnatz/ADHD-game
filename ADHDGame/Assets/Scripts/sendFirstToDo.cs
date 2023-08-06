using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sendFirstToDo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TaskOnApp_Manager.TaskOnAppInstance.createTaskOnAppTransform(Task_Enum.GoOut);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
