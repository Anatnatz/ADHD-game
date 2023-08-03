using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class Thought_trigger : MonoBehaviour
{
    [SerializeField]
    thought_Transform thought_Transform;

    public static Thought_trigger instance;

    void Awake()
    {
        instance = this;
    }

    public void TriggerThought(Collider2D other)
    {


        bool checkFollowing = true;
        if (other.tag == "phone")
        {
            Thought thought = Thoughts_Manager.ThoughtsInstance.searchForThoughtType(thought_Transform.thoughtType);
            Task_Enum taskType = thought.taskType;
            Task task = TaskManager.instance.searchTaskOnList(taskType);
            Debug.Log("push to taskApp " + task.textInApp);
            if (thought_Transform.IsItATask == true || task.textInApp != null)
            {
                thought_Transform.thoughtTransformStatus = ThoughtStatus.PushToApp;
                thought_Transform.changeStatuse(ThoughtStatus.PushToApp);
                thought_Transform.pushToApp();
                thought_Transform.updateNumOfAppearanceOnApp();
                Thought currentThough = Thoughts_Manager.ThoughtsInstance.searchForThoughtType(thought_Transform.thoughtType);
               
                if (currentThough.loop == true)

                { startThoughtLoop(currentThough.thoughtType ); }
                
                else
                {
                    Destroy(thought_Transform.gameObject);
                    Destroy(this);

                }
                     
                
            }
            else
            {
                StartCoroutine(changeThoughtText());
                Debug.Log("there is nothing to do about it, try to ignor it");
            }
        }

        else if (other.tag == "border")
        {
            InfoManager.instance.SendInfoMessage("Thought ignored");
            thought_Transform.thoughtTransformStatus = ThoughtStatus.Deleted;
            thought_Transform.changeStatuse(ThoughtStatus.Deleted);
            Thought currentThough = Thoughts_Manager.ThoughtsInstance.searchForThoughtType(thought_Transform.thoughtType);
            if (currentThough.loop == true)
           
            { startThoughtLoop(currentThough.thoughtType); }
            
            else
            {
                Destroy(thought_Transform.gameObject);
                Destroy(this);
            }
           
            
        }
        else
        {
            checkFollowing = false;
        }

        if (checkFollowing)
        {
            Thought currentThought = Thoughts_Manager.ThoughtsInstance.searchForThoughtType(thought_Transform.thoughtType);
            currentThought.CheckFollowingAction();
        }



    }

    internal IEnumerator changeThoughtText()
    {
        TMP_Text thoughtTxt = thought_Transform.transform.GetChild(0).GetComponent<TMP_Text>();
        thoughtTxt.SetText("there is nothing to do about it, swipe to the left and try to ignor it ");
        thoughtTxt.color = Color.blue;

        yield return new WaitForSeconds(2);
        thoughtTxt.SetText(thought_Transform.thoughtText);
        thoughtTxt.color = Color.black;
    }

    internal void startThoughtLoop(Thought_Enum thoughtType)
    {
        Debug.Log("starting loop");
        Thoughts_Manager.ThoughtsInstance.startWaitGapThought(thoughtType);
        Destroy(thought_Transform.gameObject);
        Destroy(this);

    }
}
