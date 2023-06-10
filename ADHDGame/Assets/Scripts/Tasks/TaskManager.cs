using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static TaskManager instance;

    public RoomObject roomObject;

    Task task;

    void Start()
    {
        instance = this;
        roomObject = GetComponent<RoomObject>();
    }

    void OnMouseDown()
    {
        Debug.Log("clicked on" + roomObject.objectName);
    }

    public void StartTask(string name)
    {
        // task = taskList.allTasks.Find(t => t.taskName == name);
        // task.StartTask();
    }

    public void OnApplicationQuit()
    {
        // foreach (Task task in taskList.allTasks)
        // {
        //     task.isDone = false;
        // }
    }

    // public IEnumerator WaitForTask(Task current)
    // {
    //     Debug.Log("waiting on task to finish " + current.taskName);
    //     yield return new WaitForSeconds(current.waitingTime);
    //     Debug.Log("task is ready " + current.taskName);
    //     current.isDone = true;
    // }
}
