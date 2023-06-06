using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractableObject : MonoBehaviour
{
    public GameObject taskBtn;

    public string objectName;

    public Object_Enum objectType;

    public List<Task> relatedTasks;

    public List<Thought_Enum> relatedThoughts;

    private Task curTask;

    private List<Button> taskButtons;

    private Button curBtn;

    private GameObject objectText;

    private GameObject buttonObject;

    void Start()
    {
        SetObjectText();
        taskButtons = new List<Button>();
    }

    void SetObjectText()
    {
        objectText = GameObject.FindWithTag("ObjectText");
        TextMesh objTxtComponent =
            objectText.GetComponentInChildren<TextMesh>();
        objTxtComponent.text = objectName;
        objectText.SetActive(false);
    }

    void OnMouseEnter()
    {
        objectText.SetActive(true);
    }

    void OnMouseExit()
    {
        objectText.SetActive(false);
    }

    void OnMouseDown()
    {
        ShowTasks();
    }

    void ShowTasks()
    {
        foreach (Task relatedTask in relatedTasks)
        {
            CreateTaskButton(relatedTask.taskName);

            PositionTaskButton();
            NameTaskButton(relatedTask.taskName);
        }
        CreateTaskListeners();
    }

    void CreateTaskButton(string name)
    {
        GameObject canvas = GameObject.Find("Canvas");
        GameObject buttonsSpace = GameObject.Find("ButtonsSpace");

        buttonObject = Instantiate(taskBtn, Vector3.zero, Quaternion.identity);
        buttonObject.name = name;
        taskButtons.Add(buttonObject.GetComponent<Button>());
        curBtn = buttonObject.GetComponent<Button>();

        buttonObject.transform.SetParent(buttonsSpace.transform);
    }

    void PositionTaskButton()
    {
        RectTransform btnPosition = buttonObject.GetComponent<RectTransform>();
        btnPosition.anchoredPosition =
            new Vector2(transform.position.x, transform.position.y);
    }

    void NameTaskButton(string name)
    {
        TextMeshProUGUI btnText =
            curBtn.GetComponentInChildren<TextMeshProUGUI>();
        btnText.text = name;
    }

    void CreateTaskListeners()
    {
        foreach (Button button in taskButtons)
        {
            button.onClick.AddListener(() => StartTask(button.name));
        }
    }

    void StartTask(string taskName)
    {
        curTask = relatedTasks.Find(t => t.taskName == taskName);
        curTask.StartTask();
    }

    public void OnApplicationQuit()
    {
        foreach (Task task in relatedTasks)
        {
            task.isDone = false;
        }
    }
}