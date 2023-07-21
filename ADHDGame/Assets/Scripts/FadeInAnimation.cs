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
            StartCoroutine(WakeUpAnimation());
        }
    }

    IEnumerator WakeUpAnimation()
    {
        while (fadeinGroup.alpha > 0)
        {
            fadeinGroup.alpha -= 0.005f;
            yield return null;
        }

        Game_Manager.wakeUp = false;
    }
}
