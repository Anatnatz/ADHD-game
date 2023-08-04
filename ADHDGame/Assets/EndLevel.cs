using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    public static EndLevel instance;
    public bool winCondition = false;
    public List<Task> unDoneMustLIst;
    public TMP_Text intro;
    public TMP_Text score;
    public TMP_Text task;
    public TMP_Text isItEnough;
    public TMP_Text byTheWay;
    public TMP_Text results;
    public int minTasks;
    public string ByTheWayText = "By the way,";
    public string introText = "You've left the house on time!";
    public string winText = "Great! Your ready to go!";
    public string loseText = "That's not enough, try again tomorrow";
    public List<Task> doneTasksList;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        ResetUnDoneMustList();
        YallaEndLevel();
    }


    private void ResetUnDoneMustList()
    {
        for (int i = 0; i < unDoneMustLIst.Count; i++)
        {
            unDoneMustLIst.Remove(unDoneMustLIst[i]);
        }
    }
    public void YallaEndLevel()
    {
        TaskManager.instance.creatComplitedTaskList();
        Debug.Log(TaskManager.instance.complitedTasks.Count);
        Debug.Log(" Score:" + scoreController.instance.currentScore);
        score.text = " Score:" + scoreController.instance.currentScore;
        task.text = "Tasks:" + doneTasksList.Count.ToString() + "/" + TaskManager.instance.tasksList.Count;
        intro.text = "You've left the house on time!";

        if (TaskManager.instance.complitedTasks.Count >= minTasks)

        {
            isItEnough.text = winText;
        }
        else
        {
            isItEnough.text = loseText;
        }

        byTheWay.text = ByTheWayText;

        checkedResults();

    }

    private void checkedResults()
    {
        TaskManager.instance.creatMustTasksList();
        TaskManager.instance.checkMustTasksList();

        if (unDoneMustLIst.Count > 0)
        {
            int i = Random.Range(0, unDoneMustLIst.Count);


            if (unDoneMustLIst[i].results != string.Empty)
            {
                results.text = unDoneMustLIst[i].results;
            }

        }
        else
        {
            for (int i = 0; i < TaskManager.instance.tasksList.Count; i++)
            {
                if (TaskManager.instance.tasksList[i].status == TaskStatus_Enum.none)
                {
                    if (TaskManager.instance.tasksList[i].results != null)
                    {
                        results.text = TaskManager.instance.tasksList[i].results;
                        return;
                    }
                }
            }
        }

        if (results.text == null)
        {
            byTheWay.text = null;
        }
    }


}
