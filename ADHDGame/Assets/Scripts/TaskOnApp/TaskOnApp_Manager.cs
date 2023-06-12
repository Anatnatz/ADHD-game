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
    
    public List<AppTransform> markedAsDoneOnAppTasks;
    public List<AppTransform> deletedFromAppTasks;
    [SerializeField]
    AppTransform appTransform_Prefab;
    [SerializeField]
    int currentAppTransformNum;
    [SerializeField]
    int freePosition;
    [SerializeField]
    int serialNum = 0;
    [SerializeField]
    int numOfPositions = 4;

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
        //create new appTransform

        AppTransform newAppTransform = Instantiate (appTransform_Prefab, appTransform_Prefab.appTransformPosition, Quaternion.identity);
        serialNum++;
        
        //insert AppTransform info

        newAppTransform.taskType = taskType;

        //searchForTaskType(thoughtType);
        TaskManager.instance.searchTaskOnList(taskType);
        
        //changeText:
        newAppTransform.appTransformText = TaskManager.instance.tasksList[TaskManager.instance.currentTaskNumOnList].textInApp;
        newAppTransform.changeText();
       //change name:
        newAppTransform.gameObject.name = newAppTransform.appTransformText + serialNum;
        
        appTransforms.Add (newAppTransform);
        
                        
        searchForTransformOnLIst(newAppTransform.transform.name);
        
        positionTaskOnApp(currentAppTransformNum);

        ChangeTaskOnAppStatus(TextOnApp_Enum.Appeared, currentAppTransformNum);

        

    }

    private void positionTaskOnApp(int transformToPosition)
    {
        if (currentAppTransformNum + 1 <= numOfPositions)

        {
            searchForPositionOnApp();

            positionTaskOnApp(appTransforms[transformToPosition]);
        }
        else 
        {
            appTransforms[currentAppTransformNum].gameObject.SetActive(false);
        }
    }

    private void AddToList(int currentAppTransformNum, List<AppTransform> newList)
    {
        newList.Add(appTransforms[currentAppTransformNum]);
       
    }

    internal void positionTaskOnApp(AppTransform tarnsformToPosition)
    {
        tarnsformToPosition.transform.position = appTransformPositions[freePosition].transform.position;
        appTransformPositions[freePosition].changePositionStatus(TaskOnAppPosition_Enum.Taken);
        tarnsformToPosition.positionOnApp = freePosition;
    }

    public void markAsDoneTaskOnApp(string name)
    {
        searchForTransformOnLIst(name);
        ChangeTaskOnAppStatus(TextOnApp_Enum.Marked_As_Done, currentAppTransformNum);
        appTransformPositions[appTransforms[currentAppTransformNum].positionOnApp].changePositionStatus(TaskOnAppPosition_Enum.Free);
        AddToList(currentAppTransformNum, markedAsDoneOnAppTasks);
        appTransforms[currentAppTransformNum].gameObject.SetActive(false);
        RemoveFromList(currentAppTransformNum, appTransforms);
    }

    public void DeleteTaskFromApp(string name)
    {
        searchForTransformOnLIst(name);
        ChangeTaskOnAppStatus(TextOnApp_Enum.Deleted, currentAppTransformNum);
        appTransformPositions[appTransforms[currentAppTransformNum].positionOnApp].changePositionStatus(TaskOnAppPosition_Enum.Free);
        AddToList(currentAppTransformNum, deletedFromAppTasks);
        appTransforms[currentAppTransformNum].gameObject.SetActive(false);
        RemoveFromList(currentAppTransformNum, appTransforms);
    }

    private void RemoveFromList(int currentAppTransformNum, List<AppTransform> list)
    {
       // if (list == appTransforms)
       // {
       //     if (currentAppTransformNum + 1 <= numOfPositions)
       //     { repositionAppTransforms(currentAppTransformNum); }
            
       // }
        list.Remove(list[currentAppTransformNum]);
    }

    private void repositionAppTransforms(int currentAppTransformNum)
    {
        int nextAppTransform = currentAppTransformNum + 1;

        for (int i = nextAppTransform; i < numOfPositions - currentAppTransformNum; i++)
        {
            if (appTransforms[i] != null)
            {
                freePosition = appTransforms[i - 1].positionOnApp + 1;

                positionTaskOnApp(appTransforms[i]);
            }
            else { return; }
        }
    }

    internal void searchForTransformOnLIst(string name)
    {
        for (int i = 0; i < appTransforms.Count; i++)
        {
            if (appTransforms[i].gameObject.name == name)
            { currentAppTransformNum = i; break; }
        }
    }

    public void ChangeTaskOnAppStatus(TextOnApp_Enum taskOnAppStatus, int numOnList)
    {
        appTransforms[numOnList].taskOnAppStatus = taskOnAppStatus;
        ChangeTask_TaskOnAppStatus(taskOnAppStatus, numOnList);
    }

    private void ChangeTask_TaskOnAppStatus(TextOnApp_Enum taskOnAppStatus, int numOnList)
    {
        Task_Enum taskType = appTransforms[numOnList].taskType;
        TaskManager.instance.searchTaskOnList(taskType);
        TaskManager.instance.tasksList[TaskManager.instance.currentTaskNumOnList].taskOnAppStatus = taskOnAppStatus;
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
