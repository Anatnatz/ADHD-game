using System.Collections;
using System.Collections.Generic;
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
        Debug.Log("trigger");

        bool checkFollowing = true;
        if (other.tag == "phone")
        {
            Debug.Log("push to taskApp ");
            if (thought_Transform.IsItATask == true)
            {
                thought_Transform.thoughtTransformStatus = ThoughtStatus.PushToApp;
                thought_Transform.changeStatuse(ThoughtStatus.PushToApp);
                thought_Transform.pushToApp();
                thought_Transform.updateNumOfAppearanceOnApp();
                Destroy(thought_Transform.gameObject);
                Destroy(this);
            }
            else 
            {
                Debug.Log("there is nothing to do about it, try to ignor it");
            }
        }

        else if (other.tag == "border")
        {
            InfoManager.instance.SendInfoMessage("Thought ignored");
            thought_Transform.thoughtTransformStatus = ThoughtStatus.Deleted;
            thought_Transform.changeStatuse(ThoughtStatus.Deleted);
            Destroy(thought_Transform.gameObject);
            Destroy(this);
        }
        else
        {
            checkFollowing = false;
        }

        if (checkFollowing)
        {
            Thoughts_Manager.ThoughtsInstance.searchForThoughtType(thought_Transform.thoughtType);
            Thoughts_Manager.ThoughtsInstance.thoughtsList_[Thoughts_Manager.ThoughtsInstance.currentThoughtNum].CheckFollowingAction();
        }
    }
}
