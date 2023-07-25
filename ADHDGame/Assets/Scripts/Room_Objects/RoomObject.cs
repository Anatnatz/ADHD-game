using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomObject : MonoBehaviour
{
    public GameObject taskBtn;

    public string objectName;

    public Object_Enum objectType;
    public Animator animator;

    public List<Task> relatedTasks;

    public List<Thought_Enum> relatedThoughts;

    private Task curTask;

    private List<Button> taskButtons;

    private Button curBtn;

    private GameObject objectText;

    private GameObject buttonObject;

    private int currentThought = 0;

    public Task_Enum previousTask;
    public Thought_Enum previousThought;

    void Awake()
    {
        TextOnHover textOnHover = transform.GetComponent<TextOnHover>();
        textOnHover.hoveredText = objectName;
        // SetObjectText();
        taskButtons = new List<Button>();
    }

    // void SetObjectText()
    // {
    //     TextMesh objTxtComponent = transform.GetComponentInChildren<TextMesh>();
    //     objectText = objTxtComponent.gameObject;
    //     Debug.Log(objTxtComponent);
    //     objTxtComponent.text = objectName;
    //     objectText.SetActive(false);
    // }

    // void OnMouseEnter()
    // {
    //     objectText.SetActive(true);
    // }

    // void OnMouseExit()
    // {
    //     objectText.SetActive(false);
    // }

    void OnMouseDown()
    {
        ShowTasks();
    }

    void ShowTasks()
    {
        foreach (Task relatedTask in relatedTasks)
        {
            CreateTaskButton(relatedTask.taskName);
            NameTaskButton(relatedTask.taskName);
        }
        CreateTaskListeners();
        // TaskButtonController.instance.ButtonsChanged();
    }

    void CreateTaskButton(string name)
    {
        if (previousTask != Task_Enum.None)
        {
            Task preTask = TaskManager.instance.searchTaskOnList(previousTask);
            if (preTask.status == TaskStatus_Enum.Done)
            {
                StartCreatingButton(name);
                
            }
        }
        if (previousTask == Task_Enum.None)
        
        {
           if(previousThought != Thought_Enum.None) 
            {
                Thoughts_Manager.ThoughtsInstance.searchForThoughtType(previousThought);
                if (Thoughts_Manager.ThoughtsInstance.thoughtsList_[Thoughts_Manager.ThoughtsInstance.currentThoughtNum].thoughtStatus == ThoughtStatus.Appeared )
                {
                    StartCreatingButton(name);
                }
            }
           else
            {
                StartCreatingButton(name);
            }
           
        }
    }

    private void StartCreatingButton(string name)
    {
        GameObject canvas = GameObject.Find("Canvas");
        GameObject buttonsSpace = GameObject.Find("ButtonsSpace");

        buttonObject = Instantiate(taskBtn, Vector3.zero, Quaternion.identity);
        buttonObject.name = name;
        curBtn = buttonObject.GetComponent<Button>();
        taskButtons.Add(curBtn);

        // if (buttonsSpace == null)
        // buttonObject.transform.SetParent(canvas.transform);
        Vector3 mousePos = Input.mousePosition;
        buttonsSpace.transform.position = mousePos;
        buttonObject.transform.SetParent(buttonsSpace.transform);
        // buttonObject.transform.position = mousePos + offSetVector;
        // else
        //     buttonObject.transform.SetParent(buttonsSpace.transform);
    }

    void NameTaskButton(string name)
    {
        TextMeshProUGUI btnText =
            curBtn.GetComponentsInChildren<TextMeshProUGUI>()[1];
        btnText.text = name;
    }

    void CreateTaskListeners()
    {
        foreach (Button button in taskButtons)
        {
            button.onClick.AddListener(() => StartTask(button));
        }
    }

    public void StartTask(Button taskBtn)
    {
        string taskName = taskBtn.name;
        Destroy(taskBtn.gameObject);
        curTask = relatedTasks.Find(t => t.taskName == taskName);
        curTask.StartTask(animator);
    }

    public void OnApplicationQuit()
    {
        foreach (Task task in relatedTasks)
        {
            task.status = TaskStatus_Enum.none;
        }
    }

    public void objectTrigger()
    {
        int numOfTasksDone = 0;
        if (relatedThoughts != null && relatedThoughts.Count > 0)
        {   if(previousTask != Task_Enum.None)
            {
                Task preTask = TaskManager.instance.searchTaskOnList(previousTask);
                if (preTask.status == TaskStatus_Enum.Done)
                {
                    Thoughts_Manager.ThoughtsInstance.triggerThought(relatedThoughts[currentThought]);
                }
            }

            
                else
                {
                    Thoughts_Manager.ThoughtsInstance.triggerThought(relatedThoughts[currentThought]);
                }

            }
            
            
            // if (relatedTasks.Count > 0)
            // {


            //     for (int i = 0; i < relatedTasks.Count; i++)
            //     {
            //         if (relatedTasks[i].status != TaskStatus_Enum.Done)
            //         {
            //             numOfTasksDone++;
            //         }
            //     }

            //     if (numOfTasksDone != relatedTasks.Count)
            //     {
            //         Thoughts_Manager.ThoughtsInstance.triggerThought(relatedThoughts[currentThought]);
            //     }
            // }
            // else
            // {
            //     Thoughts_Manager.ThoughtsInstance.triggerThought(relatedThoughts[currentThought]);
            // }


        }
    }

