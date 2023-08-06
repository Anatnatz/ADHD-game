using System;
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
    public int totalScore = 0;

    public int currentTaskNumOnList;

    GameObject buttonsSpace;

    RectTransform rectTransform;

    [SerializeField]
    List<Task> mustTasks;

    public List<Task> NotComplitedMustTaskList;

    public List<Task> complitedTasks;

    public int numberOfTaskDone = 0;

    public static RoomObject clickedOn;


    void Start()
    {
        instance = this;
        roomObject = GetComponent<RoomObject>();
        buttonsSpace = GameObject.Find("ButtonsSpace");
        rectTransform = buttonsSpace.GetComponent<RectTransform>();
        creatMustTasksList();
    }



    void Update()
    {
        if (buttonsSpace.transform.childCount > 0)
        {
            float width = rectTransform.sizeDelta.x;
            float height = rectTransform.sizeDelta.y;
            Vector3 rectPosition = rectTransform.position;
            Vector3 mousePos = Input.mousePosition;
            Vector3 offSetVector = new Vector3(100f, 300f, 0f);


            if (mousePos.x + 600f > 1920f)
            {
                offSetVector = new Vector3(500f, 300f, 0f);
            }

            bool mouseBefore =
                mousePos.x < rectPosition.x - offSetVector.x ||
                mousePos.y < rectPosition.y - offSetVector.y;
            bool mouseAfter =
                mousePos.x > rectPosition.x + width + offSetVector.x ||
                mousePos.y > rectPosition.y + height + (offSetVector.y / 3);

            if (mouseBefore || mouseAfter)
            {
                for (int i = 0; i < buttonsSpace.transform.childCount; i++)
                {
                    clickedOn.animator.SetBool("isClicked", false);
                    Destroy(buttonsSpace.transform.GetChild(i).gameObject);
                }

                // TaskButtonController.instance.ButtonsChanged();
            }
        }

        if (numberOfTaskDone > 5)
        { Thoughts_Manager.ThoughtsInstance.triggerThought(Thought_Enum.Almost_late); }
    }

    public void creatMustTasksList()
    {
        for (int i = 0; i < mustTasks.Count; i++)
        {
            mustTasks.Remove(mustTasks[i]);
        }

        for (int i = 0; i < tasksList.Count; i++)
        {
            if (tasksList[i].must == true)
            { mustTasks.Add(tasksList[i]); }
        }
    }

    public void creatComplitedTaskList()
    {
        for (int i = 0; i < complitedTasks.Count; i++)
        {
            complitedTasks.Remove(complitedTasks[i]);

        }

        for (int i = 0; i < tasksList.Count; i++)
        {
            if (tasksList[i].status == TaskStatus_Enum.Done)
            {
                complitedTasks.Add(tasksList[i]);
            }

        }
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
        searchTaskOnList(taskType);
        tasksList[currentTaskNumOnList].status = status;

        if (status == TaskStatus_Enum.Done)
        {
            UpdateTotalScore(tasksList[currentTaskNumOnList]);

        }
    }

    public void UpdateTotalScore(Task taskForScore)
    {
        // totalScore += taskForScore.score;
        //InfoManager.instance.SendInfoMessage("Your score:" + totalScore);
        
    }

    public void StartTask(Button taskBtn)
    {
        string taskName = taskBtn.name;
        Task curTask = tasksList.Find(t => t.taskName == taskName);
        curTask.StartTask();
        Destroy(taskBtn.gameObject);
    }

    internal void checkMustTasksList()
    {

        //Check must list:

        for (int i = 0; i < mustTasks.Count; i++)
        {
            if (mustTasks[i].status != TaskStatus_Enum.Done)
            {
                EndLevel.instance.unDoneMustLIst.Add(mustTasks[i]);
            }

        }


    }

    internal bool IsTaskDone(Task_Enum taskType)
    {
        bool isDone = false;

        for (int i = 0; i < tasksList.Count; i++)
        {
            if (tasksList[i].taskType == taskType)
            {
                if (tasksList[i].status == TaskStatus_Enum.Done)
                { isDone = true; }
                else { isDone = false; }
            }

        }

        if (isDone) { return true; }
        else { return false; }
    }

    void OnApplicationQuit()
    {
        foreach (Task task in tasksList)
        {
            task.status = TaskStatus_Enum.none;
        }
    }
}
