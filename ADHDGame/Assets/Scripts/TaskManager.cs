using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static TaskManager instance;

    public InteractableObject interactableObject;

    Task task;

    void Start()
    {
        instance = this;
        interactableObject = GetComponent<InteractableObject>();
    }

    void OnMouseDown()
    {
        Debug.Log("clicked on" + interactableObject.objectName);
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
