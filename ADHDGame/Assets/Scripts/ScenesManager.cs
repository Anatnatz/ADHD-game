using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    bool bathFirstAppearance = true;
    bool kitchenFirstAppearance = true;
    bool bedroomFirstAppearance = true;
    [SerializeField]
    introtext introText;
    [SerializeField]
    scoreController score;
    public static void SwitchToScene(string name)
    {
        SoundManager.RegisterAction(SoundManager.SoundAction.click);
        SceneManager.LoadScene(name);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "Bedroom":
               StartCoroutine( loadIntroText());
                break;

            case "Bathroom":
                //  StepOnLegos();
                break;
            case
                "Kitchen":
                KitchenThoughts();
                break;
        }
    }

    private IEnumerator loadIntroText()
    {
        
            
        score.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        introText.gameObject.SetActive(true);


    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

    }

    void StepOnLegos()
    {
        if (bathFirstAppearance)
        {
            bathFirstAppearance = false;
            Thoughts_Manager.ThoughtsInstance.createThought(Thought_Enum.Stepping_on_legos);
            Debug.Log("beth");
        }
    }

    void KitchenThoughts()
    {
        if (kitchenFirstAppearance)
        {
            kitchenFirstAppearance = false;
            // Thoughts_Manager.ThoughtsInstance.startWaitGapThought(Thought_Enum.Unload_dishes);
            // Thoughts_Manager.ThoughtsInstance.startWaitGapThought(Thought_Enum.Take_lunch);

        }
    }


}
