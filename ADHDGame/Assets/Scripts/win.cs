using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class win : MonoBehaviour

{
    public static win instance;
    public bool winCondition = false;
    public List<Task> unDoneMustLIst;

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
       if(unDoneMustLIst.Count >0)
        
       {
            InfoManager.instance.SendInfoMessage("YOU WIN!");
            //camerazoom
            //setText
       }

       else
        {
            InfoManager.instance.SendInfoMessage("NET YET...");
            Thoughts_Manager.ThoughtsInstance.createThought(Thought_Enum.Cant_leave_the_house);
        }
    }
}
