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

    public float duration = 3f;

    public float waitingTime = 0f;

    public int score = 20;

    public bool isDone = false;

    public Animation animation;

    public Task waitingOnTask;

    public void StartTask()
    {
        Debug.Log("trying to start task" + taskName);
        if (waitingOnTask == null || waitingOnTask.isDone == true)
        {
            Debug.Log("starting task" + taskName);

            //play animation
            TaskManager.instance.StartCoroutine(WaitForTask());
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
        isDone = true;
    }
}
