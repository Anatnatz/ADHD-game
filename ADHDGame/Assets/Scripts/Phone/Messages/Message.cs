using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Message : MonoBehaviour
{
    public TMP_Text textSender;
    public TMP_Text textMessage;
    public MessageType_Enum messageType;
    public MessageStatus_Enum messageStatus;
    public MessageName_Enum currentMessageName;

    public void SetText(string theTextSender, string theTextMessage, string theFullTextMessage)
    {
        if (messageStatus == MessageStatus_Enum.OutsideAppClosePhone)
        {
            textSender.text = theTextSender;
            textMessage.text = theTextMessage;
        }

        if (messageStatus == MessageStatus_Enum.OutsideAppOpenPhone)
        {
            textSender.text = theTextSender;
            textMessage.text = theTextMessage;
        }

        if (messageStatus == MessageStatus_Enum.OnApp)
        {
            textSender.text = theTextSender;
            textMessage.text = theTextMessage;
        }
    }





    public void SendMessage()
    {

        if (messageStatus == MessageStatus_Enum.OutsideAppClosePhone)
        {

            this.gameObject.SetActive(false);
        }

        if (messageStatus == MessageStatus_Enum.OutsideAppOpenPhone)
        {
            this.gameObject.SetActive(false);
        }

        MessageController.messageControlInstance.ViewMessage(currentMessageName);

    }

    public void setThisOff()
    {
        MessageController.messageControlInstance.setOffPhoneMessage();
    }


}

