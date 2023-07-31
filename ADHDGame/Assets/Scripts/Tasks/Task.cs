using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
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

    public int taskScore;

    public TaskStatus_Enum status;

    public TextOnApp_Enum taskOnAppStatus;

    public bool must;

    public string textIfNotDone;

    // public Status_Enum smtatus;
    public Animator animator;

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

   
    [SerializeField]
    Object_Enum connectedRoomObject;

    

    public float zoomNeeded;
    public Vector2 zoomLocation;
    

    public void StartTask(Animator taskAnimator)
    {
        animator = taskAnimator;
        StartTask();
    }

    public void StartTask()
    {
        Debug.Log("trying to start task" + taskName);
        if (
            waitingOnTask == null ||
            waitingOnTask.status == TaskStatus_Enum.Done
        )
        {
            if (connectedRoomObject != Object_Enum.None)
            {
                ZoomOnObject();
            }

            InfoManager.instance.SendInfoMessage("Staring " + taskName + "...");

            //play animation
            if (taskName == "Drink")
            {
                SoundManager.RegisterAction(SoundManager.SoundAction.drinkWater);
            }
            TaskManager.instance.StartCoroutine(WaitForDuration());

        }
        else
        {
            InfoManager.instance.SendInfoMessage("Can't start " + taskName + " until " + waitingOnTask.taskName + " is done");
        }
    }

    private void ZoomOnObject()
    {
        Room_Object objectToZoom = ObjectsManager.Instance.searchInList(connectedRoomObject);
        CameraController.cameraControllerInstance.ZoomOnObject(this);
    }

    public IEnumerator WaitForTask()
    {

        InfoManager.instance.SendInfoMessage(taskName + "will be done in " + waitingTime + " minutes");
        float waitUntil = PhoneController.instance.GetCurrentTime() + (waitingTime / 100f);
        while (PhoneController.instance.GetCurrentTime() < waitUntil)
        {
            Debug.Log(PhoneController.instance.GetCurrentTime() + " > " + (waitUntil) + " ... " + PhoneController.instance.gameMinute);
            yield return new WaitForSeconds(PhoneController.instance.gameMinute);
        }

        Debug.Log(PhoneController.instance.GetCurrentTime());

        InfoManager.instance.SendInfoMessage(taskName + " is ready!");

        status = TaskStatus_Enum.Done;
        SoundManager.RegisterAction(SoundManager.SoundAction.score);
        CheckFollowingAction();
    }

    public IEnumerator WaitForDuration()
    {
        //start animation
        Debug.Log(animator);
        if (animator != null)
        {
            animator.SetBool("isActive", true);
        }

        Cursor.lockState = CursorLockMode.Locked;
        Game_Manager.gameInstance.doingTask = true;
        yield return new WaitForSeconds(duration);
        PhoneController.instance.AddToTime((int)(duration - (duration / PhoneController.instance.gameMinute)));
        Cursor.lockState = CursorLockMode.None;
        Game_Manager.gameInstance.doingTask = false;
        

        //end animation
        if (animator != null)
        {
            animator.SetBool("isActive", false);
        }
        if (waitingTime > 0)
        {
            status = TaskStatus_Enum.Waiting;
            CheckFollowingAction();
            TaskManager.instance.StartCoroutine(WaitForTask());
        }
        else
        {
            status = TaskStatus_Enum.Done;
            SoundManager.RegisterAction(SoundManager.SoundAction.score);
            TaskOnApp_Manager.TaskOnAppInstance.UpdateTaskAsDone(taskType);
            checkTasksThought();
            Debug.Log(status.ToString());
            CheckFollowingAction();
            //TaskManager.instance.UpdateTotalScore(this);
            scoreController.instance.changeScore(taskScore);
            TaskManager.instance.numberOfTaskDone ++;
        }
    }

    private void checkTasksThought()
    {
        thought_Transform currentThoughtTransform = Thoughts_Manager.ThoughtsInstance.searchForThoughtTransformTypeByTask(taskType);
        if (currentThoughtTransform != null && currentThoughtTransform.thoughtTransformStatus == ThoughtStatus.Appeared)
        {
            currentThoughtTransform.gameObject.SetActive(false);
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
       
        MessageController.messageControlInstance.startWaitGapMessage(messageName);
    }

    private void TriggerThought(Thought_Enum thoughtType)
    {
        Thoughts_Manager.ThoughtsInstance.startWaitGapThought(thoughtType);
    }
}
