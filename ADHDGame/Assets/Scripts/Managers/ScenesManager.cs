using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public enum SceneNames
    {
        GameUI,
        Bedroom,
        Bathroom
    }

    public static void LoadStartScenes()
    {
        if (SceneManager.GetActiveScene().name != "GameUI")
        {
            SceneManager.LoadScene("GameUI");
        }

        SceneManager.LoadScene("Bedroom", LoadSceneMode.Additive);
    }

    public static void GoToBathroom()
    {
        SceneManager.UnloadSceneAsync("Bedroom");
        SceneManager.LoadScene("Bathroom", LoadSceneMode.Additive);
    }

    public static void GoToBedroom()
    {
        SceneManager.UnloadSceneAsync("Bathroom");
        SceneManager.LoadScene("Bedroom", LoadSceneMode.Additive);
    }
}
