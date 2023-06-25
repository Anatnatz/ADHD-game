using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static void SwitchToScene(string name)
    {
        SceneManager.LoadScene (name);
    }
}
