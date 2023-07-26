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

    [SerializeField]
    List<Task> NotComplitedMustTaskList;

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
                    Debug.Log(mousePos + " : " + rectPosition);
                    Debug.Log(buttonsSpace.transform.GetChild(i).gameObject);
                    Destroy(buttonsSpace.transform.GetChild(i).gameObject);
                }

                // TaskButtonController.instance.ButtonsChanged();
            }
        }
    }

    private void creatMustTasksList()
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
        totalScore += taskForScore.score;
        InfoManager.instance.SendInfoMessage("Your score:" + totalScore);
    }

    public void StartTask(Button taskBtn)
    {
        string taskName = taskBtn.name;
        Task curTask = tasksList.Find(t => t.taskName == taskName);
        curTask.StartTask();
        Destroy(taskBtn.gameObject);
    }

    internal bool checkMustTasksList()
    {
        //Reset lists:

        for (int i = 0; i < NotComplitedMustTaskList.Count; i++)
        {
            NotComplitedMustTaskList.Remove(NotComplitedMustTaskList[i]);
        }



        //Check must list:

        for (int i = 0; i < mustTasks.Count; i++)
        {
            if (mustTasks[i].status != TaskStatus_Enum.Done)
            {
                NotComplitedMustTaskList.Add(mustTasks[i]);
            }

        }

        if (NotComplitedMustTaskList.Count > 0)
        {
            return false;
        }
        else
        {
            return true;
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
