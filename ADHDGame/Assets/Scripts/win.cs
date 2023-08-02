using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class win : MonoBehaviour

{
    public static win instance;
    public bool winCondition = false;
    public List<Task> unDoneMustLIst;

    public Animator doorAnimator;

    public void Start()
    {
        instance = this;
        ResetUnDoneMustList();
    }

    private void ResetUnDoneMustList()
    {
        for (int i = 0; i < unDoneMustLIst.Count; i++)
        {
            unDoneMustLIst.Remove(unDoneMustLIst[i]);
        }
    }

    public void checkWinCondition()
    {
        if (unDoneMustLIst.Count > 0)

        {
            InfoManager.instance.SendInfoMessage("YOU WIN!");
            //change maxX on camerazoom to Mathf.Infinty
            //camerazoom -> call zoom in with door location - no need to move camera
            //setText
            doorAnimator.SetBool("open", true);
        }

        else
        {
            InfoManager.instance.SendInfoMessage("NET YET...");
            Thoughts_Manager.ThoughtsInstance.createThought(Thought_Enum.Cant_leave_the_house);
        }
    }
}
