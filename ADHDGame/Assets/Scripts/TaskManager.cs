using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static TaskManager instance;

    public TaskList tasks;

    Task task;

    void Start()
    {
        instance = this;
    }

    public void StartTask(string name)
    {
        task = tasks.taskList.Find(t => t.taskName == name);
        task.StartTask();
    }

    public void OnApplicationQuit()
    {
        foreach (Task task in tasks.taskList)
        {
            task.isDone = false;
        }
    }
}
