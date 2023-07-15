using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class win : MonoBehaviour

{
    public bool winCondition = false;
    public void checkWinCondition()
    {
       winCondition = TaskManager.instance.checkMustTasksList();
       
       if (winCondition) 
       {
           
            InfoManager.instance.SendInfoMessage("YOU WIN!");
       }

       else
        {
            InfoManager.instance.SendInfoMessage("NET YET...");
            Thoughts_Manager.ThoughtsInstance.createThought(Thought_Enum.Cant_leave_the_house);
        }
    }
}
