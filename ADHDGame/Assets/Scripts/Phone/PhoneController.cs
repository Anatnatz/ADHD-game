using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PhoneController : MonoBehaviour
{
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

    [Header("MiniPhone")]
    [SerializeField]
    private GameObject miniPhone;

    void Start()
    {
        FormatTime();
        StartCoroutine(MoveTime(0));
    }

    public void ChangeTime(int minInterval)
    {
        if (minutes + minInterval < 60)
        {
            minutes += minInterval;
        }
        else if (hours < 23)
        {
            hours++;
            minutes = (minutes + minInterval) % 60;
        }
        else
        {
            hours = 0;
            minutes = (minutes + minInterval) % 60;
        }

        FormatTime();
    }

    IEnumerator MoveTime(int minInterval)
    {
        yield return new WaitForSeconds(gameMinute);
        ChangeTime (minInterval);
        StartCoroutine(MoveTime(minInterval));
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
        todoApp.SetActive(true);
    }

    public void BackToAllApps()
    {
        allApps.SetActive(true);
        todoApp.SetActive(false);

        //set all apps to false active
    }

    public void TogglePhone()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            miniPhone.SetActive(true);
        }
        else
        {
            gameObject.SetActive(true);
            miniPhone.SetActive(false);
        }
    }

    public void MoveTimeXTimes(float x)
    {
        gameMinute /= x;
    }

    public void AddToTime(int addMinutes)
    {
        ChangeTime (addMinutes);
    }
}
