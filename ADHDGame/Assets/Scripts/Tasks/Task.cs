using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[
    CreateAssetMenu(
        fileName = "TaskScriptableObject",
        menuName = "ScriptableObjects/Task")
]
public class Task : ScriptableObject
{
    public string taskName = "task name";

    public Task_Enum taskType;

    public string textInApp;

    public float duration = 3f;

    public float waitingTime = 0f;

    public int score = 20;

    public TaskStatus_Enum status;

    public TextOnApp_Enum taskOnAppStatus;
    

    // public Status_Enum smtatus;
    public Animation animation;

    public Task waitingOnTask;

    public Thought_Enum blockingThought;

    public void StartTask()
    {
        Debug.Log("trying to start task" + taskName);
        if (
            waitingOnTask == null ||
            waitingOnTask.status == TaskStatus_Enum.Done
        )
        {
            Debug.Log("starting task" + taskName);

            //play animation
            TaskManager.instance.StartCoroutine(WaitForDuration());
        }
        else
        {
            Debug.Log("task failed to start" + taskName);
        }
    }

    public IEnumerator WaitForTask()
    {
        Debug.Log("waiting on task to finish" + taskName);
        yield return new WaitForSeconds(waitingTime);
        Debug.Log("task is ready" + taskName);
        status = TaskStatus_Enum.Done;
    }

    public IEnumerator WaitForDuration()
    {
        //start animation
        yield return new WaitForSeconds(duration);

        //end animation
        TaskManager.instance.StartCoroutine(WaitForTask());
    }
}
