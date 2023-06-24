using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskButtonController : MonoBehaviour
{
    public static TaskButtonController instance;

    public GameObject taskInfoPanel;

    List<Button> taskButtons;

    static int selection;

    GameObject durationObject;

    TMP_Text durationText;

    GameObject waitingObject;

    TMP_Text waitingText;

    void Awake()
    {
        instance = this;

        taskButtons = new List<Button>();

        durationObject = GameObject.Find("DurationMinutes");
        durationText = durationObject.GetComponent<TMP_Text>();

        waitingObject = GameObject.Find("WaitingMinutes");
        waitingText = waitingObject.GetComponent<TMP_Text>();
    }

    public void ButtonsChanged()
    {
        if (transform.childCount > 0)
        {
            taskButtons =
                new List<Button>(transform.GetComponentsInChildren<Button>());
            Debug.Log(taskButtons.Count);
            selection = 0;
            ReactivateButtons();
            taskInfoPanel.SetActive(true);
        }
        else
        {
            taskButtons = null;
            Debug.Log("huu");

            taskInfoPanel.SetActive(false);
        }
    }

    void Update()
    {
        if (taskButtons != null && taskButtons.Count > 0)
        {
            ShowParent(durationObject.transform.parent.gameObject);
            if (Input.GetKeyDown("right"))
            {
                selection = (selection + 1) % taskButtons.Count;
                ReactivateButtons();
            }

            if (Input.GetKeyDown("left"))
            {
                selection--;
                if (selection < 0)
                {
                    selection = taskButtons.Count - 1;
                }
                ReactivateButtons();
            }

            if (Input.GetKeyDown("space"))
            {
                taskInfoPanel.SetActive(false);
                TaskManager.instance.StartTask(taskButtons[selection]);
                DestroyButtons();
                Debug.Log("huu");
            }
        }
        else
        {
            HideParent(durationObject.transform.parent.gameObject);
        }
    }

    void ReactivateButtons()
    {
        foreach (Button button in taskButtons)
        {
            button.GetComponentsInChildren<TMP_Text>()[0].SetText("");
        }

        taskButtons[selection]
            .GetComponentsInChildren<TMP_Text>()[0]
            .SetText(">");

        Task selectedTask =
            TaskManager
                .instance
                .tasksList
                .Find(t => t.taskName == taskButtons[selection].name);

        SetPanelInfo(selectedTask.duration, selectedTask.waitingTime);
    }

    public void SetPanelInfo(float duration, float waitingTime)
    {
        durationText.SetText(duration.ToString());

        waitingText.SetText(waitingTime.ToString());

        if (waitingTime == 0)
        {
            HideParent (waitingObject);
        }
        else
        {
            ShowParent (waitingObject);
        }
    }

    void HideParent(GameObject child)
    {
        child.transform.parent.gameObject.SetActive(false);
    }

    void ShowParent(GameObject child)
    {
        child.transform.parent.gameObject.SetActive(true);
    }

    void DestroyButtons()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
