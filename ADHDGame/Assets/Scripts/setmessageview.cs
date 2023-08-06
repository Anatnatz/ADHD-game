using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class setmessageview : MonoBehaviour
{
    public TMP_Text textSender;
    public TMP_Text textMessage;

     public void SetMessageText(string sender,string message)
    {

        textSender.text = sender;
        textMessage.text = message;

    }
}
