using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager gameInstance;

    public static bool wakeUp;

    public bool doingTask = false;

    public GameObject pausePanel;
    public GameObject pauseButton;

    void Awake()
    {
        gameInstance = this;
    }

    public void StartGame(int level)
    {
        wakeUp = true;
        switch (level)
        {
            case 1:
                InfoManager.instance.SendInfoMessage("Tutorial");
                ScenesManager.SwitchToScene("Bedroom");
                StartLevel1();
                break;
        }
    }

    void StartLevel1()
    {
        PhoneController.instance.TogglePhone();
        PhoneController.instance.TogglePhone();
        StartCoroutine(ScenesManager.instance.loadIntroText());

        SoundManager.instance.PlayMusic();
        MessageController.messageControlInstance.SendMessage(MessageName_Enum.Good_morning);
        SoundManager.instance.PlayMusic();

        TaskOnApp_Manager.TaskOnAppInstance.createTaskOnAppTransform(Task_Enum.GoOut);
    }

    public static void QuitGame()
    {
        SoundManager.RegisterAction(SoundManager.SoundAction.click);
        Application.Quit();
    }

    public void ToMainMenu()
    {
        ScenesManager.SwitchToScene("MainMenu");
    }

    public void PauseGame()
    {
        SoundManager.RegisterAction(SoundManager.SoundAction.click);
        pausePanel.transform.SetAsLastSibling();
        pauseButton.transform.SetAsLastSibling();
        pausePanel.SetActive(!pausePanel.activeSelf);
        if (pausePanel.activeSelf)
        {
            PhoneController.instance.ResumeTime();
        }
        else
        {
            PhoneController.instance.StopTime();
        }

    }


}
