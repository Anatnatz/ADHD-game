using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerInfoController : MonoBehaviour
{
    public TMP_Text textAbovePhone;
    public TMP_Text textInfo;
    public static PlayerInfoController instance;
    public bool testPhoneInfo;
    public bool testThoutghtInfo;
    public bool testObjectInfo;
   
   

    public void changeTextInfo(string text, Vector2 location)
    {
        
        Debug.Log(text);
            textInfo.gameObject.SetActive(true);
            textInfo.transform.position = location;
            textInfo.text = text;
        

    }

    public void changePlayerInfoAboveThought(string playerMessage, Thought_Enum thought) 
    {
        testThoutghtInfo = false;
        Thoughts_Manager.ThoughtsInstance.searchForThoughtType(thought);

        thought_Transform currentTransform = Thoughts_Manager.ThoughtsInstance.searchForTransformByThoughtType(thought);
        if(currentTransform.thoughtTransformStatus == ThoughtStatus.Appeared)
        {
            textInfo.gameObject.SetActive(true);

            float y = currentTransform.transform.position.y;
            y = y + 200;
            textInfo.transform.position = new Vector3(currentTransform.transform.position.x, y, textInfo.transform.position.z);
            textInfo.text = playerMessage;
            Debug.Log (textInfo.transform.position);
            Debug.Log (currentTransform.transform.position);

            StartCoroutine(waitToClosePlayerInfo(textInfo));
        }
        

    }

    public void changePlayerInfoAbovePone(string playerMessage)
    {
        testPhoneInfo = false;
        textAbovePhone.gameObject.SetActive(true);
        textAbovePhone.text = playerMessage;
        StartCoroutine(waitToClosePlayerInfo(textAbovePhone));
    }

    public void changePlayerInfoAboveObject(string playerMessage, Task_Enum task)
    {
       testObjectInfo= false;
        Task cTask =  TaskManager.instance.searchTaskOnList(task);
        Debug.Log(cTask.zoomLocation);
        textInfo.gameObject.SetActive(true);
        textInfo.text = playerMessage;
        textInfo.transform.position = new Vector2 (cTask.zoomLocation.x, cTask.zoomLocation.y+1);
    }

    internal IEnumerator waitToClosePlayerInfo(TMP_Text text)
    {
        yield return new WaitForSeconds(3);
        text.gameObject.SetActive(false);
    }

    public void Start()
    {
        instance = this;
    }

    public void Update()
    {
        if (testPhoneInfo)
        {
            
            changePlayerInfoAbovePone("if you leave your phone on, your ADHD will haunt you. Close TikTok!");
        }
        if( testThoutghtInfo)
        {
            changePlayerInfoAboveThought("Swipe to the left of the screen to ignor this thougt", Thought_Enum.My_throat_is_dry);

        }

        if(testObjectInfo)
        {
            changePlayerInfoAboveObject("bleble", Task_Enum.Drink_water);
        }
    }
}

