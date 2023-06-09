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
        CheckFollowingAction();
    }

    public IEnumerator WaitForDuration()
    {
        //start animation
        yield return new WaitForSeconds(duration);

        //end animation
        if (waitingTime > 0)
        {
            status = TaskStatus_Enum.Waiting;
            CheckFollowingAction();
            TaskManager.instance.StartCoroutine(WaitForTask());
        }
        else
        {
            status = TaskStatus_Enum.Done;
            CheckFollowingAction();
        }
    }


    public void CheckFollowingAction()
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

                TriggerThought(followingThoughtsWhenWaiting[i]);

            }
        }

        if (status == TaskStatus_Enum.Done)
        {
            for (int i = 0; i < followingThoughtsWhenDone.Count; i++)
            {

                TriggerThought(followingThoughtsWhenDone[i]);

            }
        }
    }

    private void checkFollowingMessage(TaskStatus_Enum status)
    {
        if (status == TaskStatus_Enum.Waiting)
        {
            for (int i = 0; i < followingMessagesWhenWaiting.Count; i++)
            {

                TriggerMessage(followingMessagesWhenWaiting[i]);

            }
        }

        if (status == TaskStatus_Enum.Done)
        {
            for (int i = 0; i < followingMessagesWhenDone.Count; i++)
            {

                TriggerMessage(followingMessagesWhenDone[i]);

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
