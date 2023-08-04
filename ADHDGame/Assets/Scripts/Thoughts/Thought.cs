using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]

public class Thought : ScriptableObject
{


    [Header("Thought")]

    //public string thoughtText = " Thought's Text";

    public string thoughtText;

    public List<string> thoughtTexts;

    public Thought_Enum thoughtType;

    public Vector2 thoughtPosition;

    public ThoughtStatus thoughtStatus = ThoughtStatus.None;

    public bool loop;
    public int loopInterval;

    public bool isOnLoop = false;
    public bool currentThoughtText;

    public int numOfAppearance = 0;
    public int waitingGap = 0;
    public Thought previousThought;
    public string previousAction;
    public bool showOnlyOnc = false;
    public bool isItATask;


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






    public void start()
    {
        numOfAppearance = 0;
        UpdateFollowing();

    }

    private void UpdateFollowing()
    {

        for (int i = 0; i < followingThoughtsWhenAppeared.Count; i++)
        {
            UpdateFollowingThought(followingThoughtsWhenAppeared[i]);
        }

        for (int i = 0; i < followingThoughtsWhenDeleted.Count; i++)
        {
            UpdateFollowingThought(followingThoughtsWhenDeleted[i]);

        }

        for (int i = 0; i < followingThoughtsWhenPushToApp.Count; i++)
        {
            UpdateFollowingThought(followingThoughtsWhenPushToApp[i]);
        }

        for (int i = 0; i < followingMessagesWhenAppeared.Count; i++)
        {
            UpdateFollowingMessage(followingMessagesWhenAppeared[i]);

        }

        for (int i = 0; i < followingMessagesWhenDeleted.Count; i++)
        {
            UpdateFollowingMessage(followingMessagesWhenDeleted[i]);
        }

        for (int i = 0; i < followingMessagesWhenPushToApp.Count; i++)
        {
            UpdateFollowingMessage(followingMessagesWhenPushToApp[i]);
        }

    }

    private void UpdateFollowingMessage(MessageName_Enum messageName_Enum)
    {
        MessageScriptble message = MessageController.messageControlInstance.SearchMessageOnList(messageName_Enum);
        message.previousAction = this.thoughtText;
    }

    private void UpdateFollowingThought(Thought_Enum thought_)
    {
        Thought currentThought = Thoughts_Manager.ThoughtsInstance.searchForThoughtType(thought_);

        currentThought.previousAction = this.thoughtText;
    }

    public void CheckFollowingAction()
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
        if (thoughtStatus == ThoughtStatus.Appeared)
        {
            for (int i = 0; i < followingThoughtsWhenAppeared.Count; i++)
            {

                TriggerThought(followingThoughtsWhenAppeared[i]);

            }
        }

        if (thoughtStatus == ThoughtStatus.Deleted)
        {
            for (int i = 0; i < followingThoughtsWhenDeleted.Count; i++)
            {

                TriggerThought(followingThoughtsWhenDeleted[i]);

            }
        }

        if (thoughtStatus == ThoughtStatus.PushToApp)
        {
            for (int i = 0; i < followingThoughtsWhenPushToApp.Count; i++)
            {

                TriggerThought(followingThoughtsWhenPushToApp[i]);

            }
        }


    }



    private void checkFolowingMessages(ThoughtStatus thoughtStatus)
    {
        if (thoughtStatus == ThoughtStatus.Appeared)
        {
            for (int i = 0; i < followingMessagesWhenAppeared.Count; i++)
            {

                TriggerMessage(followingMessagesWhenAppeared[i]);

            }
        }

        if (thoughtStatus == ThoughtStatus.Deleted)
        {
            for (int i = 0; i < followingMessagesWhenDeleted.Count; i++)
            {

                TriggerMessage(followingMessagesWhenDeleted[i]);

            }
        }

        if (thoughtStatus == ThoughtStatus.PushToApp)
        {
            for (int i = 0; i < followingMessagesWhenPushToApp.Count; i++)
            {

                TriggerMessage(followingMessagesWhenPushToApp[i]);

            }
        }


    }

    private void TriggerMessage(MessageName_Enum messageName)
    {
        MessageController.messageControlInstance.startWaitGapMessage(messageName);


    }

    private void TriggerThought(Thought_Enum thoughtType)
    {
        Thoughts_Manager.ThoughtsInstance.startWaitGapThought(thoughtType);
    }

    public void Update()
    {



    }
}

