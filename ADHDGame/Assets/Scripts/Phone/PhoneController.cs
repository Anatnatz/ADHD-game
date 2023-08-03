using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class PhoneController : MonoBehaviour
{
    public static PhoneController instance;
    public PhoneStatus_Enum phoneStatus;

    [Header("Game minute duration in seconds")]
    [SerializeField]
    public float gameMinute;
    public float originalGameMinute;

    [Header("Start Time")]
    [SerializeField]
    private int hours;

    [SerializeField]
    private int minutes;

    [SerializeField]
    private TMP_Text timeText;
    [SerializeField]
    private TMP_Text smallTimeText;

    [Header("End Level Time")]
    [SerializeField]
    private int endHours;
    [SerializeField]
    private int endMinutes;

    [Header("Apps")]
    [SerializeField]
    private GameObject allApps;

    [SerializeField]
    private Transform mainScreenContent;

    [SerializeField]
    private GameObject todoApp;

    [SerializeField]
    private GameObject tiktokApp;

    [SerializeField]
    private VideoPlayer tiktokPlayer;

    [SerializeField]
    private GameObject messagesApp;


    [SerializeField]
    private GameObject messageOnApp;

    [Header("MiniPhone")]
    [SerializeField]
    private GameObject miniPhone;

    [SerializeField]
    private GameObject fullPhone;

    [Header("Tiktok Doom Counter")]
    [SerializeField]
    private float timeUntilScroll;

    public static float noTouchTime = 0;
    bool firstTimeTiktok = true;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        FormatTime();
        StartCoroutine(MoveTime(1));
    }

    void Update()
    {
        if (fullPhone.activeSelf)
        {
            if (noTouchTime >= timeUntilScroll && !tiktokApp.activeSelf)
            {
                if (firstTimeTiktok)
                {
                    timeUntilScroll = 25f;
                }

                OpenTiktokApp();
            }
            noTouchTime += Time.deltaTime;
        }
        else
        {
            noTouchTime = 0;
        }
    }

    public void ResetTouchTime()
    {
        noTouchTime = 0;
    }

    public float GetCurrentTime()
    {
        float time = (float)hours + ((float)minutes / 100f);
        return time;
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
        ChangeTime(minInterval);
        if (LevelHasMoreTime())
        {
            StartCoroutine(MoveTime(minInterval));
        }
        else
        {
            StopCoroutine(MoveTime(minInterval));
        }
    }

    bool LevelHasMoreTime()
    {
        bool moreTime = true;
        if (hours >= endHours && minutes >= endMinutes)
        {
            moreTime = false;
            Debug.Log("end of level!");
            if (ScenesManager.GetActiveScene() != "MainMenu")
            {
                introtext.instance.changeIntroText("Time is up. Run out!");
            }
            StartCoroutine(endLevel());
            //ScenesManager.SwitchToScene("Kitchen");
            //Game_Manager.gameInstance.PauseGame();
        }

        return moreTime;
    }
    internal IEnumerator endLevel()
    {
        yield return new WaitForSeconds(3f);
        if (ScenesManager.GetActiveScene() == "Bathroom")
        {
            ScenesManager.SwitchToScene("Bedroom");
            yield return new WaitForSeconds(3f);
        }

        ScenesManager.SwitchToScene("Kitchen");
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(CameraZoom.instance.ZoomInDoor());
        // Game_Manager.gameInstance.PauseGame();
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

        smallTimeText.SetText(timeText.text);
    }

    public void HideAllApps()
    {
        SoundManager.RegisterAction(SoundManager.SoundAction.click);
        tiktokPlayer.Pause();
        allApps.SetActive(false);
        messagesApp.SetActive(false);
        messageOnApp.SetActive(false);
        tiktokApp.SetActive(false);
        todoApp.SetActive(false);
    }

    public void OpenTaskApp()
    {
        HideAllApps();
        todoApp.SetActive(true);
    }
    public void OpenTiktokApp()
    {
        HideAllApps();
        tiktokApp.SetActive(true);

        tiktokPlayer.Play();
        MoveTimeXTimes(5f);
    }

    public void OpenmessagesApp()
    {
        HideAllApps();
        messagesApp.SetActive(true);
    }
    public void OpenMessagePanel()
    {
        HideAllApps();
        messageOnApp.SetActive(true);
    }

    public void BackToAllApps()
    {
        if (tiktokApp.activeSelf)
        {
            noTouchTime = 0;
            tiktokPlayer.Pause();
            MoveTimeXTimes(0.2f);
        }
        HideAllApps();
        allApps.SetActive(true);

        mainScreenContent.position = new Vector2(mainScreenContent.position.x, 0);

        //set all apps to false active
    }



    public void TogglePhone()
    {
        if (fullPhone.activeSelf)
        {
            BackToAllApps();
            fullPhone.SetActive(false);
            miniPhone.SetActive(true);
            phoneStatus = PhoneStatus_Enum.ClosePhone;
        }
        else
        {
            SoundManager.RegisterAction(SoundManager.SoundAction.click);
            fullPhone.SetActive(true);
            miniPhone.SetActive(false);
            phoneStatus = PhoneStatus_Enum.OpenPhone;
        }
    }

    public void MoveTimeXTimes(float x)
    {
        StopTime();
        gameMinute /= x;
        ResumeTime();
    }

    public void StopTime()
    {
        // originalGameMinute = gameMinute;
        // gameMinute = 0;
        StopCoroutine(MoveTime(1));
    }

    public void ResumeTime()
    {
        // gameMinute = originalGameMinute;
        StartCoroutine(MoveTime(1));
    }

    public void AddToTime(int addMinutes)
    {
        ChangeTime(addMinutes);
    }
}
