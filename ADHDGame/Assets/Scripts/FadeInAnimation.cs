using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInAnimation : MonoBehaviour
{
    private CanvasGroup fadeinGroup;

    void Start()
    {
        fadeinGroup = GetComponent<CanvasGroup>();
        if (Game_Manager.wakeUp)
        {
            fadeinGroup.alpha = 1;
        }
    }

    void Update()
    {
        if (Game_Manager.wakeUp)
        {
            if (fadeinGroup.alpha > 0)
            {
                fadeinGroup.alpha -= Time.deltaTime * 0.3f;
            }
            else
            {
                Game_Manager.wakeUp = false;
            }
        }
    }
}
