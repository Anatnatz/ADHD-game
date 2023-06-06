using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// [CreateAssetMenu(fileName = "TaskList", menuName = "ScriptableObjects/Object")]
public class InteractableObject : MonoBehaviour
{
    public GameObject taskBtn;

    public string objectName;

    public Object_Enum objectType;

    public List<Task> relatedTasks;

    public List<Thought_Enum> relatedThoughts;

    private Task curTask;

    private Button button;

    private GameObject objectText;

    void Start()
    {
        objectText = GameObject.FindWithTag("ObjectText");
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
        TextMeshProUGUI btnText =
            button.GetComponentInChildren<TextMeshProUGUI>();
        btnText.text = relatedTasks[0].taskName;
        button.onClick.AddListener (StartTask);
    }

    void ShowTasks()
    {
        GameObject canvas = GameObject.Find("Canvas");

        GameObject buttonobj =
            Instantiate(taskBtn, Vector3.zero, Quaternion.identity);

        buttonobj.transform.SetParent(canvas.transform);
        RectTransform btnPosition = buttonobj.GetComponent<RectTransform>();
        btnPosition.anchoredPosition =
            new Vector2(transform.position.x, transform.position.y);

        button = buttonobj.GetComponent<Button>();
    }

    void StartTask()
    {
        string type = "temp";
        curTask = relatedTasks.Find(t => t.taskName == type);
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
