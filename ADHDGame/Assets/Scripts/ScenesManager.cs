using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    bool bathFirstAppearance = true;
    bool kitchenFirstAppearance = true;
    public static void SwitchToScene(string name)
    {
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
            case "Bathroom":
              //  StepOnLegos();
                break;
            case
                "Kitchen":
                KitchenThoughts();
                break;
        }
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
