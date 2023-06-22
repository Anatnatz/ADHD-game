using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskOnApp_Manager : MonoBehaviour
{
    public static TaskOnApp_Manager TaskOnAppInstance;

    [SerializeField]
    List<AppTransform> appTransforms;

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

    [SerializeField]
    Transform taskOnAppParent;

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
        AppTransform newAppTransform = Instantiate(appTransform_Prefab);
        serialNum++;

        //insert AppTransform info
        newAppTransform.taskType = taskType;

        //searchForTaskType(thoughtType);
        Task currentTask = TaskManager.instance.searchTaskOnList(taskType);

        //changeText:
        newAppTransform.appTransformText = currentTask.textInApp;
        newAppTransform.changeText();

        //change name:
        newAppTransform.gameObject.name =
            newAppTransform.appTransformText + serialNum;

        appTransforms.Add (newAppTransform);

        searchForTransformOnLIst(newAppTransform.transform.name);

        locationTaskOnAPP (currentAppTransformNum, newAppTransform);

        ChangeTaskOnAppStatus(TextOnApp_Enum.Appeared, currentAppTransformNum);
        newAppTransform.transform.SetParent (taskOnAppParent);
    }

    private void locationTaskOnAPP(
        int currentAppTransformNum,
        AppTransform transform
    )
    {
        Vector2 parentPosition = taskOnAppParent.transform.position;

        Vector2 position = transform.transform.position;

        float positiony = parentPosition.y + 2 - currentAppTransformNum;

        float positionx = parentPosition.x;
        position.x = positionx;
        position.y = positiony;
        transform.transform.position = position;
    }

    public void markAsDoneTaskOnApp(string name)
    {
        searchForTransformOnLIst (name);
        ChangeTaskOnAppStatus(TextOnApp_Enum.Marked_As_Done,
        currentAppTransformNum);
        AddToList (currentAppTransformNum, markedAsDoneOnAppTasks);
        appTransforms[currentAppTransformNum].gameObject.SetActive(false);
        RemoveFromList (currentAppTransformNum, appTransforms);
    }

    public void DeleteTaskFromApp(string name)
    {
        searchForTransformOnLIst (name);
        ChangeTaskOnAppStatus(TextOnApp_Enum.Deleted, currentAppTransformNum);
        AddToList (currentAppTransformNum, deletedFromAppTasks);
        appTransforms[currentAppTransformNum].gameObject.SetActive(false);
        RemoveFromList (currentAppTransformNum, appTransforms);
    }

    private void AddToList(
        int currentAppTransformNum,
        List<AppTransform> newList
    )
    {
        newList.Add(appTransforms[currentAppTransformNum]);
    }

    private void RemoveFromList(
        int currentAppTransformNum,
        List<AppTransform> list
    )
    {
        if (list == appTransforms)
        {
            repositionAppTransforms (currentAppTransformNum);
        }

        list.Remove(list[currentAppTransformNum]);
    }

    private void repositionAppTransforms(int currentAppTransformNum)
    {
        int nextAppTransform = currentAppTransformNum + 1;

        for (int i = nextAppTransform; i < appTransforms.Count; i++)
        {
            if (appTransforms[i] != null)
            {
                locationTaskOnAPP(currentAppTransformNum, appTransforms[i]);
            }
            else
            {
                return;
            }
            currentAppTransformNum++;
        }
    }

    internal void searchForTransformOnLIst(string name)
    {
        for (int i = 0; i < appTransforms.Count; i++)
        {
            if (appTransforms[i].gameObject.name == name)
            {
                currentAppTransformNum = i;
                break;
            }
        }
    }

    public void ChangeTaskOnAppStatus(
        TextOnApp_Enum taskOnAppStatus,
        int numOnList
    )
    {
        appTransforms[numOnList].taskOnAppStatus = taskOnAppStatus;
        ChangeTask_TaskOnAppStatus (taskOnAppStatus, numOnList);
    }

    private void ChangeTask_TaskOnAppStatus(
        TextOnApp_Enum taskOnAppStatus,
        int numOnList
    )
    {
        Task_Enum taskType = appTransforms[numOnList].taskType;
        TaskManager.instance.searchTaskOnList (taskType);
        TaskManager
            .instance
            .tasksList[TaskManager.instance.currentTaskNumOnList]
            .taskOnAppStatus = taskOnAppStatus;
    }
}
