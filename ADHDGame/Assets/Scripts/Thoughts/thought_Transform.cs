using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class thought_Transform : MonoBehaviour
{
    public Thought_Enum thoughtType;
    public Task_Enum taskType;
    public Vector2 thoughtPosition;
    public string thoughtText;
    public bool setText;
    public ThoughtStatus thoughtTransformStatus;
    public int positionOnApp;
    


    internal void changeStatuse(ThoughtStatus thoughtStatus)
    {
       
        Thoughts_Manager.ThoughtsInstance.changeThoughtStatus(thoughtType, thoughtStatus);
    }

    internal void changeText()
    {

        setText = true; 
       
    }

    internal void pushToApp()
    {
        Thoughts_Manager.ThoughtsInstance.searchForThoughtType(thoughtType);
        taskType =  Thoughts_Manager.ThoughtsInstance.thoughtsList_[Thoughts_Manager.ThoughtsInstance.currentThoughtNum].taskType;
        TaskOnApp_Manager.TaskOnAppInstance.createTaskOnAppTransform(taskType);

    }

    internal void updateNumOfAppearanceOnApp()
    {
        Thoughts_Manager.ThoughtsInstance.updateNumOfAppearanceOnApp(thoughtType);
    }
}
