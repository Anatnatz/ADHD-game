using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class PhoneController : MonoBehaviour
{
    public PhoneStatus_Enum phoneStatus;

    [Header("Game minute duration in seconds")]
    [SerializeField]
    private float gameMinute;

    [Header("Start Time")]
    [SerializeField]
    private int hours;

    [SerializeField]
    private int minutes;

    [SerializeField]
    private TMP_Text timeText;

    [Header("Apps")]
    [SerializeField]
    private GameObject allApps;

    [SerializeField]
    private GameObject todoApp;

    [SerializeField]
    private GameObject messagesApp;

    [SerializeField]
    private GameObject messageOnApp;

    [Header("MiniPhone")]
    [SerializeField]
    private GameObject miniPhone;

    void Start()
    {
        FormatTime();
        StartCoroutine(ChangeTime());
       // BackToAllApps();
    }

    IEnumerator ChangeTime()
    {
        yield return new WaitForSeconds(gameMinute);
        if (minutes < 59)
        {
            minutes++;
        }
        else if (hours < 23)
        {
            hours++;
            minutes = 0;
        }
        else
        {
            hours = 0;
            minutes = 0;
        }

        FormatTime();
        StartCoroutine(ChangeTime());
    }

    void FormatTime()
    {
        if (hours < 10 && minutes < 10)
        {
            timeText.SetText("0" + hours + ":0" + minutes);
        }
        else if (hours < 10)
        {
            timeText.SetText("0" + hours + ":" + minutes);
        }
        else if (minutes < 10)
        {
            timeText.SetText(hours + ":0" + minutes);
        }
        else
        {
            timeText.SetText(hours + ":" + minutes);
        }
    }

    public void OpenTaskApp()
    {
        allApps.SetActive(false);
        messagesApp.SetActive(false);
        messageOnApp.SetActive(false);
        todoApp.SetActive(true);
        

    }

    public void OpenmessagesApp()
    {
        allApps.SetActive(false);
        todoApp.SetActive(false);
        messageOnApp.SetActive(false);
        messagesApp.SetActive(true);
    }
    public void OpenMessagePanel()
    {
        allApps.SetActive(false);
        todoApp.SetActive(false);
        messagesApp.SetActive(false);
        messageOnApp.SetActive(true);

    }
    public void BackToAllApps()
    {
        allApps.SetActive(true);
        todoApp.SetActive(false);
        messagesApp.SetActive(false);
        messageOnApp.SetActive(false);

        //set all apps to false active
    }
   
   

    public void TogglePhone()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            miniPhone.SetActive(true);
            phoneStatus= PhoneStatus_Enum.ClosePhone;
        }
        else
        {
            gameObject.SetActive(true);
            miniPhone.SetActive(false);
            phoneStatus= PhoneStatus_Enum.OpenPhone;
        }
    }

  
}
