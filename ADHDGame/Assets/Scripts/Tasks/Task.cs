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
            InfoManager.instance.SendInfoMessage("Staring " + taskName + "...");

            //play animation
            TaskManager.instance.StartCoroutine(WaitForDuration());
        }
        else
        {
            InfoManager.instance.SendInfoMessage("Can't start " + taskName + " until " + waitingOnTask.taskName + " is done");
        }
    }

    public IEnumerator WaitForTask()
    {

        InfoManager.instance.SendInfoMessage(taskName + "will be done in " + waitingTime + " minutes");
        yield return new WaitForSeconds(waitingTime);

        InfoManager.instance.SendInfoMessage(taskName + " is ready!");

        status = TaskStatus_Enum.Done;
    }

    public IEnumerator WaitForDuration()
    {
        //start animation
        yield return new WaitForSeconds(duration);

        //end animation
        if (waitingTime > 0)
        {
            TaskManager.instance.StartCoroutine(WaitForTask());
        }
    }
}
