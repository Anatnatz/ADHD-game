using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskOnApp_Manager : MonoBehaviour
{
    
    public static TaskOnApp_Manager TaskOnAppInstance;
    [SerializeField] 
    List<AppTransform> appTransforms;
    [SerializeField]
     List<TaskOnAppPosition> appTransformPositions;
    [SerializeField]

    AppTransform appTransform_Prefab;
    [SerializeField]
    int currentAppTransformNum;
    [SerializeField]
    int freePosition;
    // Start is called before the first frame update
    void Start()
    {
        TaskOnAppInstance = this;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void createTaskOnAppTransform(Task_Enum taskType)
    {

        AppTransform newAppTransform = Instantiate (appTransform_Prefab, appTransform_Prefab.appTransformPosition, Quaternion.identity);
        newAppTransform.taskType = taskType;

        //searchForTaskType(thoughtType);
        newAppTransform.appTransformText = "Eat Breakfast";
        //newAppTransform.appTransformText = taskList_[currentThoughtNum].thoughtText;
        newAppTransform.changeText();
        newAppTransform.name = "Eat Breakfast";
        newAppTransform.gameObject.name = "Eat Breakfast";
        //  newAppTransform.name = thoughtsList_[currentThoughtNum].thoughtText;
        // changeTakStatus(thoughtType, ThoughtStatus.Appeared);
       
        appTransforms.Add (newAppTransform);
        
        searchForPositionOnApp();

        positionTaskOnApp(newAppTransform);
        
        searchForTransformOnLIst(newAppTransform.transform.name);
       
        ChangeTaskOnAppStatus(TextOnApp_Enum.Apeared, currentAppTransformNum);

    }

    internal void positionTaskOnApp(AppTransform tarnsformToPosition)
    {
        tarnsformToPosition.transform.position = appTransformPositions[freePosition].transform.position;
        appTransformPositions[freePosition].changePositionStatus(TaskOnAppPosition_Enum.Taken);
    }

    public void DeleteTaskOnApp(string name)
    {
        searchForTransformOnLIst(name);
        ChangeTaskOnAppStatus(TextOnApp_Enum.Marked_As_Done, currentAppTransformNum);
        appTransforms[currentAppTransformNum].gameObject.SetActive(false);
    }

    internal void searchForTransformOnLIst(string name)
    {
        for (int i = 0; i < appTransforms.Count; i++)
        {
            if (appTransforms[i].name == name)
            { currentAppTransformNum = i; break; }
        }
    }

    public void ChangeTaskOnAppStatus(TextOnApp_Enum taskOnAppStatus, int numOnList)
    {
        appTransforms[numOnList].taskOnAppStatus = taskOnAppStatus;
    }

    internal void searchForPositionOnApp()
    {
        for (int i = 0; i < appTransformPositions.Count; i++)
        {
         if (appTransformPositions[i] != null)
            {
                if (appTransformPositions[i].positionStatus == TaskOnAppPosition_Enum.Free)
                {
                freePosition= i;
                    return;
                }

            }
        }
    }
}
