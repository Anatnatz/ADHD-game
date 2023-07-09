using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager gameInstance;

    public static bool wakeUp;

    public void StartGame(int level)
    {
        wakeUp = true;
        switch (level)
        {
            case 1:
                InfoManager.instance.SendInfoMessage("Tutorial");
                ScenesManager.SwitchToScene("Bedroom");
                break;
        }
    }
}
