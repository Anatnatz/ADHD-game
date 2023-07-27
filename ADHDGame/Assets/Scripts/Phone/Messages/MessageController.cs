using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MessageController : MonoBehaviour
{
    [SerializeField]
    PhoneController phoneController;

    [Header("Phone Messages")]

    [SerializeField]
    bool test;

    [SerializeField]
    MessageName_Enum messageNameTest;

    [SerializeField]
    Message openPhoneMessage;

    [SerializeField]
    Message closePhoneMessage;

    [SerializeField]
    GameObject messagePrefab;
    [SerializeField]
    Transform content;

    [SerializeField]
    List<MessageScriptble> messages;

    [SerializeField]
    int currentMessage;

    [SerializeField]
    setmessageview setmessageview;

    [SerializeField]
    float timeToClose;
    [SerializeField]
    float time;

    public Transform readIcon;

    public static MessageController messageControlInstance;





    public void Start()
    {
        messageControlInstance = this;


    }
    public void SendMessage(MessageName_Enum messageName)
    {
        createMessageOnApp(messageName);
        CreatOutOfPhoneMessage(messageName);
    }

    public void CreatOutOfPhoneMessage(MessageName_Enum messageName)
    {
        if (phoneController.phoneStatus == PhoneStatus_Enum.OpenPhone)
        {
            openPhoneMessage.gameObject.SetActive(true);
            setMessageTextAndInfo(messageName, openPhoneMessage);
            if (messageName != MessageName_Enum.Good_morning)
            {
                StartCoroutine(waitToClose());

            }




        }

        if (phoneController.phoneStatus == PhoneStatus_Enum.ClosePhone)
        {
            closePhoneMessage.gameObject.SetActive(true);
            setMessageTextAndInfo(messageName, closePhoneMessage);

            if (messageName != MessageName_Enum.Good_morning)
            {
                StartCoroutine(waitToClose());

            }


        }
    }

    public void createMessageOnApp(MessageName_Enum messageName)
    {
        GameObject messageObject = Instantiate(messagePrefab);

        Message newMessage = messageObject.GetComponent<Message>();
        readIcon = messageObject.transform.Find("ReadIcon");

        newMessage.transform.SetParent(content.transform);
        newMessage.messageStatus = MessageStatus_Enum.OnApp;
        setMessageTextAndInfo(messageName, newMessage);

    }

    private void setMessageTextAndInfo(MessageName_Enum messageName, Message messageToSet)
    {
        MessageScriptble MessageFromList = SearchMessageOnList(messageName);
        if (MessageFromList != null)
        {
            messageToSet.messageType = MessageFromList.messageType;
            messageToSet.currentMessageName = MessageFromList.messageName;
            messageToSet.SetText(MessageFromList.textSender, MessageFromList.textMessage, MessageFromList.fullText);
        }

    }

    public void setOffPhoneMessage()
    {
        if (phoneController.phoneStatus == PhoneStatus_Enum.OpenPhone)
        { openPhoneMessage.gameObject.SetActive(false); }

        if (phoneController.phoneStatus == PhoneStatus_Enum.ClosePhone)
        { closePhoneMessage.gameObject.SetActive(false); }
    }

    public void ViewMessage(MessageName_Enum messageNameToShow)
    {
        MessageScriptble messageToShow = SearchMessageOnList(messageNameToShow);
        messageToShow.messageOnAppStatus = MessageOnAppStatus_Enum.Read;
        setmessageview.SetMessageText(messageToShow.textSender, messageToShow.fullText);
        if (phoneController.phoneStatus == PhoneStatus_Enum.ClosePhone)
        {
            phoneController.TogglePhone();
        }
        phoneController.OpenMessagePanel();
        if (messageToShow.messageOnAppStatus == MessageOnAppStatus_Enum.Read)
        {
            Debug.Log("read");
            readIcon.gameObject.SetActive(true);

        }
        messageToShow.CheckFollowingAction();

    }



    public MessageScriptble SearchMessageOnList(MessageName_Enum messageName)

    {

        for (int i = 0; i < messages.Count; i++)
        {
            if (messages[i].messageName == messageName)
            { currentMessage = i; }
        }
        { return messages[currentMessage]; }
    }


    internal IEnumerator waitToClose()
    {
        yield return new WaitForSeconds(10);
        setOffPhoneMessage();
    }



    void Update()
    {
        if (test)
        {

            test = false;
            SendMessage(messageNameTest);
            //ViewMessage(openPhoneMessage);
            // MessageScriptble testre = SearchMessageOnList(messageNameTest);
            // Debug.Log (testre);
        }
    }

    internal void startWaitGapMessage(MessageName_Enum messageName)
    {
        StartCoroutine(waitGapMessage(messageName));
    }

    internal IEnumerator waitGapMessage(MessageName_Enum messageName)
    {
        MessageScriptble message = MessageController.messageControlInstance.SearchMessageOnList(messageName);
        yield return new WaitForSeconds(message.waitingGap);
        MessageController.messageControlInstance.SendMessage(messageName);
    }

    void OnApplicationQuit()
    {
        foreach (MessageScriptble msgScriptable in messages)
        {
            msgScriptable.messageOnAppStatus = MessageOnAppStatus_Enum.Unread;
        }
    }
}
