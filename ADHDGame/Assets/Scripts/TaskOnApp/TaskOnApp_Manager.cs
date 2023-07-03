using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskOnApp_Manager : MonoBehaviour
{
    public static TaskOnApp_Manager TaskOnAppInstance;

    [SerializeField]
    List<AppTransform> appTransforms;

    public List<AppTransform> markedAsDoneOnAppTasks;

    public List<AppTransform> deletedFromAppTasks;

    [SerializeField]
    GameObject appTransform_Prefab;

    [SerializeField]
    int currentAppTransformNum;

    [SerializeField]
    int freePosition;

    [SerializeField]
    int serialNum = 0;

 
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
        GameObject newAppTask = Instantiate(appTransform_Prefab);
        AppTransform newAppTransform = newAppTask.GetComponent<AppTransform>();
        serialNum++;

        //insert AppTransform info
        newAppTransform.taskType = taskType;

        //searchForTaskType(thoughtType);
        Task currentTask = TaskManager.instance.searchTaskOnList(taskType);

        //changeText:
        newAppTransform.appTransformText = currentTask.textInApp;
        TMP_Text appTaskTxt =
            newAppTask.transform.GetChild(1).GetComponent<TMP_Text>();
        appTaskTxt.SetText(currentTask.textInApp);
        newAppTransform.changeText();

        //change name:
        newAppTransform.gameObject.name =
            newAppTransform.appTransformText + serialNum;

        appTransforms.Add (newAppTransform);

        searchForTransformOnLIst(newAppTransform.transform.name);

        // locationTaskOnAPP (currentAppTransformNum, newAppTransform);
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

    public void changeStatus(string name, TextOnApp_Enum status)
    {
        searchForTransformOnLIst (name);
        ChangeTaskOnAppStatus(status,currentAppTransformNum);
        AddToList (currentAppTransformNum, markedAsDoneOnAppTasks);
        appTransforms[currentAppTransformNum].gameObject.SetActive(false);
        RemoveFromList (currentAppTransformNum, appTransforms);
    }

    

    private void AddToList( int currentAppTransformNum, List<AppTransform> newList)
    {
        newList.Add(appTransforms[currentAppTransformNum]);
    }

    private void RemoveFromList( int currentAppTransformNum, List<AppTransform> list)
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

    public void ChangeTaskOnAppStatus( TextOnApp_Enum taskOnAppStatus, int numOnList )
    {
        appTransforms[numOnList].taskOnAppStatus = taskOnAppStatus;
        ChangeTask_TaskOnAppStatus (taskOnAppStatus, numOnList);
    }

    private void ChangeTask_TaskOnAppStatus(TextOnApp_Enum taskOnAppStatus,  int numOnList)
    {
        Task_Enum taskType = appTransforms[numOnList].taskType;
        TaskManager.instance.searchTaskOnList (taskType);
        TaskManager .instance .tasksList[TaskManager.instance.currentTaskNumOnList].taskOnAppStatus = taskOnAppStatus;
    }
}
