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

    public int currentTaskNumOnList;

    GameObject buttonsSpace;

    RectTransform rectTransform;

    void Start()
    {
        instance = this;
        roomObject = GetComponent<RoomObject>();
        buttonsSpace = GameObject.Find("ButtonsSpace");
        rectTransform = buttonsSpace.GetComponent<RectTransform>();
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
        searchTaskOnList (taskType);
        tasksList[currentTaskNumOnList].status = status;
    }

    public void StartTask(Button taskBtn)
    {
        string taskName = taskBtn.name;
        Task curTask = tasksList.Find(t => t.taskName == taskName);
        curTask.StartTask();
        Destroy(taskBtn.gameObject);
    }
}
