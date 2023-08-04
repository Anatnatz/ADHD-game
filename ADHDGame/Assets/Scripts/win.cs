
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;



public class win : MonoBehaviour

{
    public static win instance;
    public bool winCondition = false;
  



    public void Start()
    {
        instance = this;
        
        
    }

    

    public void checkWinCondition()
    {
        Debug.Log("you win?");
        StartCoroutine(CameraZoom.instance.ZoomInDoor());

    }

    

   
    }

