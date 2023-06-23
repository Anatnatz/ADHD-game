using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PhoneController : MonoBehaviour
{
    [SerializeField]
    private int hours;

    [SerializeField]
    private int minutes;

    [SerializeField]
    private TMP_Text timeText;

    void Start()
    {
        FormatTime();
        StartCoroutine(ChangeTime());
    }

    IEnumerator ChangeTime()
    {
        yield return new WaitForSeconds(3f);
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
}
