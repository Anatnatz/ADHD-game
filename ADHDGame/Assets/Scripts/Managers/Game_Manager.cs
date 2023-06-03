using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager gameInstance;

    void Start()
    {
        gameInstance = this;
        ScenesManager.LoadStartScenes();
    }
}
