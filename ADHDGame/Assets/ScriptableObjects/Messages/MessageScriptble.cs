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

}
