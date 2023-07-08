using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MouseOver : MonoBehaviour
{
    [SerializeField]
    public TMP_Text info;



    public void OnMouseEnter()
    {
        Debug.Log("trigger");

        if (this.transform.tag == Tags_Enum.Message.ToString())
        {
            Debug.Log("trigger");
            info.gameObject.SetActive(true);
            info.text = "Read more";
        }

        if (this.transform.tag == Tags_Enum.Xmessage.ToString())
        {
            info.gameObject.SetActive(true);
            info.text = "Read Later";
        }

    }

    public void OnMouseExit()
    {
        if (this.transform.tag == Tags_Enum.Message.ToString())
        {
            info.gameObject.SetActive(false);
            info.text = "";
        }

        if (this.transform.tag == Tags_Enum.Xmessage.ToString())
        {
            info.gameObject.SetActive(false);
            info.text = "";
        }

    }
}
