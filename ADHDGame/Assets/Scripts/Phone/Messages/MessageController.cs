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

    public GameObject messagefeedback;





    public void Start()
    {
        messageControlInstance = this;


    }
    public void SendMessage(MessageName_Enum messageName)
    {

        MessageScriptble message = SearchMessageOnList(messageName);
        if (message.relatedTask != Task_Enum.None)
        {
            bool TaskIsDone = CheckMessagesTask(messageName);
            if (TaskIsDone == false)
            {
                createMessageOnApp(messageName);
                CreatOutOfPhoneMessage(messageName);
            }
        }
        else
        {
            createMessageOnApp(messageName);
            CreatOutOfPhoneMessage(messageName);
        }
    }

    private bool CheckMessagesTask(MessageName_Enum messageName)
    {
        MessageScriptble currentMessage = SearchMessageOnList(messageName);
        Task currentTask = TaskManager.instance.searchTaskOnList(currentMessage.relatedTask);
        if (currentTask.status == TaskStatus_Enum.Done)
        { return (true); }
        else { return false; }
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

            if(messageName == MessageName_Enum.Good_morning)
            {
                StartCoroutine( startMessagefeedback());
            }

        }
    }

    internal IEnumerator startMessagefeedback()
    {
        yield return new WaitForSeconds(2);
        messagefeedback.SetActive(true);
        MessageScriptble message = SearchMessageOnList(MessageName_Enum.Good_morning);
        while (message.messageOnAppStatus != MessageOnAppStatus_Enum.Read)
        {
            yield return new WaitForSeconds(0.5f);
            messagefeedback.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            messagefeedback.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            messagefeedback.SetActive(false);
        }
        messagefeedback.SetActive(false);
    }
    public void createMessageOnApp(MessageName_Enum messageName)
    {
        GameObject messageObject = Instantiate(messagePrefab);
        SoundManager.RegisterAction(SoundManager.SoundAction.message);

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
        if (messageNameToShow == MessageName_Enum.Good_morning)
        {
            SoundManager.RegisterAction(SoundManager.SoundAction.openFirstMessage);
        }
        else if (messageNameToShow == MessageName_Enum.Drink_water)
        {
            SoundManager.RegisterAction(SoundManager.SoundAction.drinkWaterMessage);
        }

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
        {
            return messages[currentMessage];
          
                                                         }
    }


    internal IEnumerator waitToClose()
    {
        yield return new WaitForSeconds(30);
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
       Debug.Log("wait for" + messageName);
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

    internal void clearMessagesFromScene()
    {
        closePhoneMessage.gameObject.SetActive(false);
        openPhoneMessage.gameObject.SetActive(false);
    }
}
