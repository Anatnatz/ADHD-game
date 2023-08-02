using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu]

public class MessageScriptble : ScriptableObject
{
    public MessageName_Enum messageName;

    public string textSender;

    public string textMessage;

    public string fullText;

    public MessageType_Enum messageType;

    public MessageOnAppStatus_Enum messageOnAppStatus;

    public int waitingGap = 0;

    public string previousAction;

    public Task_Enum relatedTask;

    [Header("Following")]

    [SerializeField]
    List<MessageName_Enum> followingMessages;

    [SerializeField]
    List<Thought_Enum> followingThoughts;

    [Header("Following When Read")]

    [SerializeField]
    List<MessageName_Enum> followingMessagesWhenRead;

    [SerializeField]
    List<Thought_Enum> followingThoughtsWhenRead;

    [Header("Following When UnRead")]

    [SerializeField]
    List<MessageName_Enum> followingMessagesWhenUnRead;

    [SerializeField]
    List<Thought_Enum> followingThoughtsWhenUnRead;


    public void Start()
    {
        UpdateFollowing();
    }

    private void UpdateFollowing()
    {
        for (int i = 0; i < followingMessagesWhenRead.Count; i++)
        {
            UpdatefollowingMessage(followingMessagesWhenRead[i]);
        }

        for (int i = 0; i < followingMessagesWhenUnRead.Count; i++)
        {
            UpdatefollowingMessage(followingMessagesWhenUnRead[i]);
        }

        for (int i = 0; i < followingThoughtsWhenRead.Count; i++)
        {
            UpdateFollowingThought(followingThoughtsWhenRead[i]);
        }

        for (int i = 0; i < followingThoughtsWhenUnRead.Count; i++)
        {
            UpdateFollowingThought(followingThoughtsWhenUnRead[i]);
        }
    }

    private void UpdateFollowingThought(Thought_Enum thought_Enum)
    {
       Thought currentThought =  Thoughts_Manager.ThoughtsInstance.searchForThoughtType(thought_Enum);
        currentThought.previousAction = this.textMessage;
    }

    private void UpdatefollowingMessage(MessageName_Enum messageName_Enum)
    {
        MessageScriptble message = MessageController.messageControlInstance.SearchMessageOnList(messageName_Enum);
        message.previousAction = this.textMessage;
    }

    public void CheckFollowingAction()
    {
        switch (messageOnAppStatus)
        {

            case MessageOnAppStatus_Enum.Unread:
                { break; }

            case MessageOnAppStatus_Enum.Read:
                {
                    checkFolowingMessages();
                    checkFollowingThoughts();
                    break;
                }
        }
    }

    private void checkFollowingThoughts()
    {

        InfoManager.instance.SendInfoMessage(followingThoughtsWhenRead.Count.ToString());
        for (int i = 0; i < followingThoughtsWhenRead.Count; i++)
        {
            TriggerThought(followingThoughtsWhenRead[i]);
        }
    }

    private void checkFolowingMessages()
    {

        for (int i = 0; i < followingMessagesWhenRead.Count; i++)
        {

            TriggerMessage(followingMessagesWhenRead[i]);
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
}


