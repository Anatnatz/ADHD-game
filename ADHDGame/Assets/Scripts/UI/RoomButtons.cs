using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomButtons : MonoBehaviour
{
    public Button bathroom_btn;

    public Button bedroom_btn;

    void Start()
    {
        bathroom_btn.onClick.AddListener (GoToBathroom);
        bedroom_btn.onClick.AddListener (GoToBedroom);
    }

    void GoToBathroom()
    {
        ScenesManager.GoToBathroom();
    }

    void GoToBedroom()
    {
        ScenesManager.GoToBedroom();
    }
}
