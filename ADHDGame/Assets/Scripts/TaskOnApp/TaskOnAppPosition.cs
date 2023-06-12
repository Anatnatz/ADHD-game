using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskOnAppPosition : MonoBehaviour
{
    public TaskOnAppPosition_Enum positionStatus;

    // Start is called before the first frame update
    void Start()
    {
        positionStatus = TaskOnAppPosition_Enum.Free;  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changePositionStatus(TaskOnAppPosition_Enum newstatus)
    {
        positionStatus = newstatus;
    }
}
