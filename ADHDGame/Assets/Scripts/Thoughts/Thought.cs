using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]

public class Thought : ScriptableObject
{
    

    [Header("Game_Object")]

    public Object_Enum objectType;



    [Header("Thought")]

    public string thoughtText = " Thought's Text";

    public Thought_Enum thoughtType;

    public Vector2 thoughtPosition;

    public ThoughtStatus thoughtStatus = ThoughtStatus.None;

    public bool RelevantThought;

    public int nagge;

    

    [Header("Tasks")]

    public Task_Enum taskType;

    
    [Header("Following When Appeared")]

    [SerializeField]
    List<MessageName_Enum> followingMessagesWhenAppeared;

    [SerializeField]
    List<Thought_Enum> followingThoughtsWhenAppeared;

    [Header("Following When Deleted")]

    [SerializeField]
    List<MessageName_Enum> followingMessagesWhenDeleted;

    [SerializeField]
    List<Thought_Enum> followingThoughtsWhenDeleted;

    [Header("Following When PushToApp")]

    [SerializeField]
    List<MessageName_Enum> followingMessagesWhenPushToApp;

    [SerializeField]
    List<Thought_Enum> followingThoughtsWhenPushToApp;

    

    public void update()
    {
        switch (thoughtStatus) 
        {
        
        case ThoughtStatus.None:
                { break; }

        case ThoughtStatus.Appeared:
                {
                    checkFolowingMessages(ThoughtStatus.Appeared);
                    checkFollowingThoughts(ThoughtStatus.Appeared);
                    break;
                }
        case ThoughtStatus.Deleted:
                {
                    checkFolowingMessages(ThoughtStatus.Deleted);
                    checkFollowingThoughts(ThoughtStatus.Deleted);
                    break;
                }

        case ThoughtStatus.PushToApp:
                {
                    checkFolowingMessages(ThoughtStatus.PushToApp);
                    checkFollowingThoughts(ThoughtStatus.PushToApp);
                    break;
                }


        }  
    }

    private void checkFollowingThoughts(ThoughtStatus thoughtStatus)
    {
        if (thoughtStatus== ThoughtStatus.Appeared)
        {
            for (int i = 0; i < followingThoughtsWhenAppeared.Count; i++)
            {
                if (followingThoughtsWhenAppeared[i] != null)
                {
                    TriggerThought(followingThoughtsWhenAppeared[i]);
                }
            }
        }

        if (thoughtStatus == ThoughtStatus.Deleted)
        {
            for (int i = 0; i < followingThoughtsWhenDeleted.Count; i++)
            {
                if (followingThoughtsWhenDeleted[i] != null)
                {
                    TriggerThought(followingThoughtsWhenDeleted[i]);
                }
            }
        }

        if (thoughtStatus == ThoughtStatus.PushToApp)
        {
            for (int i = 0; i < followingThoughtsWhenPushToApp.Count; i++)
            {
                if (followingThoughtsWhenPushToApp[i] != null)
                {
                    TriggerThought(followingThoughtsWhenPushToApp[i]);
                }
            }
        }


    }

    

    private void checkFolowingMessages(ThoughtStatus thoughtStatus)
    {
        if (thoughtStatus == ThoughtStatus.Appeared)
        {
            for (int i = 0; i < followingMessagesWhenAppeared.Count; i++)
            {
                if (followingMessagesWhenAppeared[i] != null)
                {
                    TriggerMessage(followingMessagesWhenDeleted[i]);
                }
            }
        }

        if (thoughtStatus == ThoughtStatus.Deleted)
        {
            for (int i = 0; i < followingMessagesWhenDeleted.Count; i++)
            {
                if (followingMessagesWhenDeleted[i] != null)
                {
                    TriggerMessage(followingMessagesWhenDeleted[i]);
                }
            }
        }

        if (thoughtStatus == ThoughtStatus.PushToApp)
        {
            for (int i = 0; i < followingMessagesWhenPushToApp.Count; i++)
            {
                if (followingMessagesWhenPushToApp[i] != null)
                {
                    TriggerMessage(followingMessagesWhenPushToApp[i]);
                }
            }
        }


    }

    private void TriggerMessage(MessageName_Enum messageName)
    {
        MessageController.messageControlInstance.SendMessage(messageName);
    }

    private void TriggerThought(Thought_Enum thoughtType) 
    {
        Thoughts_Manager.ThoughtsInstance.createThought(thoughtType);
    }
}

