using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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

    [Header("Following When Waiting")]

    [SerializeField]
    List<MessageName_Enum> followingMessagesWhenWaiting;

    [SerializeField]
    List<Thought_Enum> followingThoughtsWhenWaiting;

    [Header("Following When Done")]

    [SerializeField]
    List<MessageName_Enum> followingMessagesWhenDone;

    [SerializeField]
    List<Thought_Enum> followingThoughtsWhenDone;




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


    public void update()
    {
        switch (status)
        {

            case TaskStatus_Enum.none:
                { break; }

            case TaskStatus_Enum.Waiting:
                {
                    checkFollowingMessage(TaskStatus_Enum.Waiting);
                    checkFollowingThoughts(TaskStatus_Enum.Waiting);
                    break;
                }
            case TaskStatus_Enum.Done:
                {
                    checkFollowingMessage(TaskStatus_Enum.Done);
                    checkFollowingThoughts(TaskStatus_Enum.Done);
                    break;
                }
        }
    }

    private void checkFollowingThoughts(TaskStatus_Enum status)
    {
        if (status == TaskStatus_Enum.Waiting)
        {
            for (int i = 0; i < followingThoughtsWhenWaiting.Count; i++)
            {
                if (followingThoughtsWhenWaiting[i] != null)
                {
                    TriggerThought(followingThoughtsWhenWaiting[i]);
                }
            }
        }

        if (status == TaskStatus_Enum.Done)
        {
            for (int i = 0; i < followingThoughtsWhenDone.Count; i++)
            {
                if (followingThoughtsWhenDone[i] != null)
                {
                    TriggerThought(followingThoughtsWhenDone[i]);
                }
            }
        }
    }

    private void checkFollowingMessage(TaskStatus_Enum status)
    {
        if (status == TaskStatus_Enum.Waiting)
        {
            for (int i = 0; i < followingMessagesWhenWaiting.Count; i++)
            {
                if (followingMessagesWhenWaiting[i] != null)
                {
                    TriggerMessage(followingMessagesWhenWaiting[i]);
                }
            }
        }

        if (status == TaskStatus_Enum.Done)
        {
            for (int i = 0; i < followingMessagesWhenDone.Count; i++)
            {
                if (followingMessagesWhenDone[i] != null)
                {
                    TriggerMessage(followingMessagesWhenDone[i]);
                }
            }
        }
    }

    private void TriggerMessage(MessageName_Enum messageName)
    {
        MessageController.messageControlInstance.SendMessage(messageName);
    }

    private void TriggerThought(Thought_Enum thoughtType)
    {
        Thoughts_Manager.ThoughtsInstance.createThought(thoughtType);
    }
}
