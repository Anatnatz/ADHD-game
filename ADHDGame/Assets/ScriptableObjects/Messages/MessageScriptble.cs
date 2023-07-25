using System.Collections;
using System.Collections.Generic;
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


