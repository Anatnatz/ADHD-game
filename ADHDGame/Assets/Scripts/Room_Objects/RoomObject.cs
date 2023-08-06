using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class RoomObject : MonoBehaviour
{
    public GameObject taskBtn;
    public GameObject taskInfo;

    public string objectName;

    public Object_Enum objectType;
    public Animator animator;

    public List<Task> relatedTasks;

    public List<Thought_Enum> relatedThoughts;

    private Task curTask;

    private List<Button> taskButtons;

    private List<TextMeshProUGUI> taskInfoTexts;

    private Button curBtn;

    private GameObject objectText;

    private GameObject buttonObject;
    private GameObject taskInfoObject;

    private int currentThought = 0;

    public Task_Enum previousTask;
    public Thought_Enum previousThought;
    public float zoomNeeded;
    public GameObject objectSprite;

    public TMP_Text textInfo;
    public bool textInfoTest;
    private static bool allowTakeKeys;
    public bool showingTheseTasks;



    void Awake()
    {
        for (int i = 0; i < relatedTasks.Count; i++)
        {
            relatedTasks[i].zoomLocation = this.transform.position;
            relatedTasks[i].zoomNeeded = zoomNeeded;
        }
        TextOnHover textOnHover = transform.GetComponent<TextOnHover>();
        textOnHover.hoveredText = objectName;
        // SetObjectText();
        taskButtons = new List<Button>();
        taskInfoTexts = new List<TextMeshProUGUI>();

    }

    void Start()
    {
        RecoverAnimations();
    }

    void RecoverAnimations()
    {
        foreach (Task task in relatedTasks)
        {
            if ((task.taskType == Task_Enum.Wear_shoes || task.taskType == Task_Enum.Take_the_wallet) && task.status == TaskStatus_Enum.Done)
            {
                animator.SetBool("take", true);
            }
            if (task.taskType == Task_Enum.Dry_hands && TaskManager.instance.searchTaskOnList(Task_Enum.Do_laundry).status == TaskStatus_Enum.Done)
            {
                animator.SetBool("take", true);
            }
            if (task.taskType == Task_Enum.Take_a_keys && TaskManager.instance.searchTaskOnList(Task_Enum.Wear_shoes).status == TaskStatus_Enum.Done)
            {
                animator.SetBool("take", true);
            }
            if (task.taskType == Task_Enum.Turn_on_Coffemaker)
            {
                if (task.status == TaskStatus_Enum.Waiting)
                {
                    animator.SetBool("making", true);
                }
                else if (task.status == TaskStatus_Enum.Done && TaskManager.instance.searchTaskOnList(Task_Enum.Make_coffee).status != TaskStatus_Enum.Done)
                {
                    animator.SetBool("ready", true);
                }
            }
            if (task.taskType == Task_Enum.Make_coffee && task.status == TaskStatus_Enum.Done)
            {
                animator.SetBool("take", true);
            }
        }
    }

    void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && !Game_Manager.gameInstance.doingTask)
        {
            TaskManager.clickedOn = this;
            ShowTasks();
            for (int i = 0; i < relatedTasks.Count; i++)
            {
                if (relatedTasks[i].taskType == Task_Enum.workOnGame)
                {
                    Thoughts_Manager.ThoughtsInstance.triggerThought(Thought_Enum.GetShitDone);
                }

                if (relatedTasks[i].taskType == Task_Enum.Turn_on_Coffemaker && relatedTasks[i].status == TaskStatus_Enum.Waiting)
                {
                    Thoughts_Manager.ThoughtsInstance.triggerThought(Thought_Enum.Coffe_Not_Read);
                }
            }

        }
    }

    void ShowTasks()
    {
        SoundManager.RegisterAction(SoundManager.SoundAction.click);
        bool isClickable = true;

        if (taskBtn.name == "UnclickableTaskButton")
        {
            isClickable = false;
        }

        DestoryPreviousTasks();

        foreach (Task relatedTask in relatedTasks)
        {
            if ((relatedTask.waitingOnTask == null || relatedTask.waitingOnTask.status == TaskStatus_Enum.Done) && relatedTask.status == TaskStatus_Enum.none)
            {
                CreateTaskButton(relatedTask.taskName, relatedTask);
                NameTaskButton(relatedTask.taskName);
            }
        }

        if (isClickable)
        {
            animator.SetBool("isClicked", true);
            CreateTaskListeners();
        }
        // TaskButtonController.instance.ButtonsChanged();
    }

    void DestoryPreviousTasks()
    {
        GameObject buttonsSpace = GameObject.Find("ButtonsSpace");
        for (int i = 0; i < buttonsSpace.transform.childCount; i++)
        {
            Destroy(buttonsSpace.transform.GetChild(i).gameObject);
        }

    }

    void CreateTaskButton(string name, Task relatedTask)
    {
        if (previousTask != Task_Enum.None)
        {
            Task preTask = TaskManager.instance.searchTaskOnList(previousTask);
            if (preTask.status == TaskStatus_Enum.Done)
            {
                StartCreatingButton(name, relatedTask);

            }
        }
        if (previousTask == Task_Enum.None)

        {
            if (previousThought != Thought_Enum.None)
            {

                Thought currentThought = Thoughts_Manager.ThoughtsInstance.searchForThoughtType(previousThought);
                if (currentThought.thoughtStatus == ThoughtStatus.Appeared)
                {
                    StartCreatingButton(name, relatedTask);
                }
            }
            else
            {
                StartCreatingButton(name, relatedTask);
            }

        }
    }

    private void StartCreatingButton(string name, Task relatedTask)
    {
        GameObject canvas = GameObject.Find("Canvas");
        GameObject buttonsSpace = GameObject.Find("ButtonsSpace");

        buttonObject = Instantiate(taskBtn, Vector3.zero, Quaternion.identity);
        taskInfoObject = Instantiate(taskInfo, Vector3.zero, Quaternion.identity);

        buttonObject.name = name;
        curBtn = buttonObject.GetComponent<Button>();
        taskButtons.Add(curBtn);
        TextMeshProUGUI taskInfoText = taskInfoObject.GetComponentsInChildren<TextMeshProUGUI>()[0];
        taskInfoText.text = "takes " + relatedTask.duration.ToString() + " min";

        // if (buttonsSpace == null)
        // buttonObject.transform.SetParent(canvas.transform);
        Vector3 mousePos = Input.mousePosition;
        buttonsSpace.transform.position = mousePos;
        VerticalLayoutGroup vlgButtons = buttonsSpace.GetComponent<VerticalLayoutGroup>();
        if (mousePos.x + 400f > 1920f)
        {
            vlgButtons.padding.left = -400;
        }
        else { vlgButtons.padding.left = 400; }
        buttonObject.transform.SetParent(buttonsSpace.transform);
        taskInfoObject.transform.SetParent(buttonsSpace.transform);
        // buttonObject.transform.position = mousePos + offSetVector;
        // else
        //     buttonObject.transform.SetParent(buttonsSpace.transform);

        CheckForKeys(relatedTask);

    }

    void NameTaskButton(string name)
    {
        Debug.Log(name);
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
        SoundManager.RegisterAction(SoundManager.SoundAction.click);
        string taskName = taskBtn.name;
        Destroy(taskBtn.gameObject);
        Destroy(taskInfoObject.gameObject);
        curTask = relatedTasks.Find(t => t.taskName == taskName);
        CheckForLaundry(curTask);
        StartCoroutine(CameraZoom.instance.ZoomIn(objectSprite.transform.position, zoomNeeded));
        curTask.StartTask(animator);
        animator.SetBool("isClicked", false);
    }

    void CheckForKeys(Task curTask)
    {
        switch (curTask.name)
        {
            case "wearShoes":
                allowTakeKeys = true;
                Debug.Log(allowTakeKeys);
                break;
            case "Take a keys":
                Debug.Log(allowTakeKeys);
                if (allowTakeKeys)
                {
                    animator.SetBool("take", true);
                    allowTakeKeys = false;
                }
                else
                {
                    animator.SetBool("take", false);
                }
                break;
        }
    }

    void CheckForLaundry(Task curTask)
    {
        Debug.Log(curTask.name);
        switch (curTask.name)
        {
            case "StartLaundry":
                Animator towelAnimator = curTask.waitingOnTask.animator;
                towelAnimator.SetBool("take", true);
                break;
        }
    }


    public void OnApplicationQuit()
    {
        foreach (Task task in relatedTasks)
        {
            task.status = TaskStatus_Enum.none;
        }
    }


    public void changeTextInfo(string text)
    {
        if (textInfo != null)
        {
            textInfo.gameObject.SetActive(true);
            textInfo.text = text;
        }

    }

    public void objectTrigger()
    {

        if (relatedThoughts != null && relatedThoughts.Count > 0)
        {
            if (previousTask != Task_Enum.None)
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

