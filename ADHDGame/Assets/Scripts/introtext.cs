using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class introtext : MonoBehaviour
{
    public static introtext instance;
    public TMP_Text introText;
    public bool firstMessageAppeared = false;

    private void Start()
    {
        instance= this;
        changeIntroText(" Alright, let's get ready for work.");
        StartCoroutine(switchText());
    }

    public void changeIntroText (string text)
    {
        introText.text = text;
    }
     internal IEnumerator switchText()
    {
        yield return new WaitForSeconds(60);
        changeIntroText(" Dude, you're late!");
        
    }

    private void Update()
    {
        if (firstMessageAppeared == false)
        {
            MessageScriptble firstMessage = MessageController.messageControlInstance.SearchMessageOnList(MessageName_Enum.Hi);
            if (firstMessage != null)
            {
                if (firstMessage.messageOnAppStatus == MessageOnAppStatus_Enum.Read)
                {
                    changeIntroText(" Alright, let's get ready for work. by 08:10!");
                    firstMessageAppeared =true;
                }

            }
        }
    }

}
