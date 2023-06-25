using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    public static TaskManager instance;

    public RoomObject roomObject;

    public List<Task> tasksList;

    Task task;

    public int currentTaskNumOnList;

    void Start()
    {
        instance = this;
        roomObject = GetComponent<RoomObject>();
    }

    void OnMouseDown()
    {
        Debug.Log("clicked on" + roomObject.objectName);
    }

    public Task searchTaskOnList(Task_Enum taskType)
    {
        for (int i = 0; i < tasksList.Count; i++)
        {
            if (tasksList[i].taskType == taskType)
            {
                currentTaskNumOnList = i;
                return tasksList[i];
            }
        }

        return null;
    }

    public void UpdateTaskStatus(Task_Enum taskType, TaskStatus_Enum status)
    {
        searchTaskOnList (taskType);
        tasksList[currentTaskNumOnList].status = status;
    }

    public void StartTask(Button taskBtn)
    {
        string taskName = taskBtn.name;
        Task curTask = tasksList.Find(t => t.taskName == taskName);
        curTask.StartTask();
        Destroy(taskBtn.gameObject);
    }
}
