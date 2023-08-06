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
    public string runOutOfTimeText = " Time's up...";
    public string wentOutText = "You've left the house on time!";
    public bool runOutTime = false;

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
        clearScene();
        TaskManager.instance.creatComplitedTaskList();
        Debug.Log(TaskManager.instance.complitedTasks.Count);
        Debug.Log(" Score:" + scoreController.instance.currentScore);

        bool runOutOfTime = PhoneController.instance.runOutOfTime;
        if (runOutOfTime == true) 
        {
            intro.text = runOutOfTimeText;
        }

        else
        {
            intro.text = wentOutText;

        }
        
        score.text = " Score:" + scoreController.instance.currentScore;
        
        task.text = "Tasks:" + TaskManager.instance.complitedTasks.Count.ToString() + "/" + TaskManager.instance.tasksList.Count;
        

        if (TaskManager.instance.complitedTasks.Count >= minTasks)

        {
            isItEnough.text = winText;
        }
        else
        {
            isItEnough.text = loseText;
        }

        

        checkedResults();

        runOutTime= false;

    }

    private void clearScene()
    {
        Thoughts_Manager.ThoughtsInstance.clearThoughtsFromScene();
        MessageController.messageControlInstance.clearMessagesFromScene();

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
                byTheWay.text = ByTheWayText;
                results.text = unDoneMustLIst[i].results;
            }
            else { closeLastTextLines(); };

        }
        else
        {
            
            for (int i = 0; i < TaskManager.instance.tasksList.Count; i++)
            {
                if (TaskManager.instance.tasksList[i].status == TaskStatus_Enum.none)
                {
                    if (TaskManager.instance.tasksList[i].results != null)
                    {
                        byTheWay.text = ByTheWayText;
                        results.text = TaskManager.instance.tasksList[i].results;
                        return;
                    }

                    else
                    { closeLastTextLines(); }
                }
            }
        }

        
    }

    
    internal void closeLastTextLines()
    {
        byTheWay.gameObject.SetActive(false);
        results.gameObject.SetActive(false);
    }
}
